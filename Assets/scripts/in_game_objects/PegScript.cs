using UnityEngine;
using System.Collections;

public class PegScript : MonoBehaviour 
{
  public static Publisher<GameEvent> peg_hit_publisher = new Publisher<GameEvent>();
  public static GameEvent peg_hit_event;

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
      PlayPegHitSound();
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

  private static void PublishPegHitEvent()
  {
    peg_hit_publisher.PublishMessage(GameEventRegistry.Instance().FindEventByName("peg_hit_event"));
  }

  private void SpawnParticleEmitter()
  {
    emitter = Instantiate(collision_emitter, transform.position, transform.rotation) as ParticleSystem;
  }

  private void PlayPegHitSound()
  {
    AudioSource.PlayClipAtPoint(AudioHandler.Instance().GetPopSound(), gameObject.transform.position);
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
