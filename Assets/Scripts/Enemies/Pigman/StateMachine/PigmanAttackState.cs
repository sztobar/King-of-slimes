using System;
using System.Collections;
using UnityEngine;
using Bt = BtStatus;

public class PigmanAttackState : MonoBehaviour, IPigmanState
{

  public PigmanAttackPhase phase = PigmanAttackPhase.None;

  private ScriptablePigman data;
  private PigmanAnimator animator;
  private PigmanAnimationEvents animationEvents;

  private float timeToAttackLeft = 0;
  private float parryTimeLeft = 0;
  private float staggerTimeLeft = 0;

  public Action<bool> OnIsAttackingChange { get; set; } = delegate { };

  public void StateStart()
  {
    if (phase == PigmanAttackPhase.None)
    {
      PrepareForAttack();
    }
    else if (phase == PigmanAttackPhase.Prepare)
    {
      animator.SetState(PigmanAnimatorState.Idle);
    }
    animationEvents.OnAttackEnd += OnAttackEnd;
    animationEvents.OnAttackStart += OnAttackStart;
  }

  private void PrepareForAttack()
  {
    timeToAttackLeft = RandomRange.FromVector(data.prepareTimeToAttack);
    animator.SetState(PigmanAnimatorState.Idle);
    phase = PigmanAttackPhase.Prepare;
  }

  public Bt StateUpdate()
  {
    switch (phase)
    {
      case PigmanAttackPhase.Prepare:
        return PrepareForAttackUpdate();
      case PigmanAttackPhase.Parry:
        return ParryUpdate();
      case PigmanAttackPhase.Stagger:
        return StaggerUpdate();
    }
    return Bt.Running;
  }

  private Bt StaggerUpdate()
  {
    if (staggerTimeLeft <= 0)
    {
      PrepareForAttack();
    }
    else
    {
      staggerTimeLeft -= Time.deltaTime;
    }
    return Bt.Running;
  }

  private Bt ParryUpdate()
  {
    if (parryTimeLeft <= 0)
    {
      PrepareForAttack();
    }
    else
    {
      parryTimeLeft -= Time.deltaTime;
    }
    return Bt.Running;
  }

  private Bt PrepareForAttackUpdate()
  {
    if (timeToAttackLeft <= 0)
    {
      phase = PigmanAttackPhase.Attack;
      animator.SetState(PigmanAnimatorState.Attack);
    }
    else
    {
      timeToAttackLeft -= Time.deltaTime;
    }
    return Bt.Running;
  }

  public void OnAttackStart()
  {
    OnIsAttackingChange(true);
    AudioSingleton.PlaySound(AudioSingleton.Instance.clips.axe);
  }

  internal void OnAttackEnd()
  {
    PrepareForAttack();
    OnIsAttackingChange(false);
  }

  public void StartParry()
  {
    OnAttackInterrupt();
    animator.SetState(PigmanAnimatorState.Parry);
    phase = PigmanAttackPhase.Parry;
    parryTimeLeft = RandomRange.FromVector(data.parryTime);
    AudioSingleton.PlaySound(AudioSingleton.Instance.clips.parry);
  }

  private void OnAttackInterrupt()
  {
    if (phase == PigmanAttackPhase.Attack)
    {
      OnIsAttackingChange(false);
    }
  }

  internal void OnParryEnd()
  {
    PrepareForAttack();
  }

  public void StartStagger()
  {
    OnAttackInterrupt();
    animator.SetState(PigmanAnimatorState.Stagger);
    phase = PigmanAttackPhase.Stagger;
    staggerTimeLeft = RandomRange.FromVector(data.staggerTime);
  }

  internal void OnStaggerEnd()
  {
    PrepareForAttack();
  }

  public void Inject(PigmanController controller)
  {
    animator = controller.di.animator;
    animationEvents = controller.di.animationEvents;
    data = controller.data;
  }

  public void StateExit()
  {
    animationEvents.OnAttackEnd -= OnAttackEnd;
    if (phase != PigmanAttackPhase.Prepare)
    {
      // reset prepare timer :/
      phase = PigmanAttackPhase.None;
    }
  }

  public bool CanMove() =>
    phase == PigmanAttackPhase.None ||
    phase == PigmanAttackPhase.Prepare;
}
