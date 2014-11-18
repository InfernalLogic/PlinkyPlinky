using UnityEngine;
using System.Collections;

public class LinkedObjectRow : PrefabLinker
{
  [SerializeField]
  private ObjectRow dummy_object_row_prefab;
  [SerializeField]
  private int object_count = 1;
  [SerializeField]
  private float distance_between_objects = 1f;
  [SerializeField]
  private GameObject object_in_row;
  [SerializeField]
  private Vector3 slope = Vector3.right;

  protected override void Awake()
  {
    dummy_object_row_prefab.SetObjectCount(object_count);
    dummy_object_row_prefab.SetDistanceBetweenObjects(distance_between_objects);
    dummy_object_row_prefab.SetObjectInRow(object_in_row);
    dummy_object_row_prefab.SetSlope(slope);

    base.Awake();
  }
}