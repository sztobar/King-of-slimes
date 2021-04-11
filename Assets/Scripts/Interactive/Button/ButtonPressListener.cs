using Kite;
using System.Collections;
using UnityEngine;

public class ButtonPressListener : MonoBehaviour
{
  public event ButtonPressAction.Event OnTriggerAction;

  private ButtonBase button;

  public bool IsClosing()
  {
    return button.GetActionFor(this).openOnPress;
  }

  private void Awake()
  {
    button.GetActionFor(this).OnPress += OnTriggerAction;
  }
}