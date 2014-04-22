using UnityEngine;
using System.Collections;

public class LevelChangeButton : MonoBehaviour 
{
	public Texture level_selection_texture;
	private Rect level_selection_rect;

	public Texture awards_texture;
	private Rect awards_rect;
	
	void OnGUI () 
	{
		LoadLevelSelectionButton();
		LoadAwardsButton ();
	}

	void LoadLevelSelectionButton()
	{
		level_selection_rect.width = (Screen.width / 3);
		level_selection_rect.height = (level_selection_rect.width / 2);
		level_selection_rect.x = (Screen.width / 2) - (level_selection_rect.width / 2);
		level_selection_rect.y = (Screen.height / 4) - (level_selection_rect.height / 2);

		//Debug.Log ("level_selection_rect.x: " + level_selection_rect.x + "Screen.width: " + Screen.width);

		if (GUI.Button (level_selection_rect, level_selection_texture, GUIStyle.none)) 
		{
			Debug.Log ("You clicked the button!");
			Application.LoadLevel ("menu_level_selection");
		}
	}

	void LoadAwardsButton()
	{
		awards_rect.width = level_selection_rect.width;
		awards_rect.height = level_selection_rect.height;
		awards_rect.x = level_selection_rect.x;
		awards_rect.y = level_selection_rect.y + (awards_rect.height * 1.5f);

		if (GUI.Button (awards_rect, awards_texture, GUIStyle.none)) 
		{
			Debug.Log ("You clicked the button!");
			Application.LoadLevel ("menu_level_selection");
		}
	}
}