using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour 
{
	public float cooldown = 0f;

	// Use this for initialization
	void Start () 
	{

    Destroy(gameObject, 25);
  }
	
	void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.tag == "bomb_destroy_trigger")
		{
			Destroy(gameObject);
			Debug.Log ("destroy trigger tripped");
		}
	}

}
