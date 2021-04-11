using System.Collections;
using UnityEngine;

public class RockbatCameraSegmentCollider : MonoBehaviour, IRockbatComponent
{
  public new BoxCollider2D collider;

  private Rockbat controller;

  private CameraSegment initialCameraSegment;
  private GameplayCamera playerCamera;

  public void Inject(Rockbat rockbat)
  {
    controller = rockbat;
  }

  public void DisableCollider() => collider.enabled = false;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CameraSegment cameraSegment = collision.GetComponent<CameraSegment>();
    if (cameraSegment)
    {
      OnCameraSegmentEnter(cameraSegment);
    }
  }

  private void OnCameraSegmentEnter(CameraSegment cameraSegment)
  {
    if (!initialCameraSegment)
    {
      initialCameraSegment = cameraSegment;
    }
    else if (cameraSegment != initialCameraSegment)
    {
      GameplayCamera gameplayCamera = GameplayManager.instance.camera;
      if (gameplayCamera.ActiveSegment != initialCameraSegment)
      {
        controller.destroyable.DestroyEnemy();
      }
      else if (!gameplayCamera)
      {
        this.playerCamera = gameplayCamera;
        gameplayCamera.OnChange += OnActiveCameraChange;
      }
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    CameraSegment cameraSegment = collision.GetComponent<CameraSegment>();
    if (cameraSegment && !controller.fsm.IsHit())
      controller.destroyable.DestroyEnemy();
  }

  private void OnActiveCameraChange() =>
    controller.destroyable.DestroyEnemy();

  private void OnDisable()
  {
    if (playerCamera)
      playerCamera.OnChange -= OnActiveCameraChange;
  }
}
