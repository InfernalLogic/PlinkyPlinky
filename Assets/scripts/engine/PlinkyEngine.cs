using UnityEngine;
using System.Collections;

public class PlinkyEngine : MonoBehaviour 
{
	public AudioHandler audio_handler;
	public LevelHandler level_handler;
	public ScoreTracker score_tracker;
	public PlayerStats player_stats;
	public HUD hud;

	void Start()
	{
		if (hud == null)
		{
			Debug.Log ("hud failed to load in engine");
		}
	}
}
