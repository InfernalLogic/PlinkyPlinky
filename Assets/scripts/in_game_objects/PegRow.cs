using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PegRow : MonoBehaviour 
{
	public int object_count = 1;
	public float distance_between_objects = 1f;
	public GameObject object_in_row;
	private GameObject new_object;


	void Start()
	{
		SpawnRow ();
	}

	private void SpawnRow()
	{
		for (int i = 0; i < object_count; ++i)
		{
			new_object = GameObject.Instantiate (object_in_row, 
			                                     transform.TransformPoint(transform.position), 
			                                     transform.rotation) as GameObject;
			new_object.transform.parent = gameObject.transform;
			new_object.transform.position = transform.TransformPoint ((new_object.transform.right * i) * distance_between_objects);
		}
	}
	
}
