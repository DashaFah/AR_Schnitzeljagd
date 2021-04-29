using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(RebuildStage))]

public class MyPropertyDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        //// Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var amountRect = new Rect(position.x, position.y, 50, EditorGUIUtility.singleLineHeight);
        var unitRect = new Rect(position.x + 52, position.y, 120, EditorGUIUtility.singleLineHeight);
        //var property_go_RebuildPart = property.FindPropertyRelative("go_RebuildPart");
        //var arraySize = property_go_RebuildPart.arraySize;
        //var isExpanded = property_go_RebuildPart.isExpanded;
        var nameRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, position.height);


        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("BuildStage"), GUIContent.none);
        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("Requirement"), GUIContent.none);
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("go_RebuildPart"), true);


        //// Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var property_go_RebuildPart = property.FindPropertyRelative("go_RebuildPart");
        var arraySize = property_go_RebuildPart.arraySize;
        var isExpanded = property_go_RebuildPart.isExpanded;

        var realArraySize = isExpanded ? EditorGUIUtility.singleLineHeight * (arraySize + 2) + EditorGUIUtility.singleLineHeight : EditorGUIUtility.singleLineHeight;
        var pre = EditorGUIUtility.singleLineHeight;

        return realArraySize + pre;
    }
}
