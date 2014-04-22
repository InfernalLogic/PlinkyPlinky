using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PegWheel : MonoBehaviour 
{
	public int object_count = 1;
	public float wheel_radius = 1f;
	public GameObject object_on_wheel;
	public float rotation_speed = 0f;
	
	private GameObject new_object;
	private Vector3 wheel_rotator = Vector3.zero;

	void Start()
	{
		if (transform.childCount == 0)
		{
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


		}
		wheel_rotator.z = rotation_speed;
	}

	void FixedUpdate()
	{
		transform.Rotate (wheel_rotator);
	}

}
