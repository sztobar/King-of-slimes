using Kite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
  public ButtonPressEmitter pressEmitter;
  public InteractionTrigger trigger;
  public SpriteRenderer spriteRenderer;
  public SlimeInteractionPredicate interactionPredicate;
  public bool needsConstantWeight;

  private readonly HashSet<PlayerUnitController> pressing = new HashSet<PlayerUnitController>();
  private bool isPressed;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collision);
    if (unit && interactionPredicate.CanInteract(unit))
    {
      if (!isPressed)
      {
        isPressed = true;
        //trigger.CallTriggerAction(isPressed);
        pressEmitter.Emit(isPressed);
        AudioSingleton.PlaySound(AudioSingleton.Instance.clips.button);
      }
      pressing.Add(unit);
      spriteRenderer.enabled = false;
      if (unit.di.stats.OnChange != null)
      {
        unit.di.stats.OnChange += OnPressingAssemblyStatsChange;
      }
    }
  }

  private void OnPressingAssemblyStatsChange(PlayerBaseStats stats)
  {
    foreach (PlayerUnitController unit in new List<PlayerUnitController>(pressing))
    {
      if (unit.di.stats.IsAssembly)
      {
        if (!interactionPredicate.CanInteract(unit))
        {
          RemovePressingUnit(unit);
        }
      }
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collision);
    if (unit && interactionPredicate.CanInteract(unit))
    {
      RemovePressingUnit(unit);
    }
  }

  private void RemovePressingUnit(PlayerUnitController unit)
  {
    pressing.Remove(unit);
    if (pressing.Count == 0 && needsConstantWeight)
    {
      AudioSingleton.PlaySound(AudioSingleton.Instance.clips.button);
      isPressed = false;
      spriteRenderer.enabled = true;
      //trigger.CallTriggerAction(isPressed);
      pressEmitter.Emit(isPressed);
    }
    if (unit.di.stats.OnChange != null)
    {
      unit.di.stats.OnChange -= OnPressingAssemblyStatsChange;
    }
  }
}
