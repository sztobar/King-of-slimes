using Kite;
using System.Collections.Generic;
using UnityEngine;

public class ManualPigmanAnimatorStateInfo : IAnimatorStateInfo<ManualPigmanAnimatorStateInfo.State>
{
  public void PlayState(State state)
  {

  }

  public int GetHash(State state)
  {
    return (int)state;
  }


  public enum State
  {
    Default,
    Idle,
    Walk,
    Attack,
    Battlecry,
    Sausage,
    Rise,
    Run,
    Defence,
    Stunned,
    Damage,
    Death
  }
}