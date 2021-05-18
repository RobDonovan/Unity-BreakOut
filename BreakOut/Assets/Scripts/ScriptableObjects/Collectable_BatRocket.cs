using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = Consts.SCRIPTABLE_OBJECTS.FILE_NAMES.COLLECTABLE_BAT_ROCKET,
    menuName = Consts.SCRIPTABLE_OBJECTS.MENU_TEXT.COLLECTABLE_BAT_ROCKET,
    order = Consts.SCRIPTABLE_OBJECTS.MENU_ORDER.COLLECTABLE_BAT_ROCKET
)]

public class Collectable_BatRocket : Collectable {
    public float RocketSpeed = 500;

    public Collectable_BatRocket() {
        CollectableType = CollectableTypes.BatRocket;
    }
}
