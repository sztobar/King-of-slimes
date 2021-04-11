using Kite;
using System.Collections;
using UnityEngine;

public abstract class ButtonBase : MonoBehaviour
{
  public abstract bool IsPressed();
  public abstract bool IsOpenButton();
  public abstract ButtonPressAction GetActionFor(ButtonPressListener listener);
}