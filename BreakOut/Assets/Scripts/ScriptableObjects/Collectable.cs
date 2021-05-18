using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : ScriptableObject {
    public enum CollectableTypes { PowerBall, TripleBall, BatRocket };

    [HideInInspector]
    public CollectableTypes CollectableType { get; set; }

    public Sprite CollectableIconSprite;
    public Sprite CollectableSprite;
    public float CollectableDuration = 3.0f;
    public float CollectableDropSpeed = 150;
}
