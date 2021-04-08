using Kite;
using System.Collections;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace KiteEditor
{
  public static class PingObjectShortcut
  {
    [Shortcut("Ping object", KeyCode.Semicolon)]
    public static void PingObject()
    {
      GameObject selection = Selection.activeGameObject;
      if (selection)
      {
        EditorGUIUtility.PingObject(selection);
      }
    }
  }
}