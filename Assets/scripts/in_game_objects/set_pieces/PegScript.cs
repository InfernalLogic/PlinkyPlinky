using UnityEngine;
using System.Collections.Generic;

public class PegScript : MonoBehaviour 
{
  private static PlinkagonUpgrade peg_hunter_upgrade;

  [SerializeField]
  private GameEvent peg_hit_event;
  [SerializeField]
	private int hit_points = 1;
  [SerializeField]
  private ParticleSystem collision_emitter;

	void OnCollisionEnter2D(Collision2D collision)
	{
    if (CollidedWithABomb(collision) || CollidedWithAPeg(collision))
      DestroyPeg();
	}

  private void PublishPegHitEvent()
  {
    GameEvents.Publish(GameEvents.peg_hit_event);
  }

  private void SpawnParticleEmitter()
  {
    Instantiate(collision_emitter, transform.position, transform.rotation);
  }

  private static bool CollidedWithABomb(Collision2D collision)
  {
    return collision.gameObject.tag == "bomb";
  }

  private static bool CollidedWithAPeg(Collision2D collision)
  {
    return collision.gameObject.tag == "peg";
  }

	void OnTriggerEnter2D(Collider2D collider)
	{
    if (collider.gameObject.tag == "peg_destroy_trigger")
			Destroy (gameObject);
    if (collider.gameObject.tag == "peg_hunter")
    {
      Destroy(collider.gameObject);
      DestroyPeg();
    }
	}

  private void DestroyPeg()
  {
    SpawnParticleEmitter();
    PublishPegHitEvent();
    
    Destroy(gameObject);
  }
}
