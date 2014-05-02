using UnityEngine;
using System.Collections;

public class PlinkyEngine : MonoBehaviour 
{
	public AudioHandler audio_handler;
	public LevelHandler level_handler;
	public ScoreTracker score_tracker;
	public PlayerStats player_stats;
	public HUD hud;

	public bool disable_level_loading = false;

	void Awake()
	{
		Screen.SetResolution(960, 600, false, 60);

		if (hud == null)
		{
			Debug.Log ("hud not loaded");
		}
	}
}