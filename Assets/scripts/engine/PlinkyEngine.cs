using UnityEngine;
using System.Collections;

public class PlinkyEngine : MonoBehaviour 
{
	public AudioHandler audio_handler;
	public LevelHandler level_handler;
	public LevelCompleteChecker level_complete_checker;
	public PlayerStats player_stats;
  public UserInputScript user_input;
  public AchievementTracker achievement_tracker;
	public HUD hud;

	public bool disable_level_loading = false;
  public bool reset_on_load = false;

	static int player_prefs_found;

	void Awake()
	{
    if (reset_on_load)
    {
      PlayerPrefs.DeleteAll();
      player_stats.ResetStats();
    }

		Screen.SetResolution(960, 600, false, 60);
	}

  private void DisplayInstructionMask()
  {
    
  }

}