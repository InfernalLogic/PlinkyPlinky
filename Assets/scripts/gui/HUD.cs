using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HUD : PlinkyObject 
{
	public Texture hud_bg;
	private Rect hud_bg_rect;

	public Texture default_box;
	private Rect score_rect;
	private string current_money = "";
	private string career_money = "";
	private string coin_upgrade_cost = "";

	/*
	void Awake()
	{
		LoadEngine();
		UpdateMoney();
	}*/

	void OnGUI()
	{
		LoadHUDBG();
		LoadRandomLevelButton();
		LoadScoreDisplay();
		LoadCoinUpgradeButton();
		LoadResetButton();
		LoadPegUpgradeButton();
		LoadBumperUpgradeButton();
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
		GUI.Label(new Rect((Screen.width - 130), 60, 130, 50), "Coin Upgrade: " + coin_upgrade_cost);
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
		coin_upgrade_cost = engine.player_stats.GetCoinUpgradeCost().ToString();
	}

	public void LoadCoinUpgradeButton()
	{
		if (GUI.Button (new Rect(0, 0, 150, 50), "Coin up\n" + engine.player_stats.GetCoinUpgradeCost()))
		{
			engine.player_stats.UpgradeCoin();
		}
	}

	public void LoadPegUpgradeButton()
	{
		if (GUI.Button (new Rect(0, 50, 150, 50), "Peg upgrade\n" + engine.player_stats.GetPegUpgradeCost()))
		{
			engine.player_stats.UpgradePeg();
		}
	}

	public void LoadBumperUpgradeButton()
	{
		if (GUI.Button (new Rect(0, 100, 150, 50), "Bumper upgrade\n" + engine.player_stats.GetBumperUpgradeCost()))
		{
			engine.player_stats.UpgradeBumper();
		}
	}

	public void LoadResetButton()
	{
		if (GUI.Button (new Rect(50, (Screen.height - 50), 50, 50), "Reset"))
		{
			engine.player_stats.ResetStats();
		}
	}
}
