using UnityEngine;
using System.Collections;

public class BackgroundObjectSpawner : MonoBehaviour 
{
	public GameObject bg_object;
	private GameObject new_bg_object;
	private BoxCollider2D spawn_zone;
	Vector3 spawn_point = Vector3.zero;

	Vector3 move_vector = Vector3.one;

	public int spawn_timer_min = 0,
						 spawn_timer_max = 0;

	private int spawn_timer= 0;

	void Start()
	{
		spawn_zone = GetComponent<BoxCollider2D> ();
		if (spawn_zone == null)
		{
			Debug.Log ("spawn_zone failed to load");
		}
		FindNewSpawnPoint ();
		move_vector = RelativeVector (0f);
		SpawnNewObject ();
	}

	void FixedUpdate()
	{
		if (spawn_timer <= 0) 
		{
			spawn_timer = Random.Range (spawn_timer_min, spawn_timer_max);
			FindNewSpawnPoint();
			SpawnNewObject ();

		}

		--spawn_timer;
	}

	void SpawnNewObject()
	{
		new_bg_object = GameObject.Instantiate (bg_object, spawn_point, transform.rotation) as GameObject;
		new_bg_object.GetComponent<BackgroundObject>().SetMoveVector (move_vector);
	}

	void FindNewSpawnPoint()
	{
		spawn_point.x = Random.Range (0.0f, spawn_zone.size.x) - (spawn_zone.size.x / 2);
		spawn_point.y = Random.Range (0.0f, spawn_zone.size.y) - (spawn_zone.size.y / 2);
		spawn_point = transform.TransformPoint (spawn_point);
	}

	//returns a unit vector pointing in the "heading" direction 
	//relative to the gameobject's internal transform.rotation
	//i.e. the vector will rotate with the object
	Vector3 RelativeVector(float heading)
	{
		Vector3 relative_vector = Vector3.one;
		//find the rotation about the z-axis in degrees
		float z_rotation = Mathf.Deg2Rad * transform.rotation.eulerAngles.z;
		z_rotation += heading;
		relative_vector.x *= Mathf.Cos (z_rotation);
		relative_vector.y *= Mathf.Sin (z_rotation);
		relative_vector.z = 0;

		relative_vector.Normalize ();

		Debug.Log ("BGobjectspawner move vector:<" + move_vector.x + ", " + 
		           move_vector.y + ", " + move_vector.z + ">");

		return relative_vector;
	}
}
