using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerCheckpoint : MonoBehaviour, IPlayerRespawner
{
  public Transform spawnPoint;
  public SlimeMap<bool> checkpointFor;
  public SlimeMap<SpawnWispRenderer> wisps;
  public CameraSegmentMember segmentMember;

  public Vector2 SpawnPoint => spawnPoint.transform.position;
  public CameraSegment CameraSegment => segmentMember.Segment;

  private void Awake()
  {
    foreach(SpawnWispRenderer wisp in wisps)
      wisp.SetOff();

    segmentMember.ManualCast();
  }

  public void OnActivate(SlimeType type)
  {
    wisps.Get(type).PlayAppear();
  }

  public void OnDeactivate(SlimeType type)
  {
    wisps.Get(type).SetOff();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collision);
    PlayerBaseStats stats = unit.di.stats;
    PlayerRespawnHandler respawnHandler = unit.mainController.di.respawnHandler;
    bool playSound = false;
    foreach ((SlimeType type, bool acceptsType) in checkpointFor.GetPairEnumerable())
    {
      if (acceptsType && stats.HasType(type))
      {
        bool updated = respawnHandler.SetCheckpoint(type, this);
        if (updated)
        {
          playSound = true;
        }
        if (!segmentMember.Segment)
          segmentMember.Segment = unit.di.camera.CameraSegment;
      }
    }
    if (playSound)
    {
      AudioSingleton.PlaySound(AudioSingleton.Instance.clips.checkpoint);
    }
  }
}
