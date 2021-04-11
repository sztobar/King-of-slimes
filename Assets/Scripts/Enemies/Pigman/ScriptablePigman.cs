using Kite;
using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Pigman")]
public class ScriptablePigman : ScriptableObject {

  [Header("Time ranges (from-to)")]
  public Vector2 prepareTimeToAttack;
  public Vector2 abandonChaseTime;
  public Vector2 roarsInterval;
  public Vector2 parryTime;
  public Vector2 staggerTime;

  [Header("Movement")]
  public float runTileVelocity;
  public float walkTileVelocity;

  [Header("Stats")]
  public int hp;

  [Header("Attack")]
  public int attackDamage;

  public Recoils fullAssemblyRecoils;
  public Recoils recoils;

  public float guardFullAssemblyRecoilTileForce;
  public float guardPartialAssemblyRecoilTileForce;

  public float parryFullAssemblyRecoilTileForce;
  public float parryPartialAssemblyRecoilTileForce;

  public float attackFullAssemblyRecoilTileForce;
  public float attackPartialAssemblyRecoilTileForce;

  public int collisionDamage;

  public WorldTileFloat collisionForce;

  [Serializable]
  public struct Recoils
  {
    public WorldTileFloat collisionForce;
    public WorldTileFloat guardedRecoilForce;
    public WorldTileFloat parryForce;
    public WorldTileFloat attackForce;
  }
}
