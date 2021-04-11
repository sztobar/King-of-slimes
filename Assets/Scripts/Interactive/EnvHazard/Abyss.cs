using Kite;
using System.Collections;
using UnityEngine;

public class Abyss : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
  {
    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collision);
    if (!unit)
    {
      return;
    }
    unit.di.damage.TakeFullDamage(Vector2.zero);
  }
}