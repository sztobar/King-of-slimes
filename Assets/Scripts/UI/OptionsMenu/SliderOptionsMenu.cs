using UnityEngine;

public class SliderOptionsMenu : BaseMenuOptionBehavior {

  [SerializeField]
  private TriangleSlider slider;

  public override void OnConfirm() {
  }

  public override void OnDeselect() {
    base.OnDeselect();
    slider.Deactivate();
  }

  public override void OnSelect() {
    base.OnSelect();
    slider.Activate();
  }
}
