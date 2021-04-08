using Kite;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System;

namespace KiteEditor
{
  [CustomPropertyDrawer(typeof(AnimationCurve))]
  public class AnimationCurveDrawer : PropertyDrawer
  {
    private readonly int buttonWidth = 24;
    private readonly int buttonMargin = 2;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      if (property.propertyType == SerializedPropertyType.AnimationCurve)
      {
        position.width -= buttonWidth + buttonMargin;
        EditorGUI.PropertyField(position, property, label);

        position.x += position.width + buttonMargin;
        position.width = buttonWidth;
        CurveActionsButtonGUI(position, property);
      }
    }

    private void CurveActionsButtonGUI(Rect rect, SerializedProperty property)
    {
      if (GUI.Button(rect, EditorGUIUtility.IconContent("_Menu", "|Actions")))
      {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Flip X"), false, Action_FlipX, property);
        menu.AddItem(new GUIContent("Flip Y"), false, Action_FlipY, property);
        menu.ShowAsContext();
      }
    }

    private void Action_FlipX(object context)
    {
      if (context is SerializedProperty property)
      {
        AnimationCurve curve = property.animationCurveValue;
        CurveHelpers.FlipX(curve);
        property.animationCurveValue = curve;
        property.serializedObject.ApplyModifiedProperties();
      }
    }

    private void Action_FlipY(object context)
    {
      if (context is SerializedProperty property)
      {
        AnimationCurve curve = property.animationCurveValue;
        CurveHelpers.FlipY(curve);
        property.animationCurveValue = curve;
        property.serializedObject.ApplyModifiedProperties();
      }
    }
  }
}