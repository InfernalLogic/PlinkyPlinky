using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour 
{
  [SerializeField]
  private GameEvent bomb_dropped_event;
  [SerializeField]
  private float bomb_cooldown = 0.0f;

	private static float cooldown = 1.0f;

  private static int bomb_count = 0;

  void Awake()
  {
    cooldown = bomb_cooldown;
    ++bomb_count;
    PublishBombDroppedEvent();
  }

	void Start () 
	{
    Destroy(gameObject, 30.0f);
  }

  void OnDestroy()
  {
    --bomb_count;
  }

	void OnBecameInvisible()
	{
		Destroy (gameObject);
	}

  public static int GetBombCount()
  {
    return bomb_count;
  }

  public static float GetBaseCooldown()
  {
    return cooldown;
  }

  private void PublishBombDroppedEvent()
  {
    GameEventPublisher.Instance().PublishMessage(GameEventPublisher.Instance().bomb_dropped_event);
  }
}
