using Kite;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  [CustomPropertyDrawer(typeof(Dir4))]
  public class Dir4Drawer : PropertyDrawer
  {
    static private int[] optionValues;
    static private string[] optionLabels;
    static private bool initialized;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      bool isSettings = property.serializedObject.targetObject is KiteSettings;
      if (isSettings)
      {
        EditorGUI.PropertyField(position, property, label);
        return;
      }

      if (!Dir4Settings.IsInitialized())
        Dir4Settings.Initialize();

      if (!initialized)
        Initialize();

      Rect valueRect = EditorGUI.PrefixLabel(position, new GUIContent(property.displayName));

      Dir4[] values = Dir4.GetList();
      Dir4 value = property.objectReferenceValue as Dir4;
      if (value != null)
      {
        int currentIndex = Array.IndexOf(values, value);
        if (currentIndex != -1)
        {
          int buttonWidth = 24;
          Vector2 buttonSize = new Vector2(buttonWidth, valueRect.height);
          Rect buttonRect = new Rect(valueRect.position, buttonSize);
          if (GUI.Button(buttonRect, EditorGUIUtility.IconContent("d_Refresh", "|Rotate")))
          {
            property.objectReferenceValue = Dir4Rotation.Clockwise(value);
          }
          valueRect.x += buttonWidth;
          valueRect.width -= buttonWidth;
          EditorGUI.BeginChangeCheck();
          int selectedIndex = EditorGUI.IntPopup(valueRect, currentIndex, optionLabels, optionValues);
          if (EditorGUI.EndChangeCheck())
          {
            property.objectReferenceValue = values[selectedIndex];
          }
          return;
        }
      }
      property.objectReferenceValue = values[0];
    }

    private void Initialize()
    {
      Dir4[] values = Dir4.GetList();
      optionLabels = values.Select(dir => dir.identifier).ToArray();
      optionValues = values.Select((_, i) => i).ToArray();
      initialized = true;
    }
  }
}