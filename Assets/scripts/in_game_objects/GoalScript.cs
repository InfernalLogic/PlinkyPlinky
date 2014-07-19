using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour 
{
  [SerializeField]
  private ParticleSystem collision_emitter;

  private ParticleSystem emitter;

  void OnTriggerEnter2D (Collider2D other_collider)
	{
		if (other_collider.gameObject.tag == "bomb")
		{
      LevelCompleteChecker.Instance().CoinHit();
      AudioSource.PlayClipAtPoint(AudioHandler.Instance().GetCoinHitSound(), Vector3.zero);
      emitter = Instantiate(collision_emitter, transform.position, transform.rotation) as ParticleSystem;
			Destroy (gameObject);
		}
	}
}
