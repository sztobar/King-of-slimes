using UnityEngine;
using System.Collections;

public interface ITextEffectAppear {
  void Update(float deltaTime);
  void ForceUpdate();

  bool IsEffectEnded();
  void AnimationUpdate();
}
