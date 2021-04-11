using Kite;
using System.Collections;
using UnityEngine;

public abstract class GameplayState : StackFSMState, IGameplayComponent
{
  public virtual void Inject(GameplayManager gameplayManager) { }
}