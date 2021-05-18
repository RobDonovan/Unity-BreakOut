using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    public GameObject BallPrefab;

    private int lastSpawnedGOidx;
    private int activeBalls;

    private void Awake() {
        Globals.BallManager = this;

        // Spawn a pool so we don't have to create objects during play. In bigger games creating object on the fly can cause performance problems and garbage.
        for (int b = 0; b < Consts.BALL_POOL_START_NUM; b++) {
            CreateNewBallGO();
        }

        lastSpawnedGOidx = -1;
        activeBalls = 0;
    }
    
    private void CreateNewBallGO() {
        GameObject ball = Instantiate(BallPrefab, transform) as GameObject;
        ball.SetActive(false);
        ball.transform.SetAsLastSibling();
    }

    public int SpawnBall(Vector2 startpos, Vector2 startDir, float startSpeed, bool startMoving) {
        bool foundOne = false;

        // See if there is a 'free' one. Mostly, the next one will be free, so starting at the last one used is good for performance
        for (int b = 0; b < transform.childCount; b++) {
            lastSpawnedGOidx++;

            if (lastSpawnedGOidx == transform.childCount) {
                lastSpawnedGOidx = 0;
            }

            // It's not used, so use this
            if (!transform.GetChild(lastSpawnedGOidx).gameObject.activeInHierarchy) {
                foundOne = true;
                break;
            }
        }

        // No fee one, so create a new
        if (!foundOne) {
            CreateNewBallGO();
            lastSpawnedGOidx = transform.childCount - 1;
        }

        activeBalls++;

        transform.GetChild(lastSpawnedGOidx).GetComponent<Ball>().SpawnMe(startpos, startDir, startSpeed, startMoving);

        return lastSpawnedGOidx;
    }

    public void StartBalls() {
        for (int b = 0; b < transform.childCount; b++) {
            if (transform.GetChild(b).gameObject.activeInHierarchy) {
                transform.GetChild(b).GetComponent<Ball>().StartBallMoving();
            }
        }
    }

    public void KillBall(Ball theBall) {
        theBall.KillMe();

        activeBalls--;

        // No balls left, so end the game
        if (activeBalls == 0) {
            Globals.GameManager.EndGame();
        }
    }

    public void ResetBalls() {
        for (int b = 0; b < transform.childCount; b++) {
            if (transform.GetChild(b).gameObject.activeInHierarchy) {
                transform.GetChild(b).GetComponent<Ball>().KillMe();
            }
        }

        activeBalls = 0;
    }

    private void AddTripleBall(Collectable col, int ball, int dir) {
        // Give the new ball a slightly different angle
        Vector2 newDir = new Vector2(transform.GetChild(ball).GetComponent<Rigidbody2D>().velocity.x + Random.Range(0.1f * dir, 0.5f * dir), 1);
        newDir.Normalize();
        
        int ballIdx = SpawnBall(transform.GetChild(ball).position, newDir, transform.GetChild(ball).GetComponent<Ball>().BallCurrSpeed, true);
        transform.GetChild(ballIdx).GetComponent<Ball>().CollectableStart(col);
    }
    
    public void CollectableStart(Collectable collectable) {
        switch (collectable.CollectableType) {
            case Collectable.CollectableTypes.PowerBall:
                for (int b = 0; b < transform.childCount; b++) {
                    if (transform.GetChild(b).gameObject.activeInHierarchy) {
                        transform.GetChild(b).GetComponent<Ball>().CollectableStart(collectable);
                    }
                }
                break;
            case Collectable.CollectableTypes.TripleBall:
                for (int b = 0; b < transform.childCount; b++) {
                    if (transform.GetChild(b).gameObject.activeInHierarchy) {
                        AddTripleBall(collectable, b, 1);
                        AddTripleBall(collectable, b, -1);
                        break;
                    }
                }
                break;
        }
    }
}
