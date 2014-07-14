using UnityEngine;
using System.Collections;

public class ObjectLauncher : MonoBehaviour 
{
	public GameObject projectile;
	public float launch_force;
	public float cooldown = 1f;
	public float start_time_offset = 0f;

	//public int spawn_timer;
	//private int spawn_cooldown;

	private float spawn_timer;
	private GameObject new_object;
	private Vector3 launch_vector;

	void Start()
	{
		spawn_timer = Time.time + start_time_offset;
	}

	void Update()
	{
		if (spawn_timer <= Time.time)
		{
			SpawnNewObject ();
			spawn_timer = Time.time + cooldown;
		}
	}

	void SpawnNewObject()
	{
		launch_vector = transform.right;
		launch_vector *= launch_force;
		new_object = GameObject.Instantiate (projectile, transform.position, transform.rotation) as GameObject;
		new_object.GetComponent<Rigidbody2D>().AddForce (launch_vector);
	}
}
