using System.Collections;
using UnityEngine;

namespace Kite
{
  public class PlatformEffector : PhysicsEffector<StandEffectable>
  {
    public override void AddEffectable(StandEffectable effectable)
    {
      effectable.isOnPlatform = true;
      //PlatformSkippable platformSkippable = effectable.GetComponent<PlatformSkippable>();
      //if (platformSkippable)
      //  platformSkippable.IsStandingOnPlatform = true;

      base.AddEffectable(effectable);
    }
  }
}