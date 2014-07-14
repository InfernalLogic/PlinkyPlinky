using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(GameObject))]
public class BackgroundObjectSpawner : MonoBehaviour 
{
  [SerializeField] 
	private GameObject bg_object;
  [SerializeField]
  private BoxCollider2D spawn_zone;

	private GameObject new_bg_object;

	private Vector3 spawn_point = Vector3.zero;
	private Vector3 move_vector = Vector3.one;

  [SerializeField]
	private int spawn_timer_min = 0,
						 spawn_timer_max = 0;

	private int spawn_timer= 0;

	void FixedUpdate()
	{
    if (IsTimeToSpawnNewObject()) 
		{
      ResetSpawnTimer();
			FindNewSpawnPoint();
			SpawnNewObject ();
		}

		--spawn_timer;
	}

  private void ResetSpawnTimer()
  {
    spawn_timer = Random.Range(spawn_timer_min, spawn_timer_max);
  }

  private bool IsTimeToSpawnNewObject()
  {
    return spawn_timer <= 0;
  }

	void SpawnNewObject()
	{
		new_bg_object = GameObject.Instantiate (bg_object, spawn_point, transform.rotation) as GameObject;
		new_bg_object.GetComponent<BackgroundObject>().SetMoveVector (transform.right);
		new_bg_object.transform.parent = transform;
	}

	void FindNewSpawnPoint()
	{
		spawn_point.x = Random.Range (0.0f, spawn_zone.size.x) - (spawn_zone.size.x / 2.0f);
    spawn_point.y = Random.Range(0.0f, spawn_zone.size.y) - (spawn_zone.size.y / 2.0f);
		spawn_point = transform.TransformPoint (spawn_point);
	}

}
