using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = Consts.SCRIPTABLE_OBJECTS.FILE_NAMES.WALLS,
    menuName = Consts.SCRIPTABLE_OBJECTS.MENU_TEXT.WALLS,
    order = Consts.SCRIPTABLE_OBJECTS.MENU_ORDER.WALLS
)]

public class Walls : ScriptableObject {
    public WallType[] WallList;
}
