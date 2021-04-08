using Kite;
using System.Collections;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace KiteEditor
{
  public static class UIToolkitHelpers
  {
    public static VisualElement CreateDefault(SerializedObject serializedObject)
    {
      VisualElement container = new VisualElement();

      SerializedProperty iterator = serializedObject.GetIterator();
      VisualElement propertyField;
      if (iterator.NextVisible(true))
      {
        do
        {
          if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
          {
            propertyField = CreateScriptReadonlyField(iterator);
          }
          else
          {
            propertyField = new PropertyField(iterator.Copy()) { name = $"PropertyField:{iterator.propertyPath}" };
          }
          container.Add(propertyField);

        }
        while (iterator.NextVisible(false));
      }

      return container;
    }

    public static VisualElement ReorderableList(SerializedObject serializedObject, string propertyPath) =>
      new IMGUIContainer(() => {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty(propertyPath));
        serializedObject.ApplyModifiedProperties();
      });


    private static VisualElement CreateScriptReadonlyField(SerializedProperty property)
    {
      VisualElement propertyField = new VisualElement() { name = $"PropertyField:{property.propertyPath}" };
      ObjectField objectField = new ObjectField("Script") { name = "unity-input-m_Script" };
      objectField.BindProperty(property);
      propertyField.Add(objectField);
      propertyField.Q(null, "unity-object-field__selector")?.SetEnabled(false);
      propertyField.Q(null, "unity-base-field__label")?.AddToClassList("unity-disabled");
      propertyField.Q(null, "unity-base-field__input")?.AddToClassList("unity-disabled");
      return propertyField;
    }
  }
}