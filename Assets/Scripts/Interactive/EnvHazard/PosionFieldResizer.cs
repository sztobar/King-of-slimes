using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class PosionFieldResizer : MonoBehaviour
{
  public SpriteRenderer spriteRenderer;
  public Vector2 boxColliderExtrude;
  public BoxCollider2D boxCollider;
  public new ParticleSystem particleSystem;
  public Vector2 particleSystemExtrude;

  private void Awake()
  {
    if (Application.isPlaying)
    {
      enabled = false;
    }
  }

  private void Update()
  {
    if (!Application.isPlaying)
    {
      if (spriteRenderer
        && spriteRenderer.drawMode == SpriteDrawMode.Tiled
        && boxCollider
        && particleSystem)
      {
        Vector2 size = spriteRenderer.bounds.size;
        boxCollider.size = size + boxColliderExtrude;
        var shape = particleSystem.shape;
        shape.scale = size + particleSystemExtrude;
      }
    }
  }
}