using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UIManager : MonoBehaviour {

    // All component references are setup in the editor. This can help performance if you have lots of objects, so that it doesn't have to be in in Awake/Start
    public GameObject MenuScreen;
    public GameObject GameScreen;

    public GameObject GameOver;
    public GameObject LevelComplete;
    public GameObject GameComplete;
    public GameObject HUD;

    public GameObject ScreenFade;
    public GameObject PanelAYS;

    public Button PanelAYS_Cancel;
    public Button PanelAYS_Do;

    public TextMeshProUGUI PanelAYS_Question;
    public TextMeshProUGUI PanelAYS_ButtonCancel;
    public TextMeshProUGUI PanelAYS_ButtonDo;

    public TextMeshProUGUI HUD_Score;
    public TextMeshProUGUI HUD_Level;
    public TextMeshProUGUI HUD_BricksLeft;

    private bool panelOpen;
    private char[] intString = new char[10];

    private void Awake() {
        Globals.UIManager = this;
    }

    public void ButtonClickStart() {
        MenuScreen.SetActive(false);

        Globals.GameManager.StartNewGame();
    }

    public void ButtonClickExitApp() {
        Application.Quit();

        // Quit doesn't work in the editor, so unpress the play button instead
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void Update() {
        // Do they want to quit?
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (panelOpen) {
                PanelClose();
            }
            else {
                if (Globals.GameManager.gameObject.activeInHierarchy) {
                    PanelShow(Consts.UI.PANEL_AYS.GAME_EXIT.AYS_QUESTION, Consts.UI.PANEL_AYS.GAME_EXIT.DO_BUTTON, Globals.GameManager.ExitGame , Consts.UI.PANEL_AYS.GAME_EXIT.CANCEL_BUTTON, PanelClose);
                }
                else {
                    PanelShow(Consts.UI.PANEL_AYS.APP_EXIT.AYS_QUESTION, Consts.UI.PANEL_AYS.APP_EXIT.DO_BUTTON, ButtonClickExitApp, Consts.UI.PANEL_AYS.APP_EXIT.CANCEL_BUTTON, PanelClose);
                }
            }
        }
    }

    private void PanelShow(string question, string doButton, UnityAction doAction, string cancelButton, UnityAction cancelAction) {
        // Stop time so that any current game is paused
        Time.timeScale = 0;

        panelOpen = true;

        ScreenFade.SetActive(true);
        PanelAYS.SetActive(true);

        PanelAYS_Question.text = question;

        PanelAYS_ButtonDo.text = doButton;
        PanelAYS_Do.onClick.RemoveAllListeners();
        PanelAYS_Do.onClick.AddListener(() => PanelDo(doAction));

        PanelAYS_ButtonCancel.text = cancelButton;
        PanelAYS_Cancel.onClick.RemoveAllListeners();
        PanelAYS_Cancel.onClick.AddListener(cancelAction);
    }

    private void PanelDo(UnityAction action) {
        PanelClose();

        action();
    }

    private void PanelClose() {
        // Restart time for any paused game
        Time.timeScale = 1;

        panelOpen = false;

        ScreenFade.SetActive(false);
        PanelAYS.SetActive(false);
    }

    public void ShowGameScreen() {
        GameOver.SetActive(false);
        LevelComplete.SetActive(false);
        GameComplete.SetActive(false);

        HUD.SetActive(true);
        GameScreen.SetActive(true);

        UpdateHUD();
    }

    public void ShowLevelComplete() {
        LevelComplete.SetActive(true);
    }

    public void ShowGameComplete() {
        GameComplete.SetActive(true);
    }

    public void ShowStartScreen() {
        GameScreen.SetActive(false);
        MenuScreen.SetActive(true);
    }

    public void UpdateHUD() {
        // Convert int to string without garbage
        Utils.IntToString(Globals.GameManager.TheScore, intString);
        HUD_Score.SetText(intString);

        // Convert int to string without garbage
        Utils.IntToString(Globals.BrickManager.BricksToBreak, intString);
        HUD_BricksLeft.SetText(intString);
    }

    public void UpdateLevel() {
        // Convert int to string without garbage
        Utils.IntToString(Globals.GameManager.TheLevel, intString);
        HUD_Level.SetText(intString);
    }
}
