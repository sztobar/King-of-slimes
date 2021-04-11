using Cinemachine;
using Kite;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraSegment : MonoBehaviour
{
  public CameraSegmentCamera cam;
  public int collisionDistance = 1;
  public new CompositeCollider2D collider;
  public TilemapRenderer tilemapRenderer;

  private readonly HashSet<PlayerUnitController> unitsInside = new HashSet<PlayerUnitController>();
  private readonly List<CameraSegmentMember> members = new List<CameraSegmentMember>();

  public CinemachineVirtualCamera VirtualCamera => cam.virtualCam;

  private void Awake()
  {
    if (tilemapRenderer)
      tilemapRenderer.enabled = false;
  }

  public void SetCameraPosition(Vector3 position)
  {
    CinemachineVirtualCamera virtualCamera = cam.virtualCam;
    position.z = virtualCamera.transform.position.z;
    virtualCamera.ForceCameraPosition(position, Quaternion.identity);
  }

  public Vector2 GetCameraPosition()
  {
    CinemachineVirtualCamera vcam = cam.virtualCam;
    CameraState state = vcam.State;
    return state.CorrectedPosition;
  }

  internal void SetCameraPosition(Transform followTarget)
  {
    //Vector3 unitPosition = followTarget.position;
    //Vector3 deltaPosition = unitPosition - cam.virtualCam.transform.position;
    //cam.virtualCam.OnTargetObjectWarped(followTarget, deltaPosition);

    //bool isvalid = cam.virtualCam.PreviousStateIsValid;
    //Debug.Log($"virtualCam is valid {isvalid}");
    //if (isvalid)
    //{
    //  cam.virtualCam.PreviousStateIsValid = false;
    //}
  }

  public void SetCameraActive()
  {
    cam.Activate();
  }

  public void SetCameraInactive()
  {
    cam.Deactivate();
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    CameraSegmentMember member = collider.GetComponent<CameraSegmentMember>();
    if (member)
      OnMemberEnter(member);

    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collider);
    if (!unit)
      return;

    PlayerUnitCamera playerCamera = unit.di.camera;
    if (playerCamera.CameraSegment == null)
    {
      unitsInside.Add(unit);
      playerCamera.CameraSegment = this;
    }
    else
      CheckUnitForSegmentEnter(unit);
  }

  private void OnTriggerStay2D(Collider2D collider)
  {
    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collider);
    if (!unit || unitsInside.Contains(unit))
      return;

    CheckUnitForSegmentEnter(unit);
  }

  private void CheckUnitForSegmentEnter(PlayerUnitController unit)
  {
    bool enteredFromSide = false;
    foreach (Dir4 dir in Dir4.GetList())
    {
      enteredFromSide = CheckUnitForSegmentEnterFrom(unit, dir);
      if (enteredFromSide)
        break;
    }
    if (!enteredFromSide)
      CheckIfFullyInside(unit);
  }

  private void CheckIfFullyInside(PlayerUnitController unit)
  {
    Bounds playerBounds = unit.di.boxCollider.bounds;
    Bounds segmentBounds = collider.bounds;
    bool isXContained = playerBounds.max.x <= segmentBounds.max.x && playerBounds.min.x >= segmentBounds.min.x;
    bool isYContained = playerBounds.max.y <= segmentBounds.max.y && playerBounds.min.y >= segmentBounds.min.y;
    if (isXContained && isYContained)
    {
      unitsInside.Add(unit);
      unit.di.camera.CameraSegment = this;
    }
  }

  private bool CheckUnitForSegmentEnterFrom(PlayerUnitController unit, Dir4 dir)
  {
    Bounds playerBounds = unit.di.boxCollider.bounds;
    Bounds segmentBounds = collider.bounds;
    int axis = dir.Axis;

    float centersDistance = dir * (playerBounds.center[axis] - segmentBounds.center[axis]);
    float distanceToSegment = centersDistance - segmentBounds.extents[axis];
    float distanceToEnter = playerBounds.extents[axis] - collisionDistance;
    if (distanceToSegment < distanceToEnter && distanceToSegment > -collisionDistance)
    {
      unitsInside.Add(unit);
      unit.di.camera.CameraSegment = this;
      unit.di.physics.movement.TryToMove(playerBounds.size[axis], -dir);
      return true;
    }
    return false;
  }

  private void OnTriggerExit2D(Collider2D collider)
  {
    CameraSegmentMember member = collider.GetComponent<CameraSegmentMember>();
    if (member)
      OnMemberExit(member);

    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collider);
    if (!unit)
      return;

    unitsInside.Remove(unit);
    
    PlayerUnitCamera camera = unit.di.camera;
    if (camera.CameraSegment == this)
      camera.CameraSegment = null;
    //camera.RemoveCameraSegment(this);
  }

  private void OnMemberExit(CameraSegmentMember member)
  {
    members.Remove(member);
    if (member.Segment == this)
      member.Segment = null;
  }

  private void OnMemberEnter(CameraSegmentMember member)
  {
    members.Add(member);
    member.Segment = this;
  }

  internal void SetCameraTarget(Transform followTarget)
  {
    Transform previousFollow = cam.virtualCam.Follow;
    cam.virtualCam.Follow = followTarget;
    if (Application.isPlaying)
    {
      if (previousFollow == null)
        cam.virtualCam.ForceCameraPosition(cam.virtualCam.transform.position, Quaternion.identity);
    }
  }
}
