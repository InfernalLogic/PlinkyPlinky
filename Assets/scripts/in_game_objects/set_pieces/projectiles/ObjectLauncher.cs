using UnityEngine;
using System.Collections;

public class ObjectLauncher : MonoBehaviour 
{
  [SerializeField]
	private GameObject projectile;
  [SerializeField]
  private float launch_force;
  [SerializeField]
  private float cooldown = 1.0f;
  [SerializeField]
  private float start_time_offset = 0.0f;
  [SerializeField]
  private bool spawn_object_as_child = true;

	private float spawn_timer;
	private GameObject new_object;
	private Vector3 launch_vector;

	void Start()
	{
		spawn_timer = Time.time + start_time_offset;
    launch_vector = transform.right;
    launch_vector *= launch_force;
	}

	void Update()
	{
    if (TimerHasCooledDown())
		{
			SpawnNewObject ();
      ResetTimer();
		}
	}

  private void ResetTimer()
  {
    spawn_timer = Time.time + cooldown;
  }

  private bool TimerHasCooledDown()
  {
    return spawn_timer <= Time.time;
  }

	void SpawnNewObject()
	{
    new_object = InstantiateNewProjectile();
    ApplyLaunchForce(new_object);
    if (spawn_object_as_child)
      MakeObjectAChild(new_object);
	}

  private void ApplyLaunchForce(GameObject target_object)
  {
    target_object.GetComponent<Rigidbody2D>().AddForce(launch_vector);
  }

  GameObject InstantiateNewProjectile()
  {
    return GameObject.Instantiate(projectile, transform.position, transform.rotation) as GameObject;
  }

  private void MakeObjectAChild(GameObject target_object)
  {
    target_object.transform.parent = gameObject.transform;
  }
}
