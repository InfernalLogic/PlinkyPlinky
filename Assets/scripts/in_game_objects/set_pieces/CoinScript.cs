using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour 
{
  [SerializeField]
  private GameEvent coin_hit_event;
  [SerializeField]
  private ParticleSystem collision_emitter;

  private PlinkagonUpgrade critical_coin_upgrader;

  void Awake()
  {
    critical_coin_upgrader = GameObject.Find("critical_coin_upgrader").GetComponent<PlinkagonUpgrade>();
  }

  void OnTriggerEnter2D (Collider2D other_collider)
	{
    if (CollidedWithABomb(other_collider))
		{
      HitCoin();
      other_collider.GetComponent<BombScript>().IncrementCoinsHit();
		}
	}

  public void HitCoin()
  {
    PublishCoinHitEvent();
    Instantiate(collision_emitter, transform.position, transform.rotation);
    Destroy(gameObject);
  }

  private void PublishCoinHitEvent()
  {
    GameEvents.Publish(GameEvents.coin_hit_event);
  }

  private static bool CollidedWithABomb(Collider2D other_collider)
  {
    return other_collider.gameObject.tag == "bomb";
  }

  private bool RolledToCriticalCoin()
  {
    return Random.Range(0.0f, 100.0f) <= critical_coin_upgrader.GetChanceToProc();
  }
}
