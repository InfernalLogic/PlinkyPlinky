using UnityEngine;
using System.Collections;

public class GoalScript : PlinkyObject 
{
  void OnTriggerEnter2D (Collider2D other_collider)
	{
		if (other_collider.gameObject.tag == "bomb")
		{
			engine.score_tracker.GoalHit();
			AudioSource.PlayClipAtPoint(engine.audio_handler.GetCoinHitSound(), gameObject.transform.position);
			Destroy (gameObject);
		}
	}
}
