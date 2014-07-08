using UnityEngine;
using System.Collections;

public class ScoreTicker : HUDField
{
  [SerializeField]
  GUIStyle label_style;

	protected override void DisplayGUIElements()
	{
		DisplayBackgroundBox ();
		
		DisplayCurrentMoney ();

		DisplayCurrentLevel ();
	}

	void DisplayBackgroundBox ()
	{
    GUI.Box(new Rect(0, 0, display_rect.GetRect().width, display_rect.GetRect().height), "", background_style);
	}

	void DisplayCurrentMoney ()
	{
    GUI.Label(new Rect(0, 10, display_rect.GetRect().width, display_rect.GetRect().height / 2), 
		           "Current money: \n" + engine.player_stats.GetCurrentMoney (), label_style);
	}

	void DisplayCurrentLevel ()
	{
    GUI.Label(new Rect(0, display_rect.GetRect().height / 2, display_rect.GetRect().width, display_rect.GetRect().height / 2),
               "Current level: " + engine.level_handler.GetCurrentLevel(), label_style);
	}
}
