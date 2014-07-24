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
  private float cooldown_timer = 0.0f;

	private Animator bumper_animator;

  private Vector2 bump_vector;

	void Awake()
	{
		bumper_animator = GetComponent<Animator> ();
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
    if (CollidedWithABomb(collision))
		{
			if (IsCooledDown())
			{
				ResetCooldownTimer ();
        PlayBumperHitAnimation();
        ApplyBumperForceToBomb(collision);

        GameEventPublisher.Instance().PublishMessage(GameEventPublisher.Instance().bumper_hit_event);
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

	void ResetCooldownTimer()
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
