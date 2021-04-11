using Kite;
using System;
using System.Collections;
using UnityEngine;

[Serializable]
public struct ButtonPressAction 
{
  public delegate void Event(bool isPressed);
  public event Event OnPress;
  public InteractionTrigger target;
  public bool openOnPress;
}