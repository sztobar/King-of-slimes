using Kite;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

public class UnlockableDoor : MonoBehaviour, ISerializationCallbackReceiver
{
  public bool lockedAtStart = true;
  public BoolListenerComponent boolListener;
  public InteractionTrigger trigger;
  public DoorOpener doorOpener;

  private void Awake()
  {
    boolListener.Subscribe(OnPress);
    if (!lockedAtStart)
    {
      doorOpener.SetIsOpen(true);
    }
  }

  private void OnPress(bool pressed)
  {
    bool open = GetOpenStateOnPress(pressed);
    doorOpener.StartIsOpen(open);
  }

  public bool GetOpenStateOnPress(bool pressed) => !(lockedAtStart ^ pressed);

  public void OnBeforeSerialize() { }

  public void OnAfterDeserialize()
  {
    if (boolListener)
      boolListener.Subscribe(OnPress);
  }
}
