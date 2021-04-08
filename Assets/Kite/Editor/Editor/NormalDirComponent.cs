using Kite;
using System.Collections;
using UnityEngine;
using UnityEditor;

namespace KiteEditor
{
  [CustomEditor(typeof(NormalDirComponent))]
  public class NormalDirComponentEditor : Editor
  {
    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();
      NormalDirComponent component = target as NormalDirComponent;
      float rotation = Dir4Rotation.GetRotation(component.SurfaceNormal);
      component.transform.localRotation = Quaternion.Euler(0, 0, rotation);
    }
  }
}