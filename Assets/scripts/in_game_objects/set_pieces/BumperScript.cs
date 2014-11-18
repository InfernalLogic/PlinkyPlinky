using UnityEngine;

public class BumperScript : MonoBehaviour 
{
  [SerializeField]
  private GameEvent bumper_hit_event;
  [SerializeField]
	private float bump_strength = 0.0f;
  [SerializeField]
  private float bump_cooldown = 0.0f;
  [SerializeField]
  private Timer cooldown_timer;
  [SerializeField]
	private Animator bumper_animator;

  private Vector2 bump_vector;

	void OnCollisionEnter2D (Collision2D collision)
	{
    if (CollidedWithABomb(collision))
		{
			if (cooldown_timer.IsExpired())
			{
				cooldown_timer.Reset();
        PlayBumperHitAnimation();
        ApplyBumperForceToBomb(collision);

        GameEvents.Publish(GameEvents.bumper_hit_event);
			}
		}
	}

  private void PlayBumperHitAnimation()
  {
    bumper_animator.SetTrigger("hit_trigger");
  }

  private void ApplyBumperForceToBomb(Collision2D collision)
  {
    ContactPoint2D[] contacts = collision.contacts;

    foreach (ContactPoint2D element in contacts)
    {
      bump_vector = (-1 * element.normal * bump_strength);
      element.collider.attachedRigidbody.AddForce(bump_vector);
    }
  }

  private static bool CollidedWithABomb(Collision2D collision)
  {
    return collision.gameObject.tag == "bomb";
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.gameObject.tag == "bumper_destroy_trigger")
    {
      Destroy(gameObject);
    }
  }
}
