using System.Collections;
using UnityEngine;

public class SnailController : MonoBehaviour {

  public SnailDi di;
  public SnailBehaviourTree behaviourTree;


  private void Awake() {
    di.Inject(this);
    behaviourTree.Inject(this);
  }

  private void FixedUpdate()
  {
    behaviourTree.Act();
  }
}