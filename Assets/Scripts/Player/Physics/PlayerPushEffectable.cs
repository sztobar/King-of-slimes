using Kite;
using System.Collections;
using UnityEngine;

public class PlayerPushEffectable : MonoBehaviour
{
  public float pushVelocityMultiplier;
  public PushEffectable effectable;
  public AudioSource pushAudio;

  public void Init(PlayerBaseStats stats)
  {
    if (stats.OnChange != null)
      stats.OnChange += ReadStats;
    else
      ReadStats(stats);
  }

  private void ReadStats(PlayerBaseStats stats)
  {
    effectable.strength = stats.Strength;
  }
}