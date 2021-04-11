using Kite;
using System.Collections.Generic;
using UnityEngine;

public class WorldTileTest : MonoBehaviour
{
  public WorldTileFloat velocity = 5;
  public WorldTileFloat acceleration = new WorldTileFloat { value = 1, type = WorldTileFloat.Type.Tile };

  void Awake()
  {
    Debug.Log($"vel: {velocity}");
    Debug.Log($"acc: {acceleration}");
  }

}