using UnityEngine;
using System.Collections;

public class PegScript : PlinkyObject 
{
	public int hp = 1;
	public bool is_destructible = true;

	private AudioHandler audio_handler;
	

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "bomb")
		{
			//AudioSource.PlayClipAtPoint (audio.clip, gameObject.transform.position);
			AudioSource.PlayClipAtPoint(engine.audio_handler.GetPopSound(), gameObject.transform.position);

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
