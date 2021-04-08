using System;
using UnityEngine;

namespace Kite
{
  public class PhysicsForceData
  {
    public delegate float PhysicsForceEase(float t);
    private static readonly PhysicsForceEase defaultEase = (float t) => t;

    public readonly float duration;
    public readonly PhysicsForceType type;
    public readonly Vector2 value;
    public readonly PhysicsForceEase ease;

    public float elapsedTime = 0;

    public PhysicsForceData(Vector2 value, float duration) : this(value, duration, defaultEase) { }
    public PhysicsForceData(Vector2 value, float duration, PhysicsForceEase ease)
    {
      this.value = value;
      this.duration = duration;
      this.ease = ease;
    }

    public void Update()
    {
      elapsedTime += Time.deltaTime;
    }

    public bool IsOver() => elapsedTime >= duration;

    public Vector2 GetValue()
    {
      float t = elapsedTime / duration;
      float easedT = ease(t);
      return Vector2.Lerp(value, Vector2.zero, easedT);
    }
  }
}