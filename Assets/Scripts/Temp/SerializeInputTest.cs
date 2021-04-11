using Kite;
using System.Collections;
using UnityEngine;

public class SerializeInputTest : MonoBehaviour, ISerializationCallbackReceiver
{
  public CutsceneInput cutsceneInput;
  private bool isPlayMode;

  private void Awake()
  {
    isPlayMode = true;
  }

  private void OnEnable()
  {
    if (cutsceneInput == null)
    {
      Debug.Log("[SerializeInputTest] OnEnable create CutsceneInput");
      cutsceneInput = new CutsceneInput();
    }
    else
    {
      Debug.Log("[SerializeInputTest] OnEnable CutsceneInput is not null");
    }
    cutsceneInput.EnableInputActions();
    cutsceneInput.OnAnyAction += CutsceneInput_OnAnyAction;
    cutsceneInput.OnSkipAction += CutsceneInput_OnConfirmAction;
  }

  private void CutsceneInput_OnConfirmAction()
  {
    Debug.Log("CutsceneInput_OnConfirmAction");
  }

  private void CutsceneInput_OnAnyAction()
  {
    Debug.Log("CutsceneInput_OnAnyAction");
  }

  private void OnDisable()
  {
    if (cutsceneInput != null)
    {
      Debug.Log("[SerializeInputTest] OnDisable disable CutsceneInput");
      cutsceneInput.DisableInputActions();
    }
    else
    {
      Debug.Log("[SerializeInputTest] OnDisable no CutsceneInput");
    }
  }

  public void OnBeforeSerialize()
  {
    Debug.Log("[SerializeInputTest] OnBeforeSerialize");
  }

  public void OnAfterDeserialize()
  {
    Debug.Log("[SerializeInputTest] OnAfterDeserialize");
  }
}