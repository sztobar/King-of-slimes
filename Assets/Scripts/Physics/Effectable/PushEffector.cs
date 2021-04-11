using Kite;
using System.Collections;
using UnityEngine;

public class PushEffector : PhysicsEffector<PushEffectable>
{
  [Range(2, 5)] public int requiredStrength = 2;

  public override void AddEffectable(PushEffectable effectable)
  {
    base.AddEffectable(effectable);
    effectable.isPushing = true;
  }
}