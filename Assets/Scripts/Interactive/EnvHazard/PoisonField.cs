using System.Collections;
using UnityEngine;

  public class PoisonField : MonoBehaviour {

  [SerializeField] private float intensity = 1f;

  public float Intensity => intensity;

  private void OnTriggerEnter2D(Collider2D collision) {
    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collision);
    if (!unit) {
      return;
    }
    unit.di.poison.AddPoisonField(this);
  }

  private void OnTriggerExit2D(Collider2D collision) {
    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collision);
    if (!unit) {
      return;
    }
    unit.di.poison.RemovePoisonField(this);
  }
}
