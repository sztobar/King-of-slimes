using Kite;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitDI : MonoBehaviour
{
  [HideInInspector]
  public PlayerUnitController controller;

  [HideInInspector]
  public PlayerDI mainDi;

  public PlayerSelectable selectable;
  public PlayerBaseStats stats;
  public PlayerPhysics physics;

  public PlayerAbilities abilities;
  public PlayerUnitStateMachine stateMachine;

  #region modules
  public new PlayerUnitCamera camera;
  public BasePlayerAnimator animator;
  public PlayerUnitSFX sfx;
  public PlayerHPModule hp;
  public PlayerPoisonModule poison;
  public PlayerUnitRespawnHandler respawnHandler;
  public new PlayerUnitCollider collider;
  public PlayerDamageModule damage;
  public PlayerVulnerability vulnerability;
  public PlayerUnitAnimationEvents animationEvents;
  public PlayerFallAttackCollider fallAttackCollider;
  #endregion

  [Header("General")]
  public HorizontalFlipComponent flip;
  public BoxCollider2D boxCollider;

  [Header("For assembly")]
  public BaseAssemblyModules assembly;
  public YeetOutModule yeetModule;

  public void Init(PlayerUnitController controller)
  {
    this.controller = controller;
    this.mainDi = controller.mainController.di;
    foreach (IPlayerUnitComponent component in GetPlayerUnitComponents())
      if (component != null)
        component.Inject(this);
  }

  private IPlayerUnitComponent[] GetPlayerUnitComponents() =>
    new IPlayerUnitComponent[]
    {
      selectable,
      stats,
      physics,
      abilities,
      stateMachine,
      camera,
      animator,
      sfx,
      hp,
      poison,
      respawnHandler,
      collider,
      damage,
      vulnerability,
      animationEvents,
      fallAttackCollider,
      assembly,
      yeetModule
    };
}