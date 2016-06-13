using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(JSONreader))]
public class SelectTester : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        JSONreader jsonReader = (JSONreader)target;
        if(GUILayout.Button("Query table"))
        {
            jsonReader.FindRows();

        }
        if (GUILayout.Button("Clear Boxes"))
        {
            jsonReader.ClearScene();

        }
    }

}
