using UnityEngine;

public class CameraSegmentMember : MonoBehaviour
{
  private static Collider2D[] results = new Collider2D[4];
  public delegate void SegmentChangeAction(CameraSegment segment);

  private CameraSegment segment;

  public SegmentChangeAction OnSegmentChange { get; set; } = _ => { };

  public CameraSegment Segment {
    get => segment;
    set
    {
      CameraSegment oldSegment = segment;
      segment = value;

      if (segment != null && segment != oldSegment)
        OnSegmentChange(segment);
    }
  }

  public void ManualCast()
  {
    Vector2 point = transform.position;
    ContactFilter2D contactFilter = new ContactFilter2D
    {
      useTriggers = true,
      useLayerMask = true,
      layerMask = UnityConstants.Layers.CameraSegmentMask
    };
    int count = Physics2D.OverlapPoint(point, contactFilter, results);
    for (int i = 0; i < count; i++)
    {
      CameraSegment overlappingSegment = results[i].GetComponent<CameraSegment>();
      if (overlappingSegment)
      {
        Segment = overlappingSegment;
        break;
      }
    }
  }
}