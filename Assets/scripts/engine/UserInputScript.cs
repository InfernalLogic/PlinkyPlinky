using UnityEngine;
using System.Collections;

public class UserInputScript : MonoBehaviour {

	private PlinkerScript plinker = null;
	private GameObject plinker_object = null;
  private Rect play_field_rect;

	void Start()
	{
		plinker_object = GameObject.FindGameObjectWithTag("plinker");
		plinker = plinker_object.GetComponent<PlinkerScript>();
    InitializePlayFieldRect();
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
		if (GUI.Button (play_field_rect, "", GUIStyle.none))
		{
			plinker.DropBomb();
		}
	}

  private void InitializePlayFieldRect()
  {
    play_field_rect.x = 0;
    play_field_rect.y = 0;
    play_field_rect.width = (Screen.width - Screen.width / 3);
    play_field_rect.height = Screen.height;
  }

  public Rect GetPlayFieldRect()
  {
    return play_field_rect;
  }
}