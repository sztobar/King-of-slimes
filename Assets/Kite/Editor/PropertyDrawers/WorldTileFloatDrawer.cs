using Kite;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  [CustomPropertyDrawer(typeof(WorldTileFloat))]
  public class WorldTileFloatDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      Rect valueRect = position;
      valueRect.width *= 0.75f;
      SerializedProperty valueProperty = property.FindPropertyRelative("value");
      EditorGUI.PropertyField(valueRect, valueProperty, label, false);

      Rect typeRect = position;
      typeRect.width *= 0.25f;
      typeRect.x += valueRect.width;
      SerializedProperty typeProperty = property.FindPropertyRelative("type");
      EditorGUI.PropertyField(typeRect, typeProperty, GUIContent.none, false);
      //Rect structPosition = EditorGUI.PrefixLabel(position, label);

      //SerializedProperty valueProperty = property.FindPropertyRelative("value");
      //Rect valuePosition = structPosition;
      //valuePosition.width *= 0.66f;
      //valueProperty.floatValue = EditorGUI.FloatField(valuePosition, valueProperty.floatValue);

      //SerializedProperty typeProperty = property.FindPropertyRelative("type");
      //Rect typePosition = structPosition;
      //typePosition.width *= 0.33f;
      //typePosition.x += valuePosition.width;
      //Array enumValues = Enum.GetValues(typeof(WorldTileFloat.WorldTile));
      //WorldTileFloat.WorldTile newType = (WorldTileFloat.WorldTile)EditorGUI.EnumPopup(typePosition, (WorldTileFloat.WorldTile)enumValues.GetValue(typeProperty.enumValueIndex));
      //typeProperty.enumValueIndex = Array.IndexOf(enumValues, newType);
    }
  }
}