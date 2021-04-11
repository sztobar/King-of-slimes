using System;
using System.Collections;
using UnityEngine;

public class PigmanController : MonoBehaviour {

  public ScriptablePigman data;
  public PigmanBehaviourTree behaviorTree;
  public EnemyPhysics physics;
  public PigmanDi di;

  private void Awake() {
    di.Init(this);
    behaviorTree.Inject(this);
  }

  private void FixedUpdate() {
    behaviorTree.Act();
  }

  internal void RemoveGameObject() {
    Destroy(gameObject);
  }
}
