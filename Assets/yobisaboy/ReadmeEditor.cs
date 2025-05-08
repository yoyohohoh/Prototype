using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Readme))]
public class ReadmeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Readme readme = (Readme)target;

        GUILayout.Label(readme.icon.texture, GUILayout.Width(90), GUILayout.Height(90));

        EditorGUILayout.LabelField("Creator", readme.creator);
        EditorGUILayout.LabelField("Contact", readme.contact);

        GUIStyle wordWrapStyle = new GUIStyle(GUI.skin.label);
        wordWrapStyle.wordWrap = true;
        EditorGUILayout.LabelField("Description");
        GUILayout.Label(readme.description, wordWrapStyle);

    }
}
