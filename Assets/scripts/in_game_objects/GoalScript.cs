using UnityEngine;
using System.Collections;

public class GoalScript : PlinkyObject 
{
  [SerializeField]
  private ParticleSystem collision_emitter;

  private ParticleSystem emitter;

  void OnTriggerEnter2D (Collider2D other_collider)
	{
		if (other_collider.gameObject.tag == "bomb")
		{
			engine.level_complete_checker.CoinHit();
      AudioSource.PlayClipAtPoint(engine.audio_handler.GetCoinHitSound(), Vector3.zero);
      emitter = Instantiate(collision_emitter, transform.position, transform.rotation) as ParticleSystem;
			Destroy (gameObject);
		}
	}
}
