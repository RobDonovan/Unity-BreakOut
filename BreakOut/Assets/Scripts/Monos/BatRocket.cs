using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRocket : MonoBehaviour {

    // All component references are setup in the editor. This can help performance if you have lots of objects, so that it doesn't have to be in in Awake/Start
    public Rigidbody2D TheRB;

    private bool inFlight;
    private float rocketStartTime;
    private Collectable_BatRocket collectable;

    private void Update() {
        UpdatePos();

        // If we have past it's duration, disable
        if (inFlight) {
            if (Time.time - rocketStartTime > collectable.CollectableDuration) {
                DisableRocket();
            }
        }
        else { 
            // Space or Left Mouse to fire
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                FireRocket();
            }
        }
    }

    private void UpdatePos() {
        // If we haven't fired it yet, then just keep it synced to the bat
        if (!inFlight) {
            transform.position = Globals.GameManager.TheBat.transform.position + new Vector3(0, 0.7f, 0);
        }
    }

    private void SetVelocity(float vel) {
        TheRB.AddForce(new Vector2(0, vel));
    }

    private void FireRocket() {
        inFlight = true;
        SetVelocity(collectable.RocketSpeed);
    }

    public void CollectableStart(Collectable coll) {
        collectable = (Collectable_BatRocket)coll;

        inFlight = false;
        rocketStartTime = Time.time;

        UpdatePos();

        gameObject.SetActive(true);
    }

    public void DisableRocket() {
        inFlight = false;
        gameObject.SetActive(false);
    }

    public void DestroyRocket() {
        inFlight = false;
        UpdatePos();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Collision layers are toggled on/off in Edit/Project Settings/Physics 2D/ so that only the nessesary collisions create callbacks here. Helps performance.

        // Destroy the rocket if it hits a wall
        if (collision.collider.CompareTag(Consts.TAGS.BOUNDARY_BOUNCE)) {
            DestroyRocket();
        }
    }
}
