using Kite;
using System;
using System.Collections;
using UnityEngine;

public class EnemyGuardable : MonoBehaviour
{
  public WorldTileFloat recoilForce;

  public delegate void GuardedHandler(PlayerUnitController player);
  public event GuardedHandler OnGuarded;

  public void HandleGuarded(PlayerUnitController player) => OnGuarded(player);

  internal virtual Vector2 GetRecoilFor(PlayerUnitController player) =>
    RecoilHelpers.GetRecoilFromTo(player.transform, transform, recoilForce);
}