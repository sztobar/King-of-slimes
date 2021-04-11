using Kite;
using TMPro;
using UnityEngine;

public class SkipCutsceneText : MonoBehaviour
{
  public TextMeshProUGUI text;

  public Envelope envelope;
  [Tooltip("How long to wait after text starts to show to accept input as skip")]
  public float delayAfterAppearTime;

  private float elapsedTime;

  public void StartShow()
  {
    if (!gameObject.activeSelf)
      gameObject.SetActive(true);

    envelope.Start();
  }

  public void ForceHide()
  {
    envelope.Reset();
    gameObject.SetActive(false);
  }

  public bool IsShowing() {
    switch (envelope.Phase)
    {
      case EnvelopePhase.Attack:
      case EnvelopePhase.Sustain:
        return elapsedTime > delayAfterAppearTime;
    }
    return false;
  }

  void Awake()
  {
    Color color = text.color;
    color.a = 0;
    text.color = color;
    gameObject.SetActive(false);
  }

  void Update()
  {
    float dt = Time.unscaledDeltaTime;
    envelope.Update(dt);
    Color color = text.color;
    color.a = envelope.Value;
    text.color = color;
    if (envelope.Phase == EnvelopePhase.Attack || envelope.Phase == EnvelopePhase.Sustain)
    {
      elapsedTime += dt;
    }
  }
}