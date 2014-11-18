using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour 
{
  [SerializeField]
  private GameEvent coin_hit_event;
  [SerializeField]
  private ParticleSystem collision_emitter;
  
  private PlinkagonUpgrade coin_critical_upgrader;

  void Awake()
  {
    coin_critical_upgrader = GameObject.Find("coin_critical_upgrader").GetComponent<PlinkagonUpgrade>();
  }

  void OnTriggerEnter2D (Collider2D other_collider)
	{
    if (CollidedWithABomb(other_collider))
		{
      other_collider.GetComponent<BombScript>().IncrementCoinsHit();
      HitCoin();
		}
	}

  public void HitCoin()
  {
    int multiplier = RollMultiplierProcs();
    Instantiate(collision_emitter, transform.position, transform.rotation);
    PublishCoinHitEvent(multiplier);

    if (multiplier > 1)
    {
      CoinMultiplierFloatingTextFactory.Instance().GenerateMultiplierPopup(multiplier, transform.position);
    }

    Destroy(gameObject);
  }

  private void PublishCoinHitEvent(int multiplier)
  {
    GameEvents.Publish(new CoinHitEvent(multiplier));
  }

  private static bool CollidedWithABomb(Collider2D other_collider)
  {
    return other_collider.gameObject.tag == "bomb";
  }

  private int RollMultiplierProcs()
  {
    return coin_critical_upgrader.RollProcs(0) + 1;
  }
}
