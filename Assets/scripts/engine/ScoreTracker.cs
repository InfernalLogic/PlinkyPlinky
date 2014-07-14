using UnityEngine;
using System.Collections;

public class ScoreTracker : PlinkyObject 
{
	public float load_new_level_delay = 0f;

	int goals_left = 0;
	int bombs_dropped = 0;

	float load_new_level_countdown;
	bool level_completed;
	
	void Start () 
	{
		CountGoals ();

		Debug.Log (goals_left + " counted on initialization");

		level_completed = false;
		load_new_level_countdown = Time.time;
	}

	void Update()
	{
		if (goals_left <= 0)
		{
			if (level_completed == true && Time.time >= load_new_level_countdown)
			{
				engine.level_handler.LoadRandomLevel();
			}

			if (level_completed == false)
			{
			level_completed = true;
			Debug.Log ("Game Won with " + bombs_dropped + " bombs dropped");
				bombs_dropped = 0;
			load_new_level_countdown = Time.time + load_new_level_delay;
			}
		}
		else if (goals_left >= 0 && level_completed)
		{
			level_completed = false;
		}
	}

	public void GoalHit()
	{
		CountGoals();
		engine.player_stats.CoinHit();
		--goals_left;
    Debug.Log("Coins left: " + goals_left);
	}

	public void BombDropped()
	{
		++bombs_dropped;
	}

	public void CountGoals()
	{
		SetGoalsLeftToZero();
		GameObject[] goal_counter = GameObject.FindGameObjectsWithTag("goal");
		goals_left = goal_counter.Length;

    Debug.Log("Counted " + goals_left + " goals");
	}
	
	public void SetGoalsLeftToZero()
	{
		goals_left = 0;
	}
}
