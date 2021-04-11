using System;
using System.Collections.Generic;
using UnityEngine;

public class SlimeRelict : MonoBehaviour
{
  private static readonly float FULL_ROTATION = 360;

  public new Rigidbody2D rigidbody;
  public Vector2 offset;

  void Awake() =>
    gameObject.SetActive(false);

  public void SpawnAt(Vector2 spawnPosition)
  {
    if (!gameObject.activeSelf)
      gameObject.SetActive(true);

    transform.position = spawnPosition + offset;
    Physics2D.SyncTransforms();
    rigidbody.rotation = 0;
    rigidbody.velocity = Vector2.zero;
    rigidbody.angularVelocity = 0;
  }

  public void SetVelocity(Vector2 relictVelocity) =>
    rigidbody.velocity = relictVelocity;

  public void SetRotation(float rotationsPerSecond) =>
    rigidbody.angularVelocity = rotationsPerSecond * FULL_ROTATION;
}
