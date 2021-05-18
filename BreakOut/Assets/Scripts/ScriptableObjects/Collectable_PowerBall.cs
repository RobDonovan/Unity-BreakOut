using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = Consts.SCRIPTABLE_OBJECTS.FILE_NAMES.COLLECTABLE_POWER_BALL,
    menuName = Consts.SCRIPTABLE_OBJECTS.MENU_TEXT.COLLECTABLE_POWER_BALL,
    order = Consts.SCRIPTABLE_OBJECTS.MENU_ORDER.COLLECTABLE_POWER_BALL
)]

public class Collectable_PowerBall : Collectable {
    public Color TheColor = Color.red;

    public Collectable_PowerBall() {
        CollectableType = CollectableTypes.PowerBall;
    }
}
