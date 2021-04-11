using System.Collections;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

  public delegate void OnTrigger(bool unlock);
  public event OnTrigger OnTriggerAction;
}
