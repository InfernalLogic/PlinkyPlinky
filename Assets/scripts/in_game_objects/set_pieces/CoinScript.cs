using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour 
{
  [SerializeField]
  private GameEvent coin_hit_event;
  [SerializeField]
  private ParticleSystem collision_emitter;

  private ParticleSystem emitter;

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
    emitter = Instantiate(collision_emitter, transform.position, transform.rotation) as ParticleSystem;
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
}
