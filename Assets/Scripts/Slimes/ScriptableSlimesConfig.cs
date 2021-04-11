using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ScriptableSlimesConfig")]
public class ScriptableSlimesConfig : ScriptableObject {

  [SerializeField] private SlimeMap<ScriptableSlime> data;

  public SlimeMap<ScriptableSlime> Data => data;
}
