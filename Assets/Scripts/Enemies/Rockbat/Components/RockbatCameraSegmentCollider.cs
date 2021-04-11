using System.Collections;
using UnityEngine;

public class RockbatCameraSegmentCollider : MonoBehaviour, IRockbatComponent
{
  public new BoxCollider2D collider;

  private Rockbat controller;

  private CameraSegment initialCameraSegment;
  private bool removeCameraSegmentListener;

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
      CameraController cameraController = GameplayManager.instance.cameraController;
      if (cameraController.CurrentSegment != initialCameraSegment)
      {
        controller.destroyable.DestroyEnemy();
      }
      else if (!removeCameraSegmentListener)
      {
        removeCameraSegmentListener = true;
        cameraController.OnSegmentChange += OnActiveCameraChange;
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
    if (removeCameraSegmentListener)
    {
      CameraController cameraController = GameplayManager.instance.cameraController;
      cameraController.OnSegmentChange -= OnActiveCameraChange;
    }
  }
}
