using UnityEngine;
using UnityEditor;
using Game;

[CustomPropertyDrawer(typeof(NamedSerializeField))]
public class NamedArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        try
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            EditorGUI.PropertyField(rect, property, new GUIContent(((NamedSerializeField)attribute).names[pos]));

        }
        catch
        {
            EditorGUI.PropertyField(rect, property, label);
        }
    }
}
