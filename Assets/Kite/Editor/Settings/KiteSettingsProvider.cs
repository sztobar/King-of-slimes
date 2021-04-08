using Kite;
using System.Collections.Generic;
using UnityEditor;

namespace KiteEditor
{
  public class KiteSettingsProvider
  {
    private const string settingsPath = "Project/KiteSettingsProvider";
    private const string settingsLabel = "Kite";

    [SettingsProvider]
    public static SettingsProvider CreateKiteSettingsProvider()
    {
      SettingsProvider provider = new SettingsProvider(settingsPath, SettingsScope.Project)
      {
        label = settingsLabel,
        guiHandler = (searchContext) =>
        {
          SerializedObject serializedObject = KiteSettingsEditor.GetSerializedSettings();
          EditorHelpers.CreateDefault(serializedObject);
          serializedObject.ApplyModifiedPropertiesWithoutUndo();
        },
        keywords = new HashSet<string>(new[] { "Kite", "tileSize" })
      };

      return provider;
    }
  }
}