using System;
using System.Collections;
using UnityEngine;

public class SnailBehaviourTree : MonoBehaviour, ISnailComponent {

  private SnailPhysics physics;
  public void Inject(SnailController controller) {
    physics = controller.di.physics;
  }

  internal void Act() {
    physics.PhysicsUpdate();
  }
}
