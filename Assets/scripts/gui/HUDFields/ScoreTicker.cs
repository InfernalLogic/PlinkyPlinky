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
		           "$: " + engine.player_stats.GetCurrentMoney (), label_style);
	}

	void DisplayCurrentLevel ()
	{
    GUI.Label(new Rect(0, display_rect.GetRect().height / 2 - 5, display_rect.GetRect().width, display_rect.GetRect().height / 2),
               "Playing stage: " + engine.level_handler.GetCurrentLevel(), label_style);
	}
}
