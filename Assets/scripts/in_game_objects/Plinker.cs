using UnityEngine;

public class Plinker : MonoBehaviour
{
	public float movement_speed = 0f;
  private GameObject new_bomb;
	public GameObject selected_bomb;
	
	private Vector3 movement_vector = Vector3.zero;
	private BombScript selected_bomb_script;
	private float bomb_cooldown_timer = 0f;

  [SerializeField]
  private float bomb_cooling_down_alpha = 0.0f;
  private Color bomb_cooling_down_color;
  private Color bomb_ready_color;
	
  void Start()
  {
		movement_vector.x = movement_speed;
    bomb_cooldown_timer = Time.time - bomb_cooldown_timer;

		selected_bomb_script = selected_bomb.GetComponent<BombScript>();
    new_bomb = selected_bomb;

    InitializeCooldownColors();
  }

	void Update()
	{
    MovePlinker();

    if (BombIsReady())
      renderer.material.color = bomb_ready_color;
    else
      renderer.material.color = bomb_cooling_down_color;
	}

  private void InitializeCooldownColors()
  {
    bomb_ready_color = renderer.material.color;
    bomb_cooling_down_color = renderer.material.color;
    bomb_cooling_down_color.a = bomb_cooling_down_alpha;
  }

  private void MovePlinker()
  {
    transform.position += movement_vector * Time.deltaTime;
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
		if (BombIsReady() && MaxBombsNotExceeded())
		{
	    Instantiate(new_bomb, transform.position, transform.rotation);
      ResetBombCooldownTimer();

      LevelCompleteChecker.Instance().BombDropped();

      PlayerStats.Instance().BombDropped();
		}
  }

  private void ResetBombCooldownTimer()
  {
    bomb_cooldown_timer = Time.time + BombScript.GetCooldown();
  }

	private bool BombIsReady()
	{
    return (bomb_cooldown_timer < Time.time);
	}

  private bool MaxBombsNotExceeded()
  {
    return BombScript.GetBombCount() < 3;
  }
	
}
