using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BoxProperty))]
public class BoxTester : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BoxProperty boxProperty = (BoxProperty)target;
        if(GUILayout.Button("go to moneytree"))
        {
            boxProperty.SizeChanger();
        }
        if (GUILayout.Button("make babies"))
        {
            boxProperty.AccountBoxSpawner();
        }
        if (GUILayout.Button("Make a special one"))
        {
            boxProperty.ManualBoxSpawn();
        }
        if (GUILayout.Button("Query Table"))
        {
            boxProperty.QueryTable();
        }
    }
}
