using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
    
    public BrickType BrickType;
    public Collectable BrickCollectable;
    public GameObject BrickPhysical;

    public GameObject CollectableIcon;

    private int timesHit;

    private void Awake() {
        // Let the manager know we have a new brick
        Globals.BrickManager.AddNewBrick(BrickType.BrickHitsToBreak > 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // When PowerBall is active, the ball passes through bricks, but we still want to destroy them, so we need a trigger event
        if (collision.CompareTag(Consts.TAGS.BALL) && collision.GetComponent<Ball>().PowerBall) {
            CollisionStart(collision);
        }
    }

    public void CollisionStart(Collider2D collision) {
        // Collision layers are toggled on/off in Edit/Project Settings/Physics 2D/ so that only the nessesary collisions create callbacks here. Helps performance.
        // Collision can be from a collision or trigger. If we have PowerBall collectable then we dont have colliders only triggers so the ball can pass through the bricks.

        bool batRocket = collision.CompareTag(Consts.TAGS.BAT_ROCKET);
        bool ball = collision.CompareTag(Consts.TAGS.BALL);

        if (ball) {
            timesHit++;
        }

        // See if we have hit the brick enough to break it.
        if (BrickType.BrickHitsToBreak != 0) {
            if (timesHit == BrickType.BrickHitsToBreak || batRocket || (ball && collision.GetComponent<Ball>().PowerBall)) {
                Globals.BrickManager.DestroyBrick();
                Globals.GameManager.UpdateScore(GameManager.GameScoreType.BrickDestroyed);

                if (BrickCollectable) {
                    Globals.CollectableItemManager.SpawnCollectable(BrickCollectable, transform.position);
                }

                gameObject.SetActive(false);
            }
            else {
                Globals.GameManager.UpdateScore(GameManager.GameScoreType.BrickHit);
            }
        }

        // Once the rocket hits a brick, it's dead
        if (batRocket) {
            collision.GetComponent<BatRocket>().DestroyRocket();
        }
    }
}
