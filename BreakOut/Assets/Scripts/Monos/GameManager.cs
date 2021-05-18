using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject lastHit;

    // All component references are setup in the editor. This can help performance if you have lots of objects, so that it doesn't have to be in in Awake/Start
    public Bat TheBat;
    public GameObject TheCurrentWall;
    public BatRocket TheBatRocket;

    public enum GameScoreType { BrickHit, BrickDestroyed };

    private enum GameStates { NotRunning, WaitingToStart, Running, LevelComplete, GameComplete, Finished };
    
    private int theScore;
    public int TheScore {
        get { return theScore; }
        set {
            theScore = value;

            // Update the HUD
            Globals.UIManager.UpdateHUD();
        }
    }

    private int theLevel;
    public int TheLevel {
        get { return theLevel; }
        set {
            theLevel = value;

            // Update the HUD
            Globals.UIManager.UpdateLevel();
        }
    }

    private float startTime;
    private GameStates currentGameState = GameStates.NotRunning;

    private void Awake() {
        Globals.GameManager = this;

        // These can also be set in Edit/Project Settings/ but I like to put it into code so it's more obvious it's been changed

        // No gravity in this game.
        Physics2D.gravity = Vector2.zero;
        
        // Speed up physics speed so that we can detect collisions better, otherwise in the corners where the ball bounces off the edge and then the bat it sometimes misses the collisions.
        Time.fixedDeltaTime = 0.01f;

        gameObject.SetActive(false);
    }
    
    private void Update() {
        // A little delay to start the game, to give them chance to be ready
        if (currentGameState == GameStates.WaitingToStart) {
            if (Time.unscaledTime - startTime < Consts.GAME_START_DELAY) {
                return;
            }

            Globals.BallManager.StartBalls();
            currentGameState = GameStates.Running;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            switch (currentGameState) {
                case GameStates.Finished:
                    ExitGame();
                    break;
                case GameStates.LevelComplete:
                    StartNewLevel();
                    break;
                case GameStates.GameComplete:
                    ExitGame();
                    break;
            }
        }

        // Move the bat, standard cursor left/right or A/D, can be defined in Edit/Project Settings/Input Manager
        if (currentGameState == GameStates.Running) {
            TheBat.SetVelocity(Input.GetAxisRaw(Consts.INPUT_AXIS));
        }
    }

    public void StartNewGame() {
        gameObject.SetActive(true);

        TheScore = 0;
        TheLevel = 0;

        StartNewLevel();
    }

    private void StartNewLevel() {
        // Increase level and load it in
        TheLevel++;
        Globals.BrickManager.LoadNewBricks(TheLevel);
        
        startTime = Time.unscaledTime;
        currentGameState = GameStates.WaitingToStart;

        Globals.UIManager.ShowGameScreen();
    }

    public void EndLevel() {
        // Reset everything
        Globals.BallManager.ResetBalls();
        Globals.CollectableItemManager.ResetCollectableItems();

        TheBat.SetVelocity(0);
        TheBatRocket.DisableRocket();

        // End of game or go to next level?
        if (TheLevel < Globals.BrickManager.TheWallsData.WallList.Length) {
            currentGameState = GameStates.LevelComplete;
            Globals.UIManager.ShowLevelComplete();
        }
        else {
            currentGameState = GameStates.GameComplete;
            Globals.UIManager.ShowGameComplete();
        }
    }

    public void EndGame() {
        Globals.UIManager.GameOver.SetActive(true);

        TheBat.SetVelocity(0);
        TheBatRocket.DisableRocket();

        currentGameState = GameStates.Finished;
    }

    public void ExitGame() {
        Globals.BallManager.ResetBalls();
        Globals.CollectableItemManager.ResetCollectableItems();

        foreach (Transform child in TheCurrentWall.transform) {
            Destroy(child.gameObject);
        }

        gameObject.SetActive(false);

        Globals.UIManager.ShowStartScreen();
    }

    public void UpdateScore(GameScoreType scoreType) {
        switch (scoreType) {
            case GameScoreType.BrickHit:
                TheScore++;
                break;
            case GameScoreType.BrickDestroyed:
                TheScore += 2;
                break;
        }
    }
}
