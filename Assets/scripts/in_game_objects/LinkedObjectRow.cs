using UnityEngine;
using System.Collections;

public class LinkedObjectRow : PrefabLinker 
{
  [SerializeField]
  private ObjectRow dummy_object_row;
  [SerializeField]
  private int object_count = 1;
  [SerializeField]
  private float distance_between_objects = 1f;
  [SerializeField]
  private GameObject object_in_row;
  [SerializeField]
  private Vector3 slope = Vector3.right;

  private GameObject instantiator;

  protected void Awake()
  {
    dummy_object_row.SetObjectCount(object_count);
    dummy_object_row.SetDistanceBetweenObjects(distance_between_objects);
    dummy_object_row.SetObjectInRow(object_in_row);
    dummy_object_row.SetSlope(slope);

    instantiator = Instantiate(dummy_object_row, transform.position, transform.rotation) as GameObject;
    linked_prefab = instantiator;

    base.Awake();
  }
}
