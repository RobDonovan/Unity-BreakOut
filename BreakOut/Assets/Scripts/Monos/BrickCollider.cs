using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCollider : MonoBehaviour{

    public Brick TheBrick;

    private void OnCollisionEnter2D(Collision2D collision) {
        // Pass the collision on to the Brick
        TheBrick.CollisionStart(collision.collider);
    }

}
