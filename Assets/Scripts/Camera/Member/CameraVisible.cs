using Kite;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CameraVisible : MonoBehaviour
{
  [HideInInspector] public bool inSight;

  public void OnBecameVisible() => inSight = true;
  public void OnBecameInvisible() => inSight = false;
}