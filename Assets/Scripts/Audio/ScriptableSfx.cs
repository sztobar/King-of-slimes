using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ScriptableSFX")]
public class ScriptableSfx : ScriptableObject
{
  [Header("Player")]
  public AudioClip merge;
  public AudioClip yeet;
  public AudioClip playerHit;
  public AudioClip swordSwing;
  public AudioClip jump;
  public AudioClip guard;
  public AudioClip poison;
  public AudioClip move;
  public AudioClip slimeSwitch;

  [Header("Pigman")]
  public AudioClip roar;
  public AudioClip axe;
  public AudioClip parry;
  public AudioClip stomp;
  public AudioClip pigmanHit;

  [Header("cucumber")]
  public AudioClip puff;

  [Header("other")]
  public AudioClip moveBlock;
  public AudioClip button;

  public AudioClip checkpoint;
  public AudioClip tutorial;
  public AudioClip crown;
  public AudioClip success;
}
