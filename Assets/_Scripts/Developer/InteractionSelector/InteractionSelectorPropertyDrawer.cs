#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InteractionSelectorAttribute))]
public class InteractionSelectorPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            string[] options = {"Add HP", "Add XP", "Substract HP", "Substract XP"};

            int index = Mathf.Max(0, System.Array.IndexOf(options, property.stringValue));
            index = EditorGUI.Popup(position, label.text, index, options);
            property.stringValue = options[index];
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
#endif
