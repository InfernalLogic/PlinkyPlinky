using UnityEngine;
using System.Collections;

public class PegScript : PlinkyObject 
{
  [SerializeField]
	private int hp = 1;
  [SerializeField]
  private bool is_destructible = true;
  [SerializeField]
  private ParticleSystem collision_emitter;

  private ParticleSystem emitter;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "bomb")
		{
			AudioSource.PlayClipAtPoint(engine.audio_handler.GetPopSound(), gameObject.transform.position);
      emitter = Instantiate(collision_emitter, transform.position, transform.rotation) as ParticleSystem;

			if (is_destructible)
			{
				engine.player_stats.PegHit();
				--hp;
				if (hp <= 0)
				{
					Destroy (gameObject);
				}
			}

		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "peg_destroy_trigger")
		{
			Destroy (gameObject);
		}
	}
}
