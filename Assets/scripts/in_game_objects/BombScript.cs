using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour 
{
	public float cooldown = 0f;

	void Start () 
	{
		AudioSource.PlayClipAtPoint(AudioHandler.Instance().GetBombDropSound(), Vector3.zero);  
    Destroy(gameObject, 25);
  }

	void OnBecameInvisible()
	{
		Destroy (gameObject);
	}

}
