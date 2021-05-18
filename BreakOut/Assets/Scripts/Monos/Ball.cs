using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    // All component references are setup in the editor. This can help performance if you have lots of objects, so that it doesn't have to be in in Awake/Start
    public Rigidbody2D TheBallRB;
    public SpriteRenderer TheBallSPR;

    private Vector2 ballStartDir;
    private Color startColor;

    private Collectable collectable;

    private float powerBallStartTime;

    private int layerBall;
    private int layerBrickPhysics;

    public float BallCurrSpeed { get; private set; }

    private bool powerBall;
    public bool PowerBall {
        get { return powerBall; }
        private set {
            powerBall = value;

            if (value) {
                powerBallStartTime = Time.time;
                Physics2D.IgnoreLayerCollision(layerBall, layerBrickPhysics, true); // Ignore physical collisions, but still do the trigger
            }
            else {
                TheBallSPR.color = startColor;
                Physics2D.IgnoreLayerCollision(layerBall, layerBrickPhysics, false); // Back to physical collisions
            }
        }
    }

    private void Awake() {
        startColor = TheBallSPR.color;

        // Cache Layer IDs
        layerBall = LayerMask.NameToLayer(Consts.LAYERS.BALL);
        layerBrickPhysics = LayerMask.NameToLayer(Consts.LAYERS.BRICKS_PHYSICS);
    }

    private void Update() {
        // If we have the PowerBall collectable, then pulse color and keep track of duration
        if (PowerBall) {
            float dur = Time.time - powerBallStartTime;
            TheBallSPR.color = Color.Lerp(startColor, ((Collectable_PowerBall)collectable).TheColor, Mathf.PingPong(dur * 5, 1));

            if (dur > collectable.CollectableDuration) {
                PowerBall = false;
            }
        }
    }

    public void StartBallMoving() {
        SetVelocity(ballStartDir);
    }

    private void SetVelocity(Vector2 newDir) {
        TheBallRB.velocity = newDir.normalized * BallCurrSpeed;
    }

    public void SpawnMe(Vector2 startpos, Vector2 startDir, float startSpeed, bool startMoving) {
        transform.position = startpos;

        ballStartDir = startDir;
        BallCurrSpeed = startSpeed;

        gameObject.SetActive(true);

        if (startMoving) {
            StartBallMoving();
        }

        PowerBall = false;
    }

    public void KillMe() {
        SetVelocity(Vector2.zero);
        gameObject.SetActive(false);
    }

    public void CollectableStart(Collectable col) {
        collectable = col;

        switch (col.CollectableType) {
            case Collectable.CollectableTypes.PowerBall:
                PowerBall = true;
                break;
            case Collectable.CollectableTypes.TripleBall:
                StartBallMoving();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Catch some wierd movements that don't make things 'fun'.

        // No Vertical movement, so give it a nudge randomly up/down
        if (Mathf.Approximately(TheBallRB.velocity.y, 0)) {
            int negPos = Random.Range(0, 2) * 2 - 1;
            SetVelocity(new Vector2(TheBallRB.velocity.x, 0.2f * negPos));
        }
        // Vertical angle too small, so amplify it in the same direction
        else if (TheBallRB.velocity.y > 0 && TheBallRB.velocity.y < 0.2f) {
            SetVelocity(new Vector2(TheBallRB.velocity.x, 0.2f));
        }
        else if (TheBallRB.velocity.y < 0 && TheBallRB.velocity.y > -0.2f) {
            SetVelocity(new Vector2(TheBallRB.velocity.x, -0.2f));
        }

        // Horizontal angle too small, so amplify it in the same direction
        if (TheBallRB.velocity.x >= 0 && TheBallRB.velocity.x < 0.2f) {
            SetVelocity(new Vector2(0.2f, TheBallRB.velocity.y));
        }
        else if (TheBallRB.velocity.x < 0 && TheBallRB.velocity.x > -0.2f) {
            SetVelocity(new Vector2(-0.2f, TheBallRB.velocity.y));
        }

        // If we hit the bat, then we want it to behave differently to standard physics
        if (collision.collider.CompareTag(Consts.TAGS.BAT)) {
            SetVelocity(Globals.GameManager.TheBat.GetBouceDir(TheBallRB.velocity, collision.contacts[0].point));
        }

        // Hit the bottom so kill it
        if (collision.collider.CompareTag(Consts.TAGS.BOUNDARY_KILL)) {
            Globals.BallManager.KillBall(this);
        }
    }
}
