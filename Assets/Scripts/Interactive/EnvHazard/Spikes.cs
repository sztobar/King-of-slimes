using Kite;
using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour {

  [SerializeField] private Vector2 pushTileForce; 

  private void OnTriggerStay2D(Collider2D collision) {
    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collision);
    if (!unit) {
      return;
    }
    if (unit.di.vulnerability.IsVulnerable()) {
      float randomSign = Random.Range(0, 2) == 1 ? 1 : -1;
      Vector2 randomPushForce = TileHelpers.TileToWorld(new Vector2(randomSign * pushTileForce.x, pushTileForce.y));
      unit.di.damage.TakeFullDamage(randomPushForce);
    }
  }
}