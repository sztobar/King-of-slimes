using System.Collections;
using UnityEngine;

public class PlayerSwordCollider : MonoBehaviour, IPlayerAssemblyComponent
{
  [SerializeField] private BoxCollider2D boxCollider;

  private PlayerUnitController controller;
  private BasePlayerActionAbility action;

  public void Inject(PlayerUnitDI di) {
    controller = di.controller;
    action = di.abilities.action;
    action.OnIsAttackingChange += OnIsAttackingChange;
    OnIsAttackingChange(false);
  }

  private void OnIsAttackingChange(bool isAttacking) {
    boxCollider.enabled = isAttacking;
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    EnemyDamagable damagable = InteractiveHelpers.GetEnemy(collision);
    if (damagable) {
      damagable.TakeDamageFrom(boxCollider, controller);
      if (damagable.HasRecoilAfterDamage())
      {
        controller.di.stateMachine.SetRecoilState(damagable.GetRecoilAfterDamage());
      }
    }
  }
}
