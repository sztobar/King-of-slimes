using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ScriptableSlime")]
public class ScriptableSlime : ScriptableUnit {

  [Header("Slime Stats")]

  [SerializeField] private SlimeType type;

  [SerializeField, Range(1, 2)] private int hearts = 1;
  [SerializeField, Range(1, 2)] private int strength = 1;
  [SerializeField] private bool isPosionImmune;

  public SlimeType Type => type;
  public int Strength => strength;
  public int Hearts => hearts;
  public bool IsPoisonImmune => isPosionImmune;
}
