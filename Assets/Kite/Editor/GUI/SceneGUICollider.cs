using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  class SceneGUICollider
  {
    private static readonly float doubleClickTime = 0.5f;
    public static GameObject clickedGameObject;
    public static float lastClickTime;

    public void OnClick(Collider2D collider)
    {
      GameObject gameObject = collider.gameObject;
      if (gameObject == clickedGameObject)
      {
        float currentTime = Time.realtimeSinceStartup;
        if (currentTime - lastClickTime < doubleClickTime)
        {
          AssetDatabase.OpenAsset(gameObject);
        }
      }
      else
      {
        clickedGameObject = gameObject;
        EditorGUIUtility.PingObject(clickedGameObject);
      }
      lastClickTime = Time.realtimeSinceStartup;
    }
  }
}
