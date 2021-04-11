using Kite;
using System.Collections;
using UnityEngine;

public class EnemyDestroyable : MonoBehaviour
{
  public DeathPoof deathPoof;

  public void DestroyEnemyWithPoof()
  {
    deathPoof.DestroyWithPoof(gameObject);
  }

  public void DestroyEnemy()
  {
    Destroy(gameObject);
  }
}