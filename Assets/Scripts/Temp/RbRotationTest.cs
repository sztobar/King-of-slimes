using Kite;
using System.Collections;
using UnityEngine;

public class RbRotationTest : DirComponent
{
  public new Rigidbody2D rigidbody;

  [SerializeField] private Dir4 surfaceNormal;
  [SerializeField] private DirX xDirection = DirX.right;

  public override Vector2 Forward =>
    xDirection == DirX.right ? Dir4Rotation.Clockwise(surfaceNormal) : Dir4Rotation.CounterClockwise(surfaceNormal);
  public override Vector2 Backward => -Forward;
  public override Vector2 Above => surfaceNormal;
  public override Vector2 Below => -Above;

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
    rigidbody.rotation = rotation;
    transform.localScale = new Vector3(xDirection, 1);
    //if (!Application.isPlaying)
    //{
    //  transform.localRotation = Quaternion.Euler(0, 0, rotation);
    //}
  }

  //#if UNITY_EDITOR
  //  [SerializeField, HideInInspector] private Dir4 cachedSurfaceNormal;
  //  [SerializeField, HideInInspector] private DirX ;
  //  private void Update()
  //  {
  //    if (cachedSurfaceNormal != surfaceNormal || cachedIsForward != isForward)
  //    {
  //      OnValidate();
  //      cachedSurfaceNormal = surfaceNormal;
  //      cachedIsForward = isForward;
  //    }
  //  }
  //#endif
}