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

  private void OnEnable()
  {
    Events.ResetEvents += OnReset;
  }

  private void OnDisable()
  {
    Events.ResetEvents -= OnReset;
  }

  private void OnReset(ResetType type)
  {
    transform.position = starting_position;
  }

  void Start()
  {
		movement_vector.x = movement_speed;

    InitializeCooldownColors();

    starting_position = transform.position;

    StartCoroutine(OutOfBoundsCheck());
  }

	void Update()
	{
    MovePlinker();

    if (BombIsReadyChecker.Instance.BombIsReady())
      GetComponent<Renderer>().material.color = bomb_ready_color;
    else
      GetComponent<Renderer>().material.color = bomb_cooling_down_color;
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
    bomb_ready_color = GetComponent<Renderer>().material.color;
    bomb_cooling_down_color = GetComponent<Renderer>().material.color;
    bomb_cooling_down_color.a = bomb_cooling_down_alpha;
  }

  private void MovePlinker()
  {
    transform.position += movement_vector * Time.deltaTime;
  }

	void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.tag == "plinker_reverse_trigger")
			ReverseDirection ();
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
    DropBall();
  }

  private bool BombIsReady()
  {
    return BombIsReadyChecker.Instance.BombIsReady();
  }
    
  private void PublishBombDroppedEvent()
  {
    GameEvents.Publish(GameEvents.bomb_dropped_event);
  }
}
