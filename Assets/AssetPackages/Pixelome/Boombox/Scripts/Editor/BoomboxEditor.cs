using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Boombox))]
public class BoomboxEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        Boombox radio = (Boombox)target;
        if (GUILayout.Button(radio.Playing ? "Turn Off" : "Turn On")) {
            radio.Playing = !radio.Playing;
        }
    }
}

