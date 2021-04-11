using Kite;
using UnityEditor;
using UnityEngine;

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
}
