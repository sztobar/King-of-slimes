using System.Collections;
using UnityEngine;

namespace Kite
{
  public class NormalDirComponent : DirComponent
  {
    public new Rigidbody2D rigidbody;

    [SerializeField] private Dir4 surfaceNormal;
    [SerializeField] private DirX xDirection = DirX.right;

    public override Vector2 Forward =>
      xDirection == DirX.right ? Dir4Rotation.Clockwise(surfaceNormal) : Dir4Rotation.CounterClockwise(surfaceNormal);
    public override Vector2 Backward => -Forward;
    public override Vector2 Above => surfaceNormal;
    public override Vector2 Below => -surfaceNormal;

    public Dir4 SurfaceNormal
    {
      get => surfaceNormal;
      set
      {
        if (surfaceNormal == value)
          return;
        surfaceNormal = value;
        OnValidate();
      }
    }

    public DirX XDirection
    {
      get => xDirection;
      set
      {
        if (xDirection == value)
          return;
        xDirection = value;
        OnValidate();
      }
    }

    private void OnValidate()
    {
      float rotation = Dir4Rotation.GetRotation(surfaceNormal);
      if (rigidbody)
        rigidbody.rotation = rotation;
      transform.localScale = new Vector3(xDirection, 1);
    }
  }
}