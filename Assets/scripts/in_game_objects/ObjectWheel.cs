using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ObjectWheel : MonoBehaviour 
{
  [SerializeField]
	protected int object_count = 1;
  [SerializeField]
  protected float wheel_radius = 1f;
  [SerializeField]
  protected GameObject object_on_wheel;
  [SerializeField]
  protected float rotation_speed = 0f;

  protected List<GameObject> children = new List<GameObject>();

	private GameObject new_object;
	private Vector3 wheel_rotator = Vector3.zero;

	void Awake()
	{
    if (OutdatedChildrenFound())
      DestroyAllChildren();

		SpawnWheelObjects();
	}

  void Update()
  {
    RotateWheel();
  }

  protected void RotateWheel()
  {
    transform.Rotate(wheel_rotator * Time.deltaTime);
  }

  protected void DestroyAllChildren()
  {
    foreach (Transform child in transform)
    {
      children.Add(child.gameObject);
    }

    foreach (GameObject child in children)
    {
      DestroyImmediate(child);
    }

    children.Clear();
  }

  protected bool OutdatedChildrenFound()
  {
    return transform.childCount > 0;
  }

	protected void SpawnWheelObjects()
	{
		double degrees_between_objects = 360.0 / object_count;
		double current_object_rotation = degrees_between_objects;
		
		Vector3 rotator = Vector3.zero;
		rotator.z = (float)current_object_rotation;

    for (int i = 0; i < object_count; ++i)
		{
			new_object = GameObject.Instantiate (object_on_wheel, 
			                                     transform.TransformPoint(transform.position), 
			                                     transform.rotation) as GameObject;
			new_object.transform.parent = gameObject.transform;
      children.Add(new_object);
			new_object.transform.Rotate (rotator);
			new_object.transform.position = transform.TransformPoint(wheel_radius * new_object.transform.right);
			
			current_object_rotation += degrees_between_objects;
			
			rotator.z = (float)current_object_rotation;
		}
		
		wheel_rotator.z = rotation_speed;

	}
}
