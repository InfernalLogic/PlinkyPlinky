using UnityEngine;
using System.Collections;

public class PlinkyEngine : MonoBehaviour 
{
	public AudioHandler audio_handler;
	public LevelHandler level_handler;
	public ScoreTracker score_tracker;
	public PlayerStats player_stats;
  public UserInputScript user_input;
	public HUD hud;

	public bool disable_level_loading = false;

	static int player_prefs_found;

	void Awake()
	{
		Screen.SetResolution(960, 600, false, 60);

		player_prefs_found = PlayerPrefs.GetInt ("player_prefs_found", 0);

		if (player_prefs_found == 0)
		{
			player_stats.ResetStats ();
			player_prefs_found = 1;
			PlayerPrefs.SetInt("player_prefs_found", player_prefs_found);
			Debug.Log ("reset on load");
		}
	}
}