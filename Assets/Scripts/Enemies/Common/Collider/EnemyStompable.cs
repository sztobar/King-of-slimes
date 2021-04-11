using Kite;
using System;
using System.Collections;
using UnityEngine;

public class EnemyStompable : MonoBehaviour
{
  public WorldTileFloat recoilForce;

  public delegate void StompedHandler(PlayerUnitController player);
  public event StompedHandler OnStomped;

  public void HandleStomped(PlayerUnitController player) => OnStomped(player);

  public Vector2 GetStompRecoil() =>
    Vector2.up * recoilForce;
}