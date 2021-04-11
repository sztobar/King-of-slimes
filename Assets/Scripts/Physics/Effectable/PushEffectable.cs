using Kite;
using System.Collections;
using UnityEngine;

public class PushEffectable : PhysicsEffectable
{
  public int strength;
  public bool isPushing;
  public bool canPush;

  public override void EffectableReset()
  {
    isPushing = false;
  }
}