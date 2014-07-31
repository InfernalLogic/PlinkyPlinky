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

  private int coins_hit_by_this_bomb = 0;

  void Awake()
  {
    cooldown = bomb_cooldown;
    ++bomb_count;
    PublishBombDroppedEvent();
  }

	void Start () 
	{
    Destroy(gameObject, 45.0f);
  }

  void OnDestroy()
  {
    --bomb_count;
  }

  void OnTriggerEnter2D(Collider2D other_collider)
  {
    if (CollidedWithACoin(other_collider))
      ++coins_hit_by_this_bomb;

    if (coins_hit_by_this_bomb == 2)
    {
      GameEventPublisher.PublishMessage(GameEventPublisher.double_plink_event);
    }

  }

  private bool CollidedWithACoin(Collider2D other_collider)
  {
    return other_collider.gameObject.tag == "goal";
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
    GameEventPublisher.PublishMessage(GameEventPublisher.bomb_dropped_event);
  }
}
