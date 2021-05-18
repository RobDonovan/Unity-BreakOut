using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour {
    public Walls TheWallsData;

    private GameObject currentWallGO;

    private int bricksToBreak;
    public int BricksToBreak {
        get { return bricksToBreak; }
        set {
            bricksToBreak = value;

            // Update HUD
            Globals.UIManager.UpdateHUD();

            // Was it the last brick?
            if (bricksToBreak == 0) {
                Globals.GameManager.EndLevel();
            }
        }
    }

    private void Awake() {
        Globals.BrickManager = this;
    }

    public void AddNewBrick(bool brickBreakable) {
        // Keep track of the number of bricks to break, so we know when the level is complete
        if (brickBreakable) {
            BricksToBreak++;
        }
    }

    public void DestroyBrick() {
        // Keep track of the number of bricks to break, so we know when the level is complete
        BricksToBreak--;
    }

    public void LoadNewBricks(int levelID) {
        int wallID = levelID - 1;

        bricksToBreak = 0;

        if (currentWallGO) {
            Destroy(currentWallGO);
        }

        currentWallGO = Instantiate(TheWallsData.WallList[wallID].WallPrefab, Globals.GameManager.TheCurrentWall.transform);

        Globals.GameManager.TheBat.SetupBat(TheWallsData.WallList[wallID].BatStartPos, TheWallsData.WallList[wallID].BatSpeed);

        Globals.BallManager.SpawnBall(TheWallsData.WallList[wallID].BallStartPos, TheWallsData.WallList[wallID].BallStartDirection, TheWallsData.WallList[wallID].BallSpeed, false);
    }
}
