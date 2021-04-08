using System.Collections;
using UnityEngine;

namespace Kite
{
  public static class StringHelpers
  {
    public static string Capitalize(string str) =>
      str.Substring(0, 1).ToUpper() + str.Substring(1);
  }
}