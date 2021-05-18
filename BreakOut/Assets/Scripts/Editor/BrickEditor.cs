using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Brick))]
[CanEditMultipleObjects]
public class BrickEditor : Editor {
    // A little editor script to allow walls to be designed in the editor scene window, so that any changes update visually.

    SerializedProperty brickTypeProp;
    SerializedProperty brickCollectableProp;

    private void OnEnable() {
        brickTypeProp = serializedObject.FindProperty(Consts.EDITOR.PROPERTIES.BRICK_TYPE);
        brickCollectableProp = serializedObject.FindProperty(Consts.EDITOR.PROPERTIES.COLLECTABLE_TYPE);
    }

    public override void OnInspectorGUI() {
        // Use Serialization/PropertyField so that multi selection works.

        serializedObject.Update();

        // Brick Type
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(brickTypeProp, new GUIContent(Consts.EDITOR.LABELS.BRICK_TYPE));
        
        if (EditorGUI.EndChangeCheck()) {
            BrickType bt = (BrickType)brickTypeProp.objectReferenceValue;

            // Update the brick icon sprite to match the type for all selected bricks
            for (int o=0;o < serializedObject.targetObjects.Length;o++) {
                Brick b = (Brick)serializedObject.targetObjects[o];

                // Add to undo list
                Undo.RecordObjects(new Object[] { b.GetComponent<SpriteRenderer>(), b.GetComponent<Brick>().BrickPhysical }, "Change Brick Type");

                if (bt) {        
                    b.GetComponent<SpriteRenderer>().sprite = bt.BrickSprite;

                    // Indestructible bricks have a different layer, so that they don't get effected by the PowerBall mod when we 'ignore' collisions
                    if (bt.BrickHitsToBreak == 0) {
                        b.GetComponent<Brick>().BrickPhysical.layer = LayerMask.NameToLayer(Consts.LAYERS.BRICKS);
                    }
                    else {
                        b.GetComponent<Brick>().BrickPhysical.layer = LayerMask.NameToLayer(Consts.LAYERS.BRICKS_PHYSICS);
                    }
                }
                else {
                    b.GetComponent<SpriteRenderer>().sprite = null;
                }
            }
        }

        // Brick Collectable
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(brickCollectableProp, new GUIContent(Consts.EDITOR.LABELS.COLLECTABLE_TYPE));

        if (EditorGUI.EndChangeCheck()) {
            BrickType bt = (BrickType)brickTypeProp.objectReferenceValue;
            Collectable col = (Collectable)brickCollectableProp.objectReferenceValue;

            // Update the brick collectable icon sprite to match the type for all selected bricks
            for (int o = 0; o < serializedObject.targetObjects.Length; o++) {
                Brick b = (Brick)serializedObject.targetObjects[o];

                // Add to undo list
                Undo.RecordObjects(new Object[] { b.CollectableIcon, b.CollectableIcon.GetComponent<SpriteRenderer>(), }, "Change Brick Collectable");

                if (col) {    
                    b.CollectableIcon.GetComponent<SpriteRenderer>().sprite = col.CollectableIconSprite;
                    b.CollectableIcon.SetActive(true);
                }
                else {
                    b.CollectableIcon.GetComponent<SpriteRenderer>().sprite = null;
                    b.CollectableIcon.SetActive(false);
                }
            }
        }

        EditorGUILayout.Separator();

        // Draw any other 'default' fields
        DrawPropertiesExcluding(serializedObject,new string[]  {
            Consts.EDITOR.PROPERTIES.SCRIPT,
            Consts.EDITOR.PROPERTIES.BRICK_TYPE,
            Consts.EDITOR.PROPERTIES.COLLECTABLE_TYPE
        });

        serializedObject.ApplyModifiedProperties();
    }
}
