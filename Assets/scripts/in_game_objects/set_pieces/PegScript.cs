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
  [SerializeField]
  private GameObject peg_hunter;

  private int peg_hunters_spawned = 0;
  private Stack<PegHunter> incoming_peg_hunters = new Stack<PegHunter>();
    

  void Awake()
  {
    if (!peg_hunter_upgrade)
    {
      peg_hunter_upgrade = GameObject.FindGameObjectWithTag("peg_hunter_upgrade").GetComponent<PlinkagonUpgrade>();
    }
  }

	void OnCollisionEnter2D(Collision2D collision)
	{
    if (CollidedWithABomb(collision) || CollidedWithAPeg(collision))
      PegHit(collision);
	}

  private void PegHit(Collision2D collision)
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

  void SpawnNewPegHunter()
  {
    GameObject new_peg_hunter = Instantiate(peg_hunter, this.transform.position, this.transform.rotation) as GameObject;
    new_peg_hunter.GetComponent<PegHunter>().FindNewTarget(this.gameObject);
  }

  private void DestroyPeg()
  {
    SpawnParticleEmitter();
    PublishPegHitEvent();

    while (incoming_peg_hunters.Count > 0)
    {
      incoming_peg_hunters.Pop().FindNewTarget(this.gameObject);
    }
    
    Destroy(gameObject);
  }
}
