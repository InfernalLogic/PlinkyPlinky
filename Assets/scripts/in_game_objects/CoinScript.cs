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
    Debug.Log("coin_hit_event published");
    GameEventPublisher.Instance().PublishMessage(GameEventPublisher.Instance().coin_hit_event);
  }

  private static bool CollidedWithABomb(Collider2D other_collider)
  {
    return other_collider.gameObject.tag == "bomb";
  }
}
