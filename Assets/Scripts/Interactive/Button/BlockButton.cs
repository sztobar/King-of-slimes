using System.Collections;
using UnityEngine;

public class BlockButton : MonoBehaviour
{
  public InteractionTrigger trigger;
  public ButtonPressEmitter pressEmitter;
  public SpriteRenderer spriteRenderer;
  private bool isPressed;

  private void OnTriggerEnter2D(Collider2D collider)
  {
    PushBlock block = InteractiveHelpers.GetBlock(collider);
    if (block)
    {
      isPressed = true;
      spriteRenderer.enabled = false;
      pressEmitter.Emit(isPressed);
      //trigger.CallTriggerAction(isPressed);
      AudioSingleton.PlaySound(AudioSingleton.Instance.clips.button);
    }
  }

  private void OnTriggerExit2D(Collider2D collider)
  {
    PushBlock block = InteractiveHelpers.GetBlock(collider);
    if (block)
    {
      isPressed = false;
      spriteRenderer.enabled = true;
      pressEmitter.Emit(isPressed);
      //trigger.CallTriggerAction(isPressed);
      AudioSingleton.PlaySound(AudioSingleton.Instance.clips.button);
    }
  }

  private void OnDrawGizmosSelected()
  {

    if (trigger)
      Gizmos.DrawLine(transform.position, trigger.transform.position);
  }
}
