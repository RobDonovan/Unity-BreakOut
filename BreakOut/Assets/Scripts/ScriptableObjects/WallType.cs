using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = Consts.SCRIPTABLE_OBJECTS.FILE_NAMES.WALL,
    menuName = Consts.SCRIPTABLE_OBJECTS.MENU_TEXT.WALL,
    order = Consts.SCRIPTABLE_OBJECTS.MENU_ORDER.WALL
)]

public class WallType : ScriptableObject {
    public GameObject WallPrefab;
    public Vector2 BallStartPos = new Vector2(0, -3.5f);
    public Vector2 BallStartDirection = Vector2.up;
    public float BallSpeed = 5.0f;
    public Vector2 BatStartPos = new Vector2(0, -4.5f);
    public float BatSpeed = 7.0f;
}
