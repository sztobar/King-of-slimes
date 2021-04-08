using Kite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsForceProfile : ScriptableObject
{
  public List<PhysicsForceConfiguration> configs;

  [Serializable]
  public struct PhysicsForceConfiguration
  {
    public PhysicsForceType type;
    public AnimationCurve curve;
  }
}