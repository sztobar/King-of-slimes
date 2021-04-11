using System.Collections;
using UnityEngine;


public abstract class BaseAssemblyModules : MonoBehaviour, IPlayerUnitComponent {

  public abstract void Inject(PlayerUnitDI di);
}
