using Kite;
using UnityEngine;

public class ParallaxBackground : ParallaxBackgroundBase
{
  [Range(-1, 1)] public float parallaxX = 0;
  [Range(-1, 1)] public float parallaxY = 0;
  public SpriteRenderer spriteRenderer;

  private Vector2Int tileOffset;
  private Vector2 spriteSize;
  private Vector2 startPosition;
  private float startZ;

  private void Awake()
  {
    if (parallaxX != 0 || parallaxY != 0)
    {
      spriteSize = spriteRenderer.sprite.rect.size;
      Vector2 tiledSpriteRendererSize = spriteSize;
      if (parallaxX != 0)
        tiledSpriteRendererSize.x = CameraHelpers.CAMERA_WIDTH + spriteSize.x * 4;
      if (parallaxY != 0)
        tiledSpriteRendererSize.y = CameraHelpers.CAMERA_HEIGHT + spriteSize.y * 4;

      spriteRenderer.size = tiledSpriteRendererSize;
    }
    else
    {
      spriteSize = spriteRenderer.size;
    }
    startPosition = transform.position;
    startZ = transform.position.z;
  }

  public override void UpdateBackgroundPosition(Vector2 cameraPosition)
  {
    Vector2 travel = cameraPosition - startPosition;
    Vector2 parallax = new Vector2(parallaxX, parallaxY);
    Vector2 deltaPosition = (travel * parallax);
    Vector2 offset = tileOffset * spriteSize;
    Vector2 newPosition = startPosition + deltaPosition + offset;
    transform.position = new Vector3(newPosition.x, newPosition.y, startZ);

    Vector2 spriteToCameraMoveRatio = travel * (Vector2.one - parallax);

    if (parallaxX != 0 &&
      (spriteToCameraMoveRatio.x < offset.x ||
      spriteToCameraMoveRatio.x > spriteSize.x + offset.x))
      tileOffset.x = Mathf.FloorToInt(spriteToCameraMoveRatio.x / spriteSize.x);

    if (parallaxY != 0 &&
      (spriteToCameraMoveRatio.y < offset.y ||
      spriteToCameraMoveRatio.y > spriteSize.y + offset.y))
      tileOffset.y = Mathf.FloorToInt(spriteToCameraMoveRatio.y / spriteSize.y);
  }

  public override void Activate()
  {
    gameObject.SetActive(true);
  }

  public override void Deactivate()
  {
    gameObject.SetActive(false);
  }
}
