using Kite;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerButtonSprite : MonoBehaviour
{
  public PlayerButton button;
  public SlimeMap<Sprite> slimeButtonSprite;

  void Update()
  {
    if (!button)
      return;

    var spriteRenderer = button.spriteRenderer;
    var interactionPredicate = button.interactionPredicate;
    if (spriteRenderer && interactionPredicate.interactionType == SlimeInteractionPredicate.SlimeInteractionType.OnlyUnitNoAssembly)
    {
      foreach ((SlimeType type, bool canInteract) in interactionPredicate.canInteract.GetPairEnumerable())
      {
        if (canInteract && slimeButtonSprite.Get(type))
        {
          spriteRenderer.sprite = slimeButtonSprite.Get(type);
        }
      }
    }
  }
}