using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionPredicate : MonoBehaviour {

  public abstract bool CanInteract(PlayerUnitController controller);
}
