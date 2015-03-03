using UnityEngine;
using System.Collections;

public class ChildScalerTarget
{
  private GameObject target;
  public GameObject Target { get { return target; } }

  private Vector3 starting_position;
  public Vector3 StartingPosition { get { return starting_position; } }

  private Vector3 ending_position;
  public Vector3 EndingPosition { get { return ending_position; } }

  public bool is_growing = true;

  public ChildScalerTarget(GameObject target, Vector3 starting_position, Vector3 ending_position)
  {
    this.target = target;
    this.starting_position = starting_position;
    this.ending_position = ending_position;
  }
}
