using UnityEngine;
using UnityEngine.UI;

namespace UI.Bars {
  public class BaseBar : MonoBehaviour {

    [SerializeField]
    Image image;

    protected float maxImageWidth;
    protected float imageHeight;

    private void Awake() {
      maxImageWidth = image.rectTransform.rect.width;
      imageHeight = image.rectTransform.rect.height;
    }

    protected void SetPercentage(float percentage) {
      image.rectTransform.sizeDelta = new Vector2(maxImageWidth * percentage, imageHeight);
    }
  }
}