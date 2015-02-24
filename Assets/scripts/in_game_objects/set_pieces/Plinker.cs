using UnityEngine;
using System.Collections;

public class Plinker : MonoBehaviour
{
  [SerializeField]
  private GameObject left_barrier;
  [SerializeField]
  private GameObject right_barrier;
  [SerializeField]
	private float movement_speed = 0f;
	public GameObject selected_bomb;
	
	private Vector3 movement_vector = Vector3.zero;

  [SerializeField]
  private float bomb_cooling_down_alpha = 0.0f;
  private Color bomb_cooling_down_color;
  private Color bomb_ready_color;

  private Vector3 starting_position;
  private AutoPlinker auto_plinker;

  private Subscriber<GameEvent> subscriber = new Subscriber<GameEvent>();

  void Awake()
  {
    GameEvents.AddSubscriber(subscriber);
  }

  void Start()
  {
		movement_vector.x = movement_speed;

    InitializeCooldownColors();

    starting_position = transform.position;
    auto_plinker = FindObjectOfType<AutoPlinker>();

    StartCoroutine(OutOfBoundsCheck());
  }

	void Update()
	{
    MovePlinker();

    if (BombIsReadyChecker.Instance().BombIsReady())
      renderer.material.color = bomb_ready_color;
    else
      renderer.material.color = bomb_cooling_down_color;

    if (!subscriber.IsEmpty())
    {
      if (subscriber.ReadNewestMessage() == GameEvents.game_reset_event)
      {
        transform.position = starting_position;
      }
      subscriber.DeleteNewestMessage();
    }
	}

  private IEnumerator OutOfBoundsCheck()
  {
    yield return new WaitForSeconds(1.0f);

    if (IsOutOfBounds())
      transform.position = starting_position;

    StartCoroutine(OutOfBoundsCheck());
  }

  private bool IsOutOfBounds()
  {
    return transform.position.x > right_barrier.transform.position.x ||
           transform.position.x < left_barrier.transform.position.x;
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

  public void DropBall()
  {
    if (BombIsReady())
		{
	    Instantiate(selected_bomb, transform.position, transform.rotation);
      PublishBombDroppedEvent();
		}
  }

  public void AutoDrop()
  {
    if (BombIsReady())
    {
      Instantiate(selected_bomb, transform.position, transform.rotation);
      PublishBombDroppedEvent();
    }
  }

  private bool BombIsReady()
  {
    return BombIsReadyChecker.Instance().BombIsReady();
  }
    
  private void PublishBombDroppedEvent()
  {
    GameEvents.Publish(GameEvents.bomb_dropped_event);
  }
}
