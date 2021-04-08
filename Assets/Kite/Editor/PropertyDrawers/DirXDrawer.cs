using Kite;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  [CustomPropertyDrawer(typeof(DirX))]
  public class DirXDrawer : PropertyDrawer
  {
    static string buttonName = "RotateTool";
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

      if (!DirXSettings.IsInitialized())
        DirXSettings.Initialize();

      if (!initialized)
        Initialize();

      Rect valueRect = EditorGUI.PrefixLabel(position, new GUIContent(property.displayName));

      DirX[] values = DirX.GetList();
      DirX value = property.objectReferenceValue as DirX;
      if (value != null)
      {
        int currentIndex = Array.IndexOf(values, value);
        if (currentIndex != -1)
        {
          int buttonWidth = 24;
          Vector2 buttonSize = new Vector2(buttonWidth, valueRect.height);
          Rect buttonRect = new Rect(valueRect.position, buttonSize);
          if (GUI.Button(buttonRect, EditorGUIUtility.IconContent("tab_prev", "|Left")))
          {
            property.objectReferenceValue = DirX.left;
          }
          buttonRect = new Rect(valueRect.position + new Vector2(buttonRect.width, 0), buttonSize);
          if (GUI.Button(buttonRect, EditorGUIUtility.IconContent("tab_next", "|Right")))
          {
            property.objectReferenceValue = DirX.right;
          }
          valueRect.x += 2 * buttonWidth;
          valueRect.width -= 2 * buttonWidth;
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
      DirX[] values = DirX.GetList();
      optionLabels = values.Select(dir => dir.identifier).ToArray();
      optionValues = values.Select((_, i) => i).ToArray();
      initialized = true;
    }
  }
}