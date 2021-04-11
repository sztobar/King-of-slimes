using NaughtyAttributes;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Tutorial")]
public class ScriptableTutorial : ScriptableObject
{
  [TextArea]
  public string text;
}
