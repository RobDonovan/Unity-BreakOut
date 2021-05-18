using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = Consts.SCRIPTABLE_OBJECTS.FILE_NAMES.COLLECTABLE_TRIPLE_BALL,
    menuName = Consts.SCRIPTABLE_OBJECTS.MENU_TEXT.COLLECTABLE_TRIPLE_BALL,
    order = Consts.SCRIPTABLE_OBJECTS.MENU_ORDER.COLLECTABLE_TRIPLE_BALL
)]

public class Collectable_TripleBall : Collectable {

    public Collectable_TripleBall() {
        CollectableType = CollectableTypes.TripleBall;
    }
}
