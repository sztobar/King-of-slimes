using Kite;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  public static class SelectionIdentifier
  {
    [MenuItem("Assets/Identify")]
    public static void IdentifySelection()
    {
      Debug.Log(Selection.activeObject);
    }
  }
}