using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(AnimationController))]
public class AnimationControllerEditor : Editor
{
    private SerializedProperty animatorProp;
    private SerializedProperty modifiersProp;

    private void OnEnable()
    {
        animatorProp = serializedObject.FindProperty("animator");
        modifiersProp = serializedObject.FindProperty("animationModifiers");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(animatorProp);

        Animator animator = (animatorProp.objectReferenceValue as Animator);

        if (animator == null)
        {
            EditorGUILayout.HelpBox("Assign an Animator to use the dropdown.", MessageType.Warning);
            serializedObject.ApplyModifiedProperties();
            return;
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Animation Modifiers", EditorStyles.boldLabel);

        for (int i = 0; i < modifiersProp.arraySize; i++)
        {
            SerializedProperty modProp = modifiersProp.GetArrayElementAtIndex(i);
            SerializedProperty paramProp = modProp.FindPropertyRelative("animationParameter");
            SerializedProperty delayProp = modProp.FindPropertyRelative("delay");

            EditorGUILayout.BeginVertical("box");

            // Dropdown of trigger parameters
            string[] triggerNames = GetTriggerParameterNames(animator);
            int currentIndex = Mathf.Max(0, System.Array.IndexOf(triggerNames, paramProp.stringValue));
            int newIndex = EditorGUILayout.Popup("Animation Parameter", currentIndex, triggerNames);
            paramProp.stringValue = triggerNames[newIndex];

            EditorGUILayout.PropertyField(delayProp);

            if (GUILayout.Button("Remove"))
            {
                modifiersProp.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Add Animation Modifier"))
        {
            modifiersProp.InsertArrayElementAtIndex(modifiersProp.arraySize);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private string[] GetTriggerParameterNames(Animator animator)
    {
        var controller = animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
        if (controller == null)
            return new string[] { "None" };

        var list = new List<string>();
        foreach (var param in controller.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
                list.Add(param.name);
        }

        return list.Count > 0 ? list.ToArray() : new string[] { "None" };
    }

}
