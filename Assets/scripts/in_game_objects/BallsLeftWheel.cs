using UnityEngine;
using System.Collections;

public class BallsLeftWheel : ObjectWheel 
{
  private int last_known_max_balls = 0;

  void Update()
  {
    if (last_known_max_balls != FindObjectOfType<MaxBombsUpgrader>().GetValue())
    {
      last_known_max_balls = FindObjectOfType<MaxBombsUpgrader>().GetValue();
      object_count = last_known_max_balls;
      DestroyAllChildren();
      SpawnWheelObjects();
    }

    for (int i = 0; i < last_known_max_balls; ++i)
    {
      if (i < BombScript.GetBombCount())
        children[i].transform.renderer.enabled = false;
      else
        children[i].transform.renderer.enabled = true;
    }

    RotateWheel();
  }
}
