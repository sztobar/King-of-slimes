using System;
using UnityEngine;

public class PlayerUnitAnimationEvents : MonoBehaviour, IPlayerUnitComponent
{
  public event Action OnSwordEnded = () => {};
  
  public void EmitSwordEnded() {
    OnSwordEnded();
  }

  public void Inject(PlayerUnitDI di) {
  }

}
