using Kite;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  [CustomPropertyDrawer(typeof(DirY))]
  public class DirYDrawer : PropertyDrawer
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

      if (!DirYSettings.IsInitialized())
        DirYSettings.Initialize();

      if (!initialized)
        Initialize();

      Rect valuePosition = EditorGUI.PrefixLabel(position, new GUIContent(property.displayName));

      DirY[] values = DirY.GetList();
      DirY value = property.objectReferenceValue as DirY;
      if (value != null)
      {
        int currentIndex = Array.IndexOf(values, value);
        if (currentIndex != -1)
        {
          EditorGUI.BeginChangeCheck();
          int selectedIndex = EditorGUI.IntPopup(valuePosition, currentIndex, optionLabels, optionValues);
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
      DirY[] values = DirY.GetList();
      optionLabels = values.Select(dir => dir.identifier).ToArray();
      optionValues = values.Select((_, i) => i).ToArray();
      initialized = true;
    }
  }
}