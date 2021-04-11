using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

public class CameraBlendHandler : MonoBehaviour {

  [SerializeField] private CinemachineBrain brain;
  [SerializeField] private CinemachineBlendDefinition sameSegmentBlend;
  [SerializeField] private CinemachineBlendDefinition differentSegmentBlend;

  private Collider2D currentCameraSegment;

  public void OnCameraChange(Collider2D targetCameraSegment) {
    if (!currentCameraSegment) {
      currentCameraSegment = targetCameraSegment;
    } else if (targetCameraSegment == currentCameraSegment) {
      brain.m_DefaultBlend = sameSegmentBlend;
    } else {
      brain.m_DefaultBlend = differentSegmentBlend;
      currentCameraSegment = targetCameraSegment;
    }
  }

  public void UpdateCurrentCameraSegment(Collider2D newCameraSegment) {
    currentCameraSegment = newCameraSegment;
  }
}
