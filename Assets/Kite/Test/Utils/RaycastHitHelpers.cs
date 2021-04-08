using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

namespace KiteEditTests {
  public class RaycastHitHelpers {

    public static RaycastHit2D MockHit(Collider2D collider, Vector2 point, Vector2 normal, float distance) {
      RaycastHit2D hit = new RaycastHit2D() {
        point = point,
        normal = normal,
        distance = distance
      };
      object hitObject = hit;
      Type type = hitObject.GetType();
      FieldInfo fieldInfo = type.GetField("m_Collider", BindingFlags.NonPublic | BindingFlags.Instance);
      fieldInfo.SetValue(hitObject, collider.GetInstanceID());
      return (RaycastHit2D)hitObject;
    }
  }
}