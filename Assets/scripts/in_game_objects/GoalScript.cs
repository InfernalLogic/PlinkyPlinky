using UnityEngine;
using System.Collections;

public class GoalScript : PlinkyObject 
{
  void OnTriggerEnter2D (Collider2D other_collider)
	{
		if (other_collider.gameObject.tag == "bomb")
		{
			//AudioSource.PlayClipAtPoint (audio.clip, gameObject.transform.position);
			engine.score_tracker.GoalHit();
			Destroy (gameObject);
		}
	}
}
