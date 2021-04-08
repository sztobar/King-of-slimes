using Kite;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  public static class EditorHelpers
  {
    public static void CreateDefault(SerializedObject serializedObject)
    {
      SerializedProperty property = serializedObject.GetIterator();
      CreateDefault(property);
    }

    public static void CreateDefault(SerializedProperty property)
    {
      property.NextVisible(true);
      bool firstProperty = true;
      do
      {
        if (firstProperty)
        {
          if (property.propertyPath == "m_Script")
          {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(property);
            EditorGUI.EndDisabledGroup();
          }
          else
          {
            EditorGUILayout.PropertyField(property);
          }
          firstProperty = false;
        }
        else
          EditorGUILayout.PropertyField(property);
      } while (property.NextVisible(false));
    }
  }
}