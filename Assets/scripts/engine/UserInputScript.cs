using UnityEngine;
using System.Collections;

public class UserInputScript : MonoBehaviour {

	private PlinkerScript plinker = null;
	private GameObject plinker_object = null;

	void Start()
	{
		plinker_object = GameObject.FindGameObjectWithTag("plinker");
		plinker = plinker_object.GetComponent<PlinkerScript>();
	}

	// Update is called once per frame
	void Update () 
  {
    if (Input.GetButtonDown("Fire1"))
    {
      plinker.DropBomb();
    }
  }


}