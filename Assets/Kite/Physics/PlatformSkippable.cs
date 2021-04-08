using UnityEngine;
using System.Collections;

namespace Kite
{
  public class PlatformSkippable : MonoBehaviour
  {
    public float skipPlatformTime = 0.1f;

    private float skipPlatformTimeLeft;

    public bool IsStandingOnPlatform
    {
      get;
      set;
    }

    public bool CanSkip
    {
      get => skipPlatformTimeLeft > 0;
      set => skipPlatformTimeLeft = skipPlatformTime;
    }

    private void Update()
    {
      if (skipPlatformTimeLeft > 0)
      {
        skipPlatformTimeLeft -= Time.deltaTime;
      }
    }
  }
}