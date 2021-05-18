using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = Consts.SCRIPTABLE_OBJECTS.FILE_NAMES.BRICK,
    menuName = Consts.SCRIPTABLE_OBJECTS.MENU_TEXT.BRICK,
    order = Consts.SCRIPTABLE_OBJECTS.MENU_ORDER.BRICK
)]

public class BrickType : ScriptableObject {
    public Sprite BrickSprite;
    public int BrickHitsToBreak = 1;
}