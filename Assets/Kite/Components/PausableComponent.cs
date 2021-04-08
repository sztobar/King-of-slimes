using UnityEngine;
using System;
using System.Collections.Generic;

namespace Kite {

  public class PausableComponent : MonoBehaviour {

    public Action OnPauseOn { get; set; } = delegate { };

    public Action OnPauseOff { get; set; } = delegate { };

    private readonly List<MonoBehaviour> disableOnPauseBehaviours = new List<MonoBehaviour>();
    private readonly List<Animator> disableOnPauseAnimators = new List<Animator>();
    private readonly List<ParticleSystem> disableOnPauseParticleSystem = new List<ParticleSystem>();

    public void DisableOnPause(MonoBehaviour behaviour) {
      disableOnPauseBehaviours.Add(behaviour);
    }

    public void DisableOnPause(Animator animator) {
      disableOnPauseAnimators.Add(animator);
    }

    public void DisableOnPause(ParticleSystem particleSystem) {
      disableOnPauseParticleSystem.Add(particleSystem);
    }

    public void HandlePauseOn() {
      for (int i = 0; i < disableOnPauseBehaviours.Count; i++) {
        MonoBehaviour behaviour = disableOnPauseBehaviours[i];
        behaviour.enabled = false;
      }
      for (int i = 0; i < disableOnPauseAnimators.Count; i++) {
        Animator animator = disableOnPauseAnimators[i];
        animator.speed = 0f;
      }
      for (int i = 0; i < disableOnPauseParticleSystem.Count; i++) {
        ParticleSystem particleSystem = disableOnPauseParticleSystem[i];
        if (particleSystem.isPlaying) {
          particleSystem.Pause();
        }
      }
      OnPauseOn();
    }

    public void HandlePauseOff() {
      for (int i = 0; i < disableOnPauseBehaviours.Count; i++) {
        MonoBehaviour behaviour = disableOnPauseBehaviours[i];
        behaviour.enabled = true;
      }
      for (int i = 0; i < disableOnPauseAnimators.Count; i++) {
        Animator animator = disableOnPauseAnimators[i];
        animator.speed = 1f;
      }
      for (int i = 0; i < disableOnPauseParticleSystem.Count; i++) {
        ParticleSystem particleSystem = disableOnPauseParticleSystem[i];
        if (particleSystem.isPaused) {
          particleSystem.Play();
        }
      }
      OnPauseOff();
    }

    private void OnEnable() {
      PauseManager.AddPausable(this);
    }

    private void OnDisable() {
      PauseManager.RemovePausable(this);
    }
  }
}