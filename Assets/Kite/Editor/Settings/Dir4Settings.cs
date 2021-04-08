using Kite;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  public static class Dir4Settings
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
        settings.upDir4 && Dir4.up &&
        settings.rightDir4 && Dir4.right &&
        settings.downDir4 && Dir4.down &&
        settings.leftDir4 && Dir4.left
      );
    }

    public static void Initialize(KiteSettings settings = null)
    {
      if (!settings)
        settings = KiteSettingsEditor.GetOrCreateSettings();
      SerializedObject serializedObject = KiteSettingsEditor.GetSerializedSettings();
      serializedObject.FindProperty(nameof(settings.upDir4)).objectReferenceValue = InitUpDir4();
      serializedObject.FindProperty(nameof(settings.rightDir4)).objectReferenceValue = InitRightDir4();
      serializedObject.FindProperty(nameof(settings.downDir4)).objectReferenceValue = InitDownDir4();
      serializedObject.FindProperty(nameof(settings.leftDir4)).objectReferenceValue = InitLeftDir4();
      serializedObject.ApplyModifiedProperties();
      Dir4.OnSettings(settings);
    }

    private static Dir4 InitUpDir4()
    {
      string assetPath = $"{KiteSettingsEditor.path}/Dir4Up.asset";
      Dir4 upDir4 = AssetDatabase.LoadAssetAtPath<Dir4>(assetPath);
      if (!upDir4)
      {
        upDir4 = ScriptableObject.CreateInstance<Dir4>();
        upDir4.identifier = "Up";
        upDir4.y = 1;
        AssetDatabase.CreateAsset(upDir4, assetPath);
      }
      return upDir4;
    }

    private static Dir4 InitRightDir4()
    {
      string assetPath = $"{KiteSettingsEditor.path}/Dir4Right.asset";
      Dir4 rightDir4 = AssetDatabase.LoadAssetAtPath<Dir4>(assetPath);
      if (!rightDir4)
      {
        rightDir4 = ScriptableObject.CreateInstance<Dir4>();
        rightDir4.identifier = "Right";
        rightDir4.x = 1;
        AssetDatabase.CreateAsset(rightDir4, assetPath);
      }
      return rightDir4;
    }

    private static Dir4 InitDownDir4()
    {
      string assetPath = $"{KiteSettingsEditor.path}/Dir4Down.asset";
      Dir4 downDir4 = AssetDatabase.LoadAssetAtPath<Dir4>(assetPath);
      if (!downDir4)
      {
        downDir4 = ScriptableObject.CreateInstance<Dir4>();
        downDir4.identifier = "Down";
        downDir4.y = -1;
        AssetDatabase.CreateAsset(downDir4, assetPath);
      }
      return downDir4;
    }

    private static Dir4 InitLeftDir4()
    {
      string assetPath = $"{KiteSettingsEditor.path}/Dir4Left.asset";
      Dir4 leftDir4 = AssetDatabase.LoadAssetAtPath<Dir4>(assetPath);
      if (!leftDir4)
      {
        leftDir4 = ScriptableObject.CreateInstance<Dir4>();
        leftDir4.identifier = "Left";
        leftDir4.x = -1;
        AssetDatabase.CreateAsset(leftDir4, assetPath);
      }
      return leftDir4;
    }
  }
}