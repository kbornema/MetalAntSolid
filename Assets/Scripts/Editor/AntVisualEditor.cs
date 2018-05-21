using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AntVisual))]
public class AntVisualEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var t = (target as AntVisual);


        GUILayout.BeginHorizontal();

        if(GUILayout.Button("FindBodyParts"))
        {
            t.FindBodyTypes();
        }

        if (GUILayout.Button("ApplyColorScheme"))
        {
            t.Colorize();
        }

        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Hide Stuff"))
        {
            t.HideParts(AntBodyPart.EBodyType.Armor);
            t.HideParts(AntBodyPart.EBodyType.Weapon);
            t.EnableArmorLevel(0, false);
        }

        if(GUILayout.Button("Show Stuff"))
        {
            t.ShowParts(AntBodyPart.EBodyType.Armor);
            t.ShowParts(AntBodyPart.EBodyType.Weapon);
            t.EnableArmorLevel(false);
        }

        GUILayout.EndHorizontal();
    }
}
