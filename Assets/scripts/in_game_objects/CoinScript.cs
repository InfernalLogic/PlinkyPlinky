﻿using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour 
{
  public static Publisher<GameEvent> coin_hit_publisher = new Publisher<GameEvent>();

  [SerializeField]
  private ParticleSystem collision_emitter;

  private ParticleSystem emitter;

  void OnTriggerEnter2D (Collider2D other_collider)
	{
    if (CollidedWithABomb(other_collider))
		{
      LevelCompleteChecker.Instance().CoinHit();
      PublishCoinHitEvent();
      AudioSource.PlayClipAtPoint(AudioHandler.Instance().GetCoinHitSound(), Vector3.zero);
      emitter = Instantiate(collision_emitter, transform.position, transform.rotation) as ParticleSystem;
			Destroy (gameObject);
		}
	}

  private static void PublishCoinHitEvent()
  {
    coin_hit_publisher.PublishMessage(GameEventRegistry.Instance().FindEventByName("coin_hit_event"));
  }

  private static bool CollidedWithABomb(Collider2D other_collider)
  {
    return other_collider.gameObject.tag == "bomb";
  }
}