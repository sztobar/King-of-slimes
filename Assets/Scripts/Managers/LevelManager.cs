using System.Collections;
using UnityEngine;

  public class LevelManager : MonoBehaviour
  {
  public AudioSource source;
  public bool playOnCollide;

  private void Awake()
  {
    
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collision);
    if (unit)
    {
      source.Play();
    }
  }
}
