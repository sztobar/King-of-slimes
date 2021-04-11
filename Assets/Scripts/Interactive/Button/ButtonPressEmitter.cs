using Kite;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class ButtonPressEmitter : MonoBehaviour
{
  public BoolEmitterComponent emitter;
  public GameplayCutscene cutscene;

  public void Emit(bool value)
  {
    if (value && cutscene && !cutscene.hasPlayed)
    {
      GameplayManager.instance.fsm.PushCutscene(cutscene);
    }
    emitter.Emit(value);
  }

  public void Update()
  {
    if (Application.isPlaying)
      return;

    if (emitter && emitter.listeners.Count == 0)
    {
      var blockButton = GetComponent<BlockButton>();
      if (blockButton && blockButton.trigger)
      {
        var door = blockButton.trigger.GetComponent<UnlockableDoor>();
        if (door)
        {
          emitter.listeners.Add(door.boolListener);
        }
      }
      var playerButton = GetComponent<PlayerButton>();
      if (playerButton && playerButton.trigger)
      {
        var door = playerButton.trigger.GetComponent<UnlockableDoor>();
        if (door)
        {
          emitter.listeners.Add(door.boolListener);
        }
      }
    }
  }
}
