using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemManager : MonoBehaviour {

    public GameObject CollectableItemPrefab;

    private int lastSpawnedGOidx;

    private void Awake() {
        Globals.CollectableItemManager = this;

        // Spawn a pool so we don't have to create objects during play. In bigger games creating object on the fly can cause performance problems and garbage.
        for (int c = 0; c < Consts.COLLECTABLE_POOL_START_NUM; c++) {
            CreateNewCollectableGO();
        }

        lastSpawnedGOidx = -1;
    }
    
    public void SpawnCollectable(Collectable collectable, Vector2 spawnPos) {
        bool foundOne = false;

        // See if there is a 'free' one. Mostly, the next one will be free, so starting at the last one used is good for performance
        for (int c = 0; c < transform.childCount; c++) {
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

        // No fee one, so we have to create a new one
        if (!foundOne) {
            CreateNewCollectableGO();
            lastSpawnedGOidx = transform.childCount - 1;
        }

        transform.GetChild(lastSpawnedGOidx).GetComponent<CollectableItem>().SpawnMe(collectable, spawnPos);
    }

    private void CreateNewCollectableGO() {
        GameObject colletable = Instantiate(CollectableItemPrefab, transform) as GameObject;
        colletable.SetActive(false);
        colletable.transform.SetAsLastSibling();
    }

    public void ResetCollectableItems() {
        for (int c = 0; c < transform.childCount; c++) {
            transform.GetChild(c).GetComponent<CollectableItem>().KillMe();
        }
    }
}
