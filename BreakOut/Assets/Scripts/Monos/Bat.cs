using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {

    // All component references are setup in the editor. This can help performance if you have lots of objects, so that it doesn't have to be in in Awake/Start
    public Rigidbody2D TheBatRB;
    public SpriteRenderer TheBatSPR;

    private float batCurrentSpeed;
    private float batSegSize;

    public void SetVelocity(float newVel) {
        TheBatRB.velocity = new Vector2(newVel * batCurrentSpeed, 0);
    }

    public void SetupBat(Vector2 startPos, float startSpeed) {
        transform.position = startPos;
        batCurrentSpeed = startSpeed;
        batSegSize = transform.localScale.x / 2.0f;
    }

    public Vector2 GetBouceDir(Vector2 ballDir, Vector2 ballHitPos) {
        // The bat has different physics that the Unity standard which we use for walls and bricks.
        // Depending on where it hits on the bat we want the direction to be changed differently, to give the play some control on what they can do to the ball, making it more fun.

        Vector2 retVal = Vector2.zero;
        Vector2 batOffset = ballHitPos - (Vector2)transform.position;

        float offsetX = Mathf.Abs(batOffset.x);

        // Scale to 0-1 for bat size
        offsetX /= batSegSize;

        // Clamp
        offsetX = Mathf.Clamp(offsetX, 0, 1); // Incase we catch past the edge of the bat a little

        // If in the middle section of the bat, then fav up and steeper angles
        if (offsetX <= 0.5f) {
            retVal = new Vector2(offsetX * 2, 1);
        }
        else {
            // Otherwise favor left/right and shallower angles but not 'too' shallow
            float yVal = (1 - offsetX) * 2;

            if (yVal < 0.1f) {
                yVal = 0.1f;
            }

            retVal = new Vector2(1, yVal);
        }

        // If hit on left side of the bat, invert the direction left/right
        if (batOffset.x < 0) {
            retVal.x *= -1;
        }

        return retVal;
    }
}

