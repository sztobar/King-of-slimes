using Kite;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  public static class DirYSettings
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
        settings.upDir4 && DirY.up &&
        settings.downDir4 && DirY.down
      );
    }

    public static void Initialize(KiteSettings settings = null)
    {
      if (!settings)
        settings = KiteSettingsEditor.GetOrCreateSettings();
      SerializedObject serializedObject = KiteSettingsEditor.GetSerializedSettings();
      serializedObject.FindProperty(nameof(settings.upDirY)).objectReferenceValue = InitUpDirY();
      serializedObject.FindProperty(nameof(settings.downDirY)).objectReferenceValue = InitDownDirY();
      serializedObject.ApplyModifiedProperties();
      DirY.OnSettings(settings);
    }

    private static DirY InitDownDirY()
    {
      string assetPath = $"{KiteSettingsEditor.path}/DirYDown.asset";
      DirY downDirY = AssetDatabase.LoadAssetAtPath<DirY>(assetPath);
      if (!downDirY)
      {
        downDirY = ScriptableObject.CreateInstance<DirY>();
        downDirY.identifier = "Down";
        downDirY.value = -1;
        AssetDatabase.CreateAsset(downDirY, assetPath);
      }
      return downDirY;
    }

    private static DirY InitUpDirY()
    {
      string assetPath = $"{KiteSettingsEditor.path}/DirYUp.asset";
      DirY upDirY = AssetDatabase.LoadAssetAtPath<DirY>(assetPath);
      if (!upDirY)
      {
        upDirY = ScriptableObject.CreateInstance<DirY>();
        upDirY.identifier = "Up";
        upDirY.value = 1;
        AssetDatabase.CreateAsset(upDirY, assetPath);
      }
      return upDirY;
    }
  }
}