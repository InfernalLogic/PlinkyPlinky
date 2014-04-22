using UnityEngine;
using System.Collections;

public class GuiTest : MonoBehaviour 
{

	void OnGUI()
	{
		GUI.Box(new Rect(250, 250, 200, 180), "Loader Menu");

		if(GUI.Button (new Rect(270, 270, 80, 20), "Level PoopS"))
		{
			Debug.Log ("PoopS");
		}
	}
}
