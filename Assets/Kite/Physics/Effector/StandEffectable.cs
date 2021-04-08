using System.Collections;
using UnityEngine;

namespace Kite
{
  public class StandEffectable : PhysicsEffectable
  {
    public float skipPlatformTime;
    public bool isOnPlatform;
    public bool isGrounded;

    private float elapsedTime;

    public bool CanSkipPlatform() => enabled;

    public override void EffectableReset()
    {
      isOnPlatform = false;
      isGrounded = false;
    }

    public void SkipPlatform()
    {
      enabled = true;
      elapsedTime = 0;
    }

    private void Awake() => enabled = false;

    private void Update()
    {
      elapsedTime += Time.deltaTime;
      if (elapsedTime >= skipPlatformTime)
      {
        elapsedTime = 0;
        enabled = false;
      }
    }
  }
}