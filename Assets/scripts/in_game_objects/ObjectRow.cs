using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ObjectRow : MonoBehaviour 
{
  [SerializeField]
	private int object_count = 1;
  [SerializeField]
  private float distance_between_objects = 1f;
  [SerializeField]
  private GameObject object_in_row;
  [SerializeField]
  private Vector3 slope = Vector3.right;

	private GameObject new_object;

	void Start()
	{
		SpawnRow ();
	}

	private void SpawnRow()
	{
		for (int i = 0; i < object_count; ++i)
		{
      InstantiateNewObjectAtParentPosition();
      MakeObjectAChild(new_object);
      MoveNewObjectToTargetPosition(i);
		}
	}

  private void MoveNewObjectToTargetPosition(int i)
  {
    new_object.transform.position = transform.TransformPoint(CalculateObjectPosition(i));
  }

  private void MakeObjectAChild(GameObject target_object)
  {
    target_object.transform.parent = gameObject.transform;
  }

  private void InstantiateNewObjectAtParentPosition()
  {
    new_object = GameObject.Instantiate(object_in_row,
                                         transform.TransformPoint(transform.position),
                                         transform.rotation) as GameObject;
  }

  private Vector3 CalculateObjectPosition(int object_count)
  {

    return (slope.normalized * distance_between_objects) * (float)object_count;
  }

  public void SetObjectCount(int object_count)
  {
    this.object_count = object_count;
  }

  public void SetDistanceBetweenObjects(float distance_between_objects)
  {
    this.distance_between_objects = distance_between_objects;
  }

  public void SetObjectInRow(GameObject object_in_row)
  {
    this.object_in_row = object_in_row;
  }

  public void SetSlope(Vector3 slope)
  {
    this.slope = slope;
  }
}
