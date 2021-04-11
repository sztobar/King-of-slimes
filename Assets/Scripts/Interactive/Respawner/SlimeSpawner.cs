using System.Collections;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour, IPlayerRespawner
{
  public PlayerUnitController unit;
  public SpawnWispRenderer wispRenderer;
  public CameraSegmentMember segmentMember;

  private Vector2 spawnPoint;

  public Vector2 SpawnPoint => spawnPoint;
  public CameraSegment CameraSegment => segmentMember.Segment;

  private void Start()
  {
#if UNITY_EDITOR
    spawnPoint = unit.transform.position;
#else
    spawnPoint = transform.position;
    unit.transform.position = SpawnPoint;
#endif
    PlayerBaseStats stats = unit.di.stats;
    unit.di.mainDi.respawnHandler.SetCheckpoint(stats.SlimeType, this);
  }

  public void OnActivate(SlimeType type)
  {
    wispRenderer.PlayIdle();
    if (!segmentMember.Segment)
      segmentMember.ManualCast();
  }

  public void OnDeactivate(SlimeType type)
  {
    wispRenderer.SetOff();
  }
}