using UnityEngine;

public class PlinkerScript : MonoBehaviour
{
	public float movement_speed = 0f;
  public GameObject new_bomb;
	public Vector2 drop_point_offset = Vector2.zero;
	public GameObject selected_bomb;
	
	private Vector2 movement_vector = Vector2.zero;
	private BombScript selected_bomb_script;
	private float bomb_cooldown_timer = 0f;
	
  // Use this for initialization
  void Start()
  {
		movement_vector.x = movement_speed;
		bomb_cooldown_timer = Time.time;

		selected_bomb_script = selected_bomb.GetComponent<BombScript>();
  }

  void FixedUpdate()
  {
		rigidbody2D.velocity = movement_vector;
  }

	void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.tag == "plinker_reverse_trigger")
		{
			ReverseDirection ();
		}

	}

  void ReverseDirection()
  {
		movement_vector *= -1.0f;
  }

  public void DropBomb()
  {
		if (BombIsReady())
		{
			Vector2 drop_point = transform.position;
	    drop_point += drop_point_offset;

	    Instantiate(new_bomb, drop_point, transform.rotation);
			bomb_cooldown_timer = Time.time;

			ScoreTracker.BombDropped();
		}
  }

	private bool BombIsReady()
	{
		if (bomb_cooldown_timer + selected_bomb_script.cooldown < Time.time)
		{
			return true;
		}
		else
		{
			return false;
		}

	}
	
}
