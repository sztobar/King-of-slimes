using Kite;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  public static class ReverseAnimationClip
  {
    [MenuItem("Assets/Create Reversed Clip", false, 14)]
    private static void ReverseClip()
    {
      string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
      string directoryPath = Path.GetDirectoryName(assetPath);
      string fileName = Path.GetFileName(assetPath);
      string fileExtension = Path.GetExtension(assetPath);
      fileName = fileName.Split('.')[0];
      string copiedFilePath = $"{directoryPath}{Path.DirectorySeparatorChar}{fileName}_Reversed{fileExtension}";
      AssetDatabase.CopyAsset(assetPath, copiedFilePath);

      AnimationClip clip = (AnimationClip)AssetDatabase.LoadAssetAtPath(copiedFilePath, typeof(AnimationClip));

      if (clip == null)
        return;

      AnimationClipHelpers.FlipX(clip);
    }


    [MenuItem("Assets/Create Reversed Clip", true)]
    static bool ReverseClipValidation()
    {
      if (Selection.activeObject)
        return Selection.activeObject.GetType() == typeof(AnimationClip);
      else
        return false;
    }

    public static AnimationClip GetSelectedClip()
    {
      Object[] clips = Selection.GetFiltered(typeof(AnimationClip), SelectionMode.Assets);
      if (clips.Length > 0)
      {
        return clips[0] as AnimationClip;
      }
      return null;
    }
  }
}