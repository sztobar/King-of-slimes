using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitRespawnHandler : MonoBehaviour, IPlayerUnitComponent
{
  private PlayerBaseStats stats;
  private PlayerUnitController controller;
  private PlayerUnitHandler unitHandler;
  private PlayerHPModule hp;
  private PlayerPhysics physics;
  private new PlayerUnitCamera camera;


  public void Inject(PlayerUnitDI di)
  {
    controller = di.controller;
    stats = di.stats;
    unitHandler = di.mainDi.unitHandler;
    hp = di.hp;
    physics = di.physics;
    camera = di.camera;
  }

  public void Respawn()
  {
    RespawnCurrentUnit();
    if (stats.IsAssembly)
      RespawnAssembly();
  }

  private void RespawnCurrentUnit()
  {
    PlayerRespawnHandler respawnHandler = controller.mainController.di.respawnHandler;
    IPlayerRespawner playerRespawner = respawnHandler.GetCheckpointRespawner(stats.SlimeType);
    if (playerRespawner.CameraSegment)
      camera.CameraSegment = playerRespawner.CameraSegment;
    Vector2 respawnPosition = playerRespawner.SpawnPoint;
    controller.transform.position = respawnPosition;
    hp.OnRespawn();
    physics.velocity.Value = Vector2.zero;
  }

  private void RespawnAssembly()
  {
    PlayerRespawnHandler respawnHandler = controller.mainController.di.respawnHandler;
    IPlayerRespawner kingRespawner = respawnHandler.GetCheckpointRespawner(SlimeType.King);
    HashSet<SlimeType> respawnSeparately = new HashSet<SlimeType>();

    foreach (SlimeType type in SlimeTypeHelpers.GetWithoutKingEnumerable())
    {
      if (stats.HasType(type))
      {
        IPlayerRespawner slimeRespawner = respawnHandler.GetCheckpointRespawner(type);
        if (slimeRespawner != kingRespawner)
          respawnSeparately.Add(type);
      }
    }

    if (respawnSeparately.Count > 0)
      unitHandler.RespawnOutOfAssembly(respawnSeparately);
  }
}
