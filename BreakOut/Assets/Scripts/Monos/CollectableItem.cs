using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour {

    // All component references are setup in the editor. This can help performance if you have lots of objects, so that it doesn't have to be in in Awake/Start
    public Rigidbody2D TheRB;
    public SpriteRenderer TheSPR;

    private Collectable collectable;
    
    private void OnCollisionEnter2D(Collision2D collision) {
        // Collision layers are toggled on/off in Edit/Project Settings/Physics 2D/ so that only the nessesary collisions create callbacks here. Helps performance.

        // If it hits the bat, then start the action
        if (collision.collider.CompareTag(Consts.TAGS.BAT)) {
            switch (collectable.CollectableType) {
                case Collectable.CollectableTypes.TripleBall:
                    Globals.BallManager.CollectableStart(collectable);
                    break;
                case Collectable.CollectableTypes.PowerBall:
                    Globals.BallManager.CollectableStart(collectable);
                    break;
                case Collectable.CollectableTypes.BatRocket:
                    Globals.GameManager.TheBatRocket.CollectableStart(collectable);
                    break;
            }

            KillMe();
        }

        // If it hits the bottom then they lost it
        if (collision.collider.CompareTag(Consts.TAGS.BOUNDARY_KILL)) {
            KillMe();
        }
    }

    public void KillMe() {
        gameObject.SetActive(false);
    }

    public void SpawnMe(Collectable type, Vector2 spawnPos) {
        // Set up collectable 
        collectable = type;
        transform.position = spawnPos;

        TheSPR.sprite = type.CollectableSprite;

        gameObject.SetActive(true);

        // Force it to fall
        TheRB.AddForce(new Vector2(0, -type.CollectableDropSpeed));
    }
}
