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
  [SerializeField]
  private PlinkagonUpgrade peg_hunter_upgrade;
  [SerializeField]
  private GameObject peg_hunter;

  private int peg_hunters_spawned = 0;

	void OnCollisionEnter2D(Collision2D collision)
	{
    if (CollidedWithABomb(collision))
      PegHit(collision);
	}

  private void PegHit(Collision2D collision)
  {
    if (is_destructible)
    {
      --hit_points;

      if (hit_points <= 0)
      {
        peg_hunters_spawned = peg_hunter_upgrade.RollProcs(0);

        if (peg_hunters_spawned > 0)
        {
          for (int i = 0; i < peg_hunters_spawned; ++i)
          {
            SpawnNewPegHunter();
          }
        }
        DestroyPeg();
      } 
    }
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

  void SpawnNewPegHunter()
  {
    GameObject new_peg_hunter = Instantiate(peg_hunter, this.transform.position, this.transform.rotation) as GameObject;
  }

  private void DestroyPeg()
  {
    SpawnParticleEmitter();
    PublishPegHitEvent();
    Destroy(gameObject);
  }
}
