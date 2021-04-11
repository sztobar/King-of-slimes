using Kite;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;

[ExecuteAlways]
public class SnailTest : MonoBehaviour
{
  public new Rigidbody2D rigidbody;
  public float tileMoveAmount;
  public bool forward;

  [Button]
  public void Rotate90()
  {
    float sign = forward ? -1 : 1;
    //rigidbody.rotation += sign * 90f;
    rigidbody.MoveRotation(rigidbody.rotation + (sign * 90f));
    //rigidbody.angularVelocity = 0f;
    //rigidbody.AddTorque(sign * 90f, ForceMode2D.Impulse);
    //rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 0, sign * 90)));
  }

  void FixedUpdate()
  {
    Vector2 moveAmount = transform.right * TileHelpers.TileToWorld(tileMoveAmount) * Time.deltaTime;
    rigidbody.position = rigidbody.position + moveAmount;
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
    Vector3 forwardVector = Vector3.forward * (forward ? 1 : -1);
    transform.rotation = Quaternion.LookRotation(forwardVector, Vector3.up);
  }
}