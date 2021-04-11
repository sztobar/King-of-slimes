using Kite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigmanAttackCollider : MonoBehaviour, IPigmanComponent
{

  public EnemyPlayerDetect detect;

  private PigmanStateMachine stateMachine;
  private PigmanPhysics physics;
  private ScriptablePigman data;
  private PigmanController controller;

  public void Inject(PigmanController controller)
  {
    this.controller = controller;
    stateMachine = controller.di.stateMachine;
    data = controller.data;
    physics = controller.di.physics;
    stateMachine.attack.OnIsAttackingChange += OnIsAttackingChange;
    OnIsAttackingChange(false);
  }

  private void OnIsAttackingChange(bool isAttacking)
  {
    enabled = isAttacking;
  }

  private void FixedUpdate()
  {
    List<PlayerUnitController> units = detect.GetAllPlayers();
    for (int i = 0; i < units.Count; i++)
    {
      AttackPlayer(units[i]);
    }
  }

  private void AttackPlayer(PlayerUnitController player)
  {
    PlayerVulnerability vulnerability = player.di.vulnerability;
    Vector2 lookVector = new Vector2(physics.flip.Direction.ToFloat(), 1);
    if (vulnerability.IsGuardingInOpposingDirection(physics.flip.Direction))
    {
      float recoilTileForce;
      if (IsFullAssembly(player))
      {
        stateMachine.attack.StartStagger();
        recoilTileForce = data.guardFullAssemblyRecoilTileForce;
        // TODO: short stagger?
      }
      else
      {
        recoilTileForce = data.guardPartialAssemblyRecoilTileForce;
        Vector2 playerRecoil = RecoilHelpers.GetRecoilFromTo(player.transform, controller.transform, recoilTileForce);
        player.di.stateMachine.SetRecoilState(playerRecoil);
      }
    }
    else if (vulnerability.IsVulnerable())
    {
      PlayerDamageModule damage = player.di.damage;
      Vector2 collisionRecoil = RecoilHelpers.GetRecoilNormalFromTo(player.transform, controller.transform);
      if (IsFullAssembly(player))
      {
        damage.TakeDamage(data.attackDamage, TileHelpers.TileToWorld(data.attackFullAssemblyRecoilTileForce) * collisionRecoil);
      }
      else
      {
        damage.TakeDamage(data.attackDamage, TileHelpers.TileToWorld(data.attackPartialAssemblyRecoilTileForce) * collisionRecoil);
      }
    }
  }

  private bool IsFullAssembly(PlayerUnitController unit) =>
    unit.di.stats.IsFullAssembly;
}