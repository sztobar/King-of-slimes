using Kite;
using UnityEngine;

public class PlayerUnitYeetState : MonoBehaviour, IPlayerUnitState {

  [SerializeField] private float yeetTime;
  public float unmergeTime;
  public Vector2 yeetedBounciness;

  private PlayerUnitStateMachine stateMachine;
  private PlayerPhysics physics;
  private HorizontalFlipComponent flip;
  private PlayerVulnerability vulnerability;

  private float yeetTimeLeft;
  private float unmergeTimeLeft;

  public void Inject(PlayerUnitDI di) {
    stateMachine = di.stateMachine;
    physics = di.physics;
    flip = di.flip;
    vulnerability = di.vulnerability;
  }

  public bool IsYeeting() => yeetTimeLeft > 0;// || unmergeTimeLeft > 0;
  public bool CanMerge() => !(yeetTimeLeft > 0 || unmergeTimeLeft > 0);

  public void StartState() {
    vulnerability.SetInvulnerable();
    yeetTimeLeft = yeetTime;
    unmergeTimeLeft = unmergeTime;
    physics.velocity.bounciness = yeetedBounciness;
    AudioSingleton.PlaySound(AudioSingleton.Instance.clips.yeet);
  }

  private void Update()
  {
    if (unmergeTimeLeft > 0)
    {
      unmergeTimeLeft -= Time.deltaTime;
    }
  }

  public void UpdateState() {
    if (yeetTimeLeft <= 0) {
      stateMachine.SetDefaultState();
    }
    yeetTimeLeft -= Time.deltaTime;
    physics.YeetUpdate();
  }

  public void ExitState()
  {
    physics.velocity.bounciness = Vector2.zero;
  }


  public void SetLaunchVelocity(Vector2 launchVelocity) {
    physics.velocity.Value = launchVelocity;
    yeetTimeLeft = yeetTime;
    flip.Direction = Direction2HHelpers.FromFloat(launchVelocity.x);
  }
}
