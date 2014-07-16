using UnityEngine;
using System.Collections;

public class BombScript : PlinkyObject 
{
	public float cooldown = 0f;

	void Start () 
	{
		AudioSource.PlayClipAtPoint(engine.audio_handler.GetBombDropSound(), Vector3.zero);  
    Destroy(gameObject, 25);
  }

	void OnBecameInvisible()
	{
		Destroy (gameObject);
	}

}
