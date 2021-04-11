using System;
using System.Collections;
using Kite;
using UnityEngine;

[ExecuteAlways]
public class SnailRotation : MonoBehaviour, ISnailComponent
{
  public Direction4 initialSurfaceNormal = Direction4.Up;
  public bool forward = true;
  private new Rigidbody2D rigidbody;

  [HideInInspector] public Direction4 dir4;

  public void Inject(SnailController controller)
  {
    rigidbody = controller.di.physics.movement.rigidbody;

    if (forward)
      dir4 = initialSurfaceNormal.RotateClockwise();
    else
      dir4 = initialSurfaceNormal.RotateCounterClockwise();
  }

  internal Vector2 GetFrontVector() => rigidbody.transform.right;

  internal void Reverse()
  {
    forward = !forward;
    rigidbody.transform.rotation = Quaternion.LookRotation(-rigidbody.transform.forward, rigidbody.transform.up);
    dir4 = dir4.Opposite();
  }

  internal void Rotate90()
  {
    if (forward)
      dir4 = dir4.RotateClockwise();
    else
      dir4 = dir4.RotateCounterClockwise();

    transform.Rotate(new Vector3(0, 0, -90));
    Physics2D.SyncTransforms();
  }

  public bool IsHorizontal() => dir4.IsHorizontal();

  internal Vector2 GetDownVector() => -rigidbody.transform.up;

  public Vector2 GetRotatedVector(Vector2 deltaPosition)
  {
    return transform.rotation * deltaPosition;
  }

  private void Update()
  {
    if (!Application.isPlaying)
    {
      EditorUpdate();
    }
  }

  void EditorUpdate()
  {
    Vector3 upwardsVector = initialSurfaceNormal.ToVector3();
    Vector3 forwardVector = Vector3.forward * (forward ? 1 : -1);
    transform.rotation = Quaternion.LookRotation(forwardVector, upwardsVector);
  }
}
