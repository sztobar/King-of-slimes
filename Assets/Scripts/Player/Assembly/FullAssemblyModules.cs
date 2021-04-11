using System.Collections;
using UnityEngine;

public class FullAssemblyModules : BaseAssemblyModules
{
  public PlayerSwordCollider swordCollider;

  public override void Inject(PlayerUnitDI di)
  {
    foreach (IPlayerAssemblyComponent component in GetComponents())
      component.Inject(di);
  }

  private IPlayerAssemblyComponent[] GetComponents() =>
    new IPlayerAssemblyComponent[]
    {
      swordCollider
    };
}