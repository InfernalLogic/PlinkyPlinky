using UnityEngine;
using System.Collections;

public class BombScript : PlinkyObject 
{
	public float cooldown = 0f;

	void Start () 
	{
		AudioSource.PlayClipAtPoint(engine.audio_handler.GetBombDropSound(), gameObject.transform.position);
    Destroy(gameObject, 25);
  }

	void OnBecameInvisible()
	{
		Destroy (gameObject);
	}

}
