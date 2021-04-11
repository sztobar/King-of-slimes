using System.Collections;
using UnityEngine;

public class PlayerUnitDeadState : MonoBehaviour, IPlayerUnitState {

  [SerializeField] private float timeToRespawn;

  private PlayerUnitStateMachine stateMachine;
  private PlayerUnitRespawnHandler respawnHandler;
  private PlayerMovementAbility movement;
  private PlayerPhysics physics;
  private PlayerVulnerability vulnerability;

  private PlayerUnitDI di;
  private float timeLeft;

  public void Inject(PlayerUnitDI di) {
    stateMachine = di.stateMachine;
    this.di = di;
    physics = di.physics;
    movement = di.abilities.movement;
    respawnHandler = di.respawnHandler;
    vulnerability = di.vulnerability;
  }

  public void StartState() {
    timeLeft = timeToRespawn;
    di.animator.Hide();
    PlayerUnitController unit = di.controller;
    physics.movement.boxCollider.enabled = false;
    unit.mainController.di.spawnables.OnDeath(unit, physics.velocity.Value);
    physics.velocity.Value = Vector2.zero;
    vulnerability.SetInvulnerable(timeToRespawn);
  }

  public void UpdateState() {
    if (timeLeft <= 0) {
      respawnHandler.Respawn();
      stateMachine.SetDefaultState();
    } else {
      timeLeft -= Time.deltaTime;
      movement.DeadUpdate();
    }
    physics.DeadUpdate();
  }

  public void ExitState() {
    timeLeft = 0;
    di.animator.Show();
    physics.movement.boxCollider.enabled = true;
  }
}
