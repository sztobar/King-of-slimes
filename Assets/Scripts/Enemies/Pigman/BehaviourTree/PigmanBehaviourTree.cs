using System.Collections;
using UnityEngine;

public class PigmanBehaviourTree : MonoBehaviour, IPigmanComponent {

  public PigmanBtIsHit isHit;
  public PigmanBtCloseCombat closeCombat;
  public PigmanBtFollowPlayer followPlayer;
  public PigmanBtGoBack goBack;
  public PigmanBtSit sit;
  public PigmanBtBlockingAnimation isBlockingAnimation;

  private BtFunc goBackSequence;

  public void Act() {
    _ = BtHelpers.Fallback(
      //isBlockingAnimation,
      isHit,
      closeCombat,
      followPlayer,
      goBackSequence
    );
  }

  public void Inject(PigmanController controller) {
    foreach (IPigmanBtNode node in GetNodes()) {
      node.Inject(controller);
    }
    goBackSequence = new BtFunc(() =>
      BtHelpers.Sequence(goBack, sit)
    );
  }

  private IPigmanBtNode[] GetNodes() =>
    new IPigmanBtNode[] {
      isHit,
      closeCombat,
      followPlayer,
      goBack,
      sit,
      isBlockingAnimation
    };
}
