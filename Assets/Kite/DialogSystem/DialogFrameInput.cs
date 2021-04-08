using UnityEngine;
using System.Collections;

public abstract class DialogFrameBaseInput
{
  public abstract bool ConfirmHeld { get; }
  public abstract bool ConfirmPressed { get; }
  public abstract bool CancelPressed { get; }
}
