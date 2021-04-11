using System.Collections;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour, IPlayerUnitComponent
{

  public PlayerJumpAbility jump;
  public PlayerMovementAbility movement;
  public PlayerWallSlideAbility wallSlide;
  public PlayerYeetAbility yeet;
  public PlayerSelectUnitAbility selectUnit;
  public BasePlayerActionAbility action;

  public void Inject(PlayerUnitDI di)
  {
    foreach (IPlayerAbility ability in GetAbilities())
      ability.Inject(di);
  }

  private IPlayerAbility[] GetAbilities() =>
    new IPlayerAbility[]
    {
      jump,
      movement,
      wallSlide,
      yeet,
      selectUnit,
      action
    };
}
