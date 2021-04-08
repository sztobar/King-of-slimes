using Kite;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  public static class KiteSettingsEditor
  {
    public const string path = "Assets/Kite/Resources";

    [InitializeOnLoadMethod]
    public static void EditorInit()
    {
      KiteSettings settings = GetOrCreateSettings();
      Dir4Settings.OnEditorInit(settings);
      DirXSettings.OnEditorInit(settings);
      DirYSettings.OnEditorInit(settings);
      TileHelpers.OnSettings(settings);
    }

    internal static KiteSettings GetOrCreateSettings()
    {
      if (KiteSettings.instance)
        return KiteSettings.instance;

      string assetPath = $"{path}/{KiteSettings.resourceName}.asset";
      KiteSettings settings = AssetDatabase.LoadAssetAtPath<KiteSettings>(assetPath);
      if (settings == null)
      {
        settings = ScriptableObject.CreateInstance<KiteSettings>();
        if (!AssetDatabase.IsValidFolder(path))
        {
          Debug.Log($"{path} is not a valid folder");
          AssetDatabase.CreateFolder("Assets/Kite", "Resources");
        }

        AssetDatabase.CreateAsset(settings, assetPath);
        AssetDatabase.SaveAssets();
      }
      KiteSettings.instance = settings;

      return settings;
    }

    internal static SerializedObject GetSerializedSettings() =>
      new SerializedObject(GetOrCreateSettings());
  }
}