using System;
using System.Collections;
using UnityEngine;

public class PlayerRespawnHandler : MonoBehaviour, IPlayerComponent
{

  private readonly SlimeMap<IPlayerRespawner> checkpointsMap = new SlimeMap<IPlayerRespawner>();

  public void Inject(PlayerController controller)
  {
  }

  public bool SetCheckpoint(SlimeType type, IPlayerRespawner checkpoint)
  {
    IPlayerRespawner previousCheckpoint = checkpointsMap.Get(type);
    if (previousCheckpoint == checkpoint)
    {
      return false;
    }
    if (previousCheckpoint != null) 
    {
      previousCheckpoint.OnDeactivate(type);
    }
    checkpointsMap.Set(type, checkpoint);
    checkpoint.OnActivate(type);
    return true;
  }

  public IPlayerRespawner GetCheckpointRespawner(SlimeType type)
  {
    return checkpointsMap.Get(type);
  }
}
