using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ObjectWheel : MonoBehaviour 
{

	public int object_count = 1;
	public float wheel_radius = 1f;
	public GameObject object_on_wheel;
	public float rotation_speed = 0f;
	
	private GameObject new_object;
	private Vector3 wheel_rotator = Vector3.zero;

	void Awake()
	{
		if(transform.childCount > 0)
		{
			ArrayList children = new ArrayList();
			foreach (Transform child in transform)
			{
				children.Add (child.gameObject);
			}
			
			foreach (GameObject child in children)
			{
				DestroyImmediate(child);
			}
		}

		SpawnWheelObjects();
	}

	void Update()
	{
		transform.Rotate(wheel_rotator * Time.deltaTime);
	}

	void SpawnWheelObjects()
	{
		double degrees_between_objects = 360.0 / object_count;
		double current_object_rotation = degrees_between_objects;
		
		Vector3 rotator = Vector3.zero;
		rotator.z = (float)current_object_rotation;
		
		for (int i = object_count; i > 0; --i)
		{
			new_object = GameObject.Instantiate (object_on_wheel, 
			                                     transform.TransformPoint(transform.position), 
			                                     transform.rotation) as GameObject;
			new_object.transform.parent = gameObject.transform;
			new_object.transform.Rotate (rotator);
			new_object.transform.position = transform.TransformPoint(wheel_radius * new_object.transform.right);
			
			current_object_rotation += degrees_between_objects;
			
			rotator.z = (float)current_object_rotation;
		}
		
		wheel_rotator.z = rotation_speed;

		/*
		float degrees_between_objects = 360 / object_count;
		Vector3 rotator = Vector3.zero;
		rotator.z = degrees_between_objects;
		
		for (int i = object_count; i > 0; --i)
		{
			new_object = GameObject.Instantiate (object_on_wheel, 
			                                     transform.TransformPoint(transform.position), 
			                                     transform.rotation) as GameObject;
			new_object.transform.parent = gameObject.transform;
			new_object.transform.Rotate (rotator);
			new_object.transform.position = transform.TransformPoint(wheel_radius * new_object.transform.right);
			rotator.z += degrees_between_objects;
		}
		
		wheel_rotator.z = rotation_speed;
		*/

	}
}
