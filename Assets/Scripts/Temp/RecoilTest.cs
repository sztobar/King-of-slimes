using Kite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RecoilTest : MonoBehaviour
{
  public AnimationCurve curve;
  public float duration;
  public Vector2 deltaPosition;
  public MoveType type = MoveType.LerpPosition;
  public DifferenceType diffType = DifferenceType.Backward;

  private float elapsedTime;
  private Vector2 destination;
  private Vector2 velocityNormal;
  private float velocityAmount;
  private Vector2 startPosition;

  private void Awake()
  {
    startPosition = transform.position;
    switch (type)
    {
      case MoveType.LerpPosition:
        LerpPositionAwake();
        break;
      case MoveType.LerpDerivedVelocity:
        LerpDerivedVelocityAwake();
        break;
    }
  }

  private void LerpDerivedVelocityAwake()
  {
    velocityNormal = deltaPosition.normalized;
    velocityAmount = deltaPosition.magnitude;
  }

  private void LerpPositionAwake()
  {
    destination = (Vector2)transform.position + deltaPosition;
  }

  private void FixedUpdate()
  {
    switch (type)
    {
      case MoveType.LerpPosition:
        LerpPositionUpdate();
        break;
      case MoveType.LerpDerivedVelocity:
        LerpDerivedVelocityUpdate();
        break;
    }
  }

  private void LerpDerivedVelocityUpdate()
  {
    float dt = Time.deltaTime;
    if (dt == 0)
      return;
    float velocityValue = Derive(elapsedTime, duration, curve);
    Vector2 velocity = velocityValue * velocityNormal * velocityAmount * dt;
    transform.Translate(velocity);
    if (elapsedTime >= duration)
    {
      enabled = false;
      Debug.Log($"{type}.{diffType}: final move: {(Vector2)transform.position - startPosition}");
    }
    else
    {
      elapsedTime += Time.deltaTime;
    }
  }

  private float Derive(float t, float duration, AnimationCurve curve)
  {
    switch (diffType)
    {
      case DifferenceType.Backward:
        return DerivativeHelpers.BackwardDerivative(t, duration, curve.Evaluate);
      case DifferenceType.Forward:
        return DerivativeHelpers.ForwardDerivative(t, duration, curve.Evaluate);
      case DifferenceType.Central:
        return DerivativeHelpers.CentralDerivative(t, duration, curve.Evaluate);
      case DifferenceType.Symmetric:
      default:
        return DerivativeHelpers.SymmetricDerivative(t, duration, curve.Evaluate);
    }
  }

  private void LerpPositionUpdate()
  {
    float t = curve.Evaluate(elapsedTime / duration);
    transform.position = Vector3.Lerp(startPosition, destination, t);
    if (elapsedTime >= duration)
    {
      enabled = false;
      Debug.Log($"{type}: final move: {(Vector2)transform.position - startPosition}");
    }
    else
    {
      elapsedTime += Time.deltaTime;
    }
  }

  public enum MoveType
  {
    LerpPosition,
    LerpDerivedVelocity
  }

  public enum DifferenceType
  {
    Forward,
    Backward,
    Central,
    Symmetric
  }
}
