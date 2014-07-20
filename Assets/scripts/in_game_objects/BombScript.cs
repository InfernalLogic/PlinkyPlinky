using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour 
{
  public static Publisher<GameEvent> bomb_dropped_publisher = new Publisher<GameEvent>();

  [SerializeField]
	private static float cooldown = 0f;

  private static int bomb_count = 0;

  void Awake()
  {
    ++bomb_count;
    PublishBombDroppedEvent();
  }

	void Start () 
	{
		AudioSource.PlayClipAtPoint(AudioHandler.Instance().GetBombDropSound(), Vector3.zero);  
    Destroy(gameObject, 25);
  }

	void OnBecameInvisible()
	{
    --bomb_count;
		Destroy (gameObject);
	}

  public static int GetBombCount()
  {
    return bomb_count;
  }

  public static float GetCooldown()
  {
    return cooldown;
  }

  private static void PublishBombDroppedEvent()
  {
    bomb_dropped_publisher.PublishMessage(bomb_dropped_event);
  }
}
