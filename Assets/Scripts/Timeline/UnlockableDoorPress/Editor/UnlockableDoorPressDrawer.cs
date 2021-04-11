using KiteEditor;
using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(UnlockableDoorPressBehaviour))]
//public class UnlockableDoorPressDrawer : PropertyDrawer
//{
//  private static readonly float helpBoxHeight = 24;
//  public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//  {
//    SerializedProperty unlockTimeProperty = property.FindPropertyRelative(nameof(UnlockableDoorPressBehaviour.unlockTime));
//    var unlockTimeValue = GetUnlockTimeValue(unlockTimeProperty);
//    bool isFixedTime = unlockTimeValue == UnlockableDoorPressBehaviour.UnlockTime.Fixed;
//    int fieldCount = isFixedTime ? 2 : 1;
//    return (fieldCount * EditorGUIUtility.singleLineHeight) + helpBoxHeight;
//  }

//  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//  {
//    Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
//    SerializedProperty unlockTimeProperty = property.FindPropertyRelative(nameof(UnlockableDoorPressBehaviour.unlockTime));
//    EditorGUI.PropertyField(singleFieldRect, unlockTimeProperty);
//    var unlockTimeValue = GetUnlockTimeValue(unlockTimeProperty);
//    if (unlockTimeValue == UnlockableDoorPressBehaviour.UnlockTime.Default)
//    {
//      singleFieldRect.y += helpBoxHeight;
//      EditorGUI.HelpBox(singleFieldRect, "Door will open in its own time", MessageType.Info);
//    }
//    else if (unlockTimeValue == UnlockableDoorPressBehaviour.UnlockTime.ClipDuration)
//    {
//      singleFieldRect.y += helpBoxHeight;
//      EditorGUI.HelpBox(singleFieldRect, "Door will be opened in whole clip time", MessageType.Info);
//    }
//    else
//    {
//      singleFieldRect.y += helpBoxHeight;
//      EditorGUI.HelpBox(singleFieldRect, "Door will be opened in fixed time", MessageType.Info);
//      singleFieldRect.y += EditorGUIUtility.singleLineHeight;
//      SerializedProperty fixedTimeProperty = property.FindPropertyRelative(nameof(UnlockableDoorPressBehaviour.fixedTime));
//      EditorGUI.PropertyField(singleFieldRect, fixedTimeProperty);
//    }
//  }

//  private UnlockableDoorPressBehaviour.UnlockTime GetUnlockTimeValue(SerializedProperty unlockTimeProperty)
//  {
//    int unlockTimeIndex = unlockTimeProperty.enumValueIndex;
//    var values = System.Enum.GetValues(typeof(UnlockableDoorPressBehaviour.UnlockTime));
//    return (UnlockableDoorPressBehaviour.UnlockTime)values.GetValue(unlockTimeIndex);
//  }
//}
