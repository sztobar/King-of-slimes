using System.Collections;
using UnityEngine;

public interface IPlayerRespawner {
  Vector2 SpawnPoint { get; }
  CameraSegment CameraSegment { get; }

  void OnActivate(SlimeType type);
  void OnDeactivate(SlimeType type);
}
