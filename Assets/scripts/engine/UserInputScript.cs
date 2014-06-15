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

	void OnGUI()
	{
		LoadInvisibleDropBombButton();
	}

	void Update () 
  {
    if (Input.GetButtonDown("Fire1"))
    {
				plinker.DropBomb();
    }
  }

	public void LoadInvisibleDropBombButton()
	{
		if (GUI.Button (new Rect(260, 0, 540, 600), "", GUIStyle.none))
		{
			plinker.DropBomb();
		}
	}
}