using Kite;
using NaughtyAttributes;
using UnityEngine;

public class PlayerSpawnables : MonoBehaviour, IPlayerComponent
{
  [MinMaxSlider(0, 180)]
  public Vector2 relicAngle;

  [MinMaxSlider(0, 10)]
  public Vector2 relicTileVelocity;

  [MinMaxSlider(0, 360)]
  public Vector2 relicRotationsPerSecond;

  public SlimeMap<SlimeRelict> relicts;
  public SlimeMap<SlimePoof> poofs;


  public void OnDeath(PlayerUnitController unit, Vector2 relictVelocity)
  {
    PlayerBaseStats stats = unit.di.stats;
    SlimeType poofType = stats.SlimeType;
    Vector2 spawnPosition = unit.transform.position;

    SlimePoof slimePoof = poofs[poofType];
    slimePoof.SpawnAt(spawnPosition);

    foreach(SlimeType type in SlimeTypeHelpers.GetEnumerable())
    {
      if (stats.HasType(type)) {
        SlimeRelict relict = relicts[type];
        relict.SpawnAt(spawnPosition);

        Vector2 velocity = GenerateRelictVelocity();

        relict.SetVelocity(velocity);
        relict.SetRotation(RandomHelpers.Range(relicRotationsPerSecond));
        //relictVelocity = Vector2.zero;
      }
    }
  }

  private Vector2 GenerateRelictVelocity()
  {
    Vector2 normal = Vector2Helpers.DegreeToVector2(RandomHelpers.Range(relicAngle));
    float velocity = TileHelpers.TileToWorld(RandomHelpers.Range(relicTileVelocity));
    return normal * velocity;
  }

  public void Inject(PlayerController controller) { }
}