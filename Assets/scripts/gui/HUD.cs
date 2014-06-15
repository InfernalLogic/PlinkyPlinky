using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HUD : PlinkyObject 
{
	public Texture hud_bg;

	public Texture coin_upgrade_icon;
	public Texture bumper_upgrade_icon;
	public Texture peg_upgrade_icon;

	private Rect hud_bg_rect;
	
	private Rect score_rect;
	private string current_money = "";
	private string career_money = "";


	void OnGUI()
	{
		LoadHUDBG();
		LoadRandomLevelButton();
		LoadScoreDisplay();
		LoadCoinUpgradeButton();
		LoadResetButton();
		LoadPegUpgradeButton();
		LoadBumperUpgradeButton();
		LoadCheatButton();
		LoadUnlockLevel03Button();
	}

	void LoadHUDBG()
	{
		hud_bg_rect.width = (Screen.width);
		hud_bg_rect.height = (Screen.height);
		hud_bg_rect.x = 0f;
		hud_bg_rect.y = 0f;

		GUI.DrawTexture(hud_bg_rect, hud_bg);
	}

	void LoadScoreDisplay()
	{
		GUI.Label(new Rect((Screen.width - 130), 0, 130, 50), "Current money: " + current_money);
		GUI.Label(new Rect((Screen.width - 130), 30, 130, 50), "Career money: " + career_money);

		GUI.Label(new Rect((Screen.width/2 - 130), 0, 130, 50), "Current level: " + engine.level_handler.GetCurrentLevel());
	}

	void LoadRandomLevelButton()
	{
		if (GUI.Button (new Rect (0, (Screen.height - 50), 50, 50), "  New\nLevel"))
		{
			GameObject.FindGameObjectWithTag("level_handler").GetComponent<LevelHandler>().LoadRandomLevel();
		}
	}

	public void UpdateMoney()
	{
		current_money = engine.player_stats.GetCurrentMoney().ToString();
		career_money = engine.player_stats.GetCareerMoney().ToString();
	}

	public void LoadCoinUpgradeButton()
	{
		if (GUI.Button (new Rect(3, 5, 75, 75), coin_upgrade_icon, GUIStyle.none))
		{
			engine.player_stats.coin_upgrader.Upgrade();
		}
	}

	public void LoadPegUpgradeButton()
	{
		if (GUI.Button (new Rect(3, 75, 75, 75), peg_upgrade_icon, GUIStyle.none))
		{
			engine.player_stats.peg_upgrader.Upgrade();
		}
	}

	public void LoadBumperUpgradeButton()
	{
		if (GUI.Button (new Rect(3, 150, 75, 75), bumper_upgrade_icon, GUIStyle.none))
		{
			engine.player_stats.bumper_upgrader.Upgrade();
		}
	}

	public void LoadResetButton()
	{
		if (GUI.Button (new Rect(50, (Screen.height - 50), 50, 50), "Reset"))
		{
			engine.player_stats.ResetStats();
		}
	}

	public void LoadCheatButton()
	{
		if (GUI.Button (new Rect(100, (Screen.height - 50), 50, 50), "Cheat"))
		{
			engine.player_stats.AddMoney(1000);
		}
	}

	public void LoadUnlockLevel03Button()
	{
		if (GUI.Button (new Rect(0, (Screen.height - 100), 150, 50), "Unlock Level 3"))
		{
			engine.level_handler.levels[3].GetComponent<LevelUpgrader>().Upgrade ();
		}
	}


}
