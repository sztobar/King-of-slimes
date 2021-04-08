using Kite;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  public static class DirXSettings
  {
    public static void OnEditorInit(KiteSettings settings = null)
    {
      if (!IsInitialized(settings))
        Initialize(settings);
    }

    public static bool IsInitialized(KiteSettings settings = null)
    {
      if (!settings)
        settings = KiteSettingsEditor.GetOrCreateSettings();

      return (
        settings.rightDirX && DirX.right &&
        settings.leftDirX && DirX.left
      );
    }

    public static void Initialize(KiteSettings settings = null)
    {
      if (!settings)
        settings = KiteSettingsEditor.GetOrCreateSettings();
      SerializedObject serializedObject = KiteSettingsEditor.GetSerializedSettings();
      serializedObject.FindProperty(nameof(settings.rightDirX)).objectReferenceValue = InitRightDirX();
      serializedObject.FindProperty(nameof(settings.leftDirX)).objectReferenceValue = InitLeftDirX();
      serializedObject.ApplyModifiedProperties();
      DirX.OnSettings(settings);
    }

    private static DirX InitLeftDirX()
    {
      string assetPath = $"{KiteSettingsEditor.path}/DirXLeft.asset";
      DirX leftDirX = AssetDatabase.LoadAssetAtPath<DirX>(assetPath);
      if (!leftDirX)
      {
        leftDirX = ScriptableObject.CreateInstance<DirX>();
        leftDirX.identifier = "Left";
        leftDirX.value = -1;
        AssetDatabase.CreateAsset(leftDirX, assetPath);
      }
      return leftDirX;
    }

    private static DirX InitRightDirX()
    {
      string assetPath = $"{KiteSettingsEditor.path}/DirXRight.asset";
      DirX rightDirX = AssetDatabase.LoadAssetAtPath<DirX>(assetPath);
      if (!rightDirX)
      {
        rightDirX = ScriptableObject.CreateInstance<DirX>();
        rightDirX.identifier = "Right";
        rightDirX.value = 1;
        AssetDatabase.CreateAsset(rightDirX, assetPath);
      }
      return rightDirX;
    }
  }
}