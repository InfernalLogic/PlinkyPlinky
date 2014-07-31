using UnityEngine;
using System.Collections;

public class PegScript : MonoBehaviour 
{
  [SerializeField]
  private GameEvent peg_hit_event;
  [SerializeField]
	private int hit_points = 1;
  [SerializeField]
  private bool is_destructible = true;
  [SerializeField]
  private ParticleSystem collision_emitter;

  private ParticleSystem emitter;

	void OnCollisionEnter2D(Collision2D collision)
	{
    if (CollidedWithABomb(collision))
		{
			if (is_destructible)
			{
				--hit_points;
				if (hit_points <= 0)
				{
          SpawnParticleEmitter();
          PublishPegHitEvent();
					Destroy (gameObject);
				}
			}
		}
	}

  private void PublishPegHitEvent()
  {
    GameEventPublisher.PublishMessage(GameEventPublisher.peg_hit_event);
  }

  private void SpawnParticleEmitter()
  {
    emitter = Instantiate(collision_emitter, transform.position, transform.rotation) as ParticleSystem;
  }

  private static bool CollidedWithABomb(Collision2D collision)
  {
    return collision.gameObject.tag == "bomb";
  }

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "peg_destroy_trigger")
		{
			Destroy (gameObject);
		}
	}
}
