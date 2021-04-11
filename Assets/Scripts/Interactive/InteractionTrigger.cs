using System;
using System.Collections;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
  public delegate void OnTrigger(bool isPressed);
  public event OnTrigger OnTriggerAction;

  private ButtonBase button;

  public void CallTriggerAction(bool isPressed)
  {
    OnTriggerAction(isPressed);
  }

  public bool IsClosing()
  {
    return false;
  }

  private void Awake()
  {
    //button.GetActionFor(this).OnPress += CallTriggerAction;
  }
}