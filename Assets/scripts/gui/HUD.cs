using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HUD : MonoBehaviour 
{
	public Texture hud_bg;
	private Rect hud_bg_rect;

	public Texture default_box;
	private Rect score_rect;
	private string score = "";

	private PlayerStats player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("player_stats").GetComponent<PlayerStats>();
	}

	void Update()
	{
		score = player.GetCurrentMoney().ToString();
	}

	void OnGUI()
	{
		LoadHUDBG();
		LoadRandomLevelButton();
		LoadScoreDisplay();
	}

	void LoadHUDBG()
	{
		hud_bg_rect.width = (Screen.width);
		hud_bg_rect.height = (Screen.height);
		hud_bg_rect.x = 0f;
		hud_bg_rect.y = 0f;

		GUI.DrawTexture(hud_bg_rect, hud_bg);
	}

	void LoadRandomLevelButton()
	{
		if (GUI.Button (new Rect (0, (Screen.height - 50), 50, 50), "  New\nLevel"))
		{
			GameObject.FindGameObjectWithTag("level_handler").GetComponent<LevelHandler>().LoadRandomLevel();
		}
	}

	void LoadScoreDisplay()
	{
		GUI.Label(new Rect(0, 0, 100, 50), score);
	}
}
