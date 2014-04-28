using UnityEngine;

public class BumperScript : PlinkyObject 
{
	public float bump_strength = 0f;
	public float bump_cooldown = 0f;

	private Animator bumper_animator;
	private float cooldown_timer = 0f;

	void Start()
	{
		bumper_animator = GetComponent<Animator> ();
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		//bamp
		if (collision.gameObject.tag == "bomb")
		{
			if (IsCooledDown())
			{
				SetCooldownTimer ();			
				bumper_animator.SetTrigger ("hit_trigger");
				//Debug.Log ("hit_trigger set");
				AudioSource.PlayClipAtPoint(engine.audio_handler.GetBumperHitSound(), gameObject.transform.position);
			
				ContactPoint2D[] contacts = collision.contacts;
				Vector2 bump_vector;
				foreach (ContactPoint2D element in contacts)
				{
					bump_vector = (-1 * element.normal * bump_strength);
					element.collider.attachedRigidbody.AddForce(bump_vector);
				}

				engine.player_stats.BumperHit();
			}

		}
	}

	void SetCooldownTimer()
	{
		cooldown_timer = Time.time + bump_cooldown;
	}

	bool IsCooledDown()
	{
		if (Time.time >= cooldown_timer) {
			return true;
		} 
		else 
		{
			return false;
		}
	}
}
