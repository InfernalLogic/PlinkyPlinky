using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour 
{
	static int goals_left = 0;
	static int bombs_dropped = 0;
	static PlayerStats player;

	// Use this for initialization
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("player_stats").GetComponent<PlayerStats>();
	}

	void Start () 
	{
		CountGoals ();
		Debug.Log (goals_left + " counted on initialization");
	}

	void Update()
	{
		if (goals_left <= 0)
		{
			Debug.Log ("Game Won with " + bombs_dropped + " bombs dropped");
			GameObject.FindGameObjectWithTag("level_handler").GetComponent<LevelHandler>().LoadRandomLevel();
		}
	}

	static public void GoalHit()
	{
		ScoreTracker.CountGoals();
		player.CoinHit();
		--goals_left;
		Debug.Log ("Goals left: " + goals_left);
	}

	static public void BombDropped()
	{
		++bombs_dropped;
		//Debug.Log (bombs_dropped + " bombs dropped");
	}

	static public void CountGoals()
	{
		GameObject[] goal_counter = GameObject.FindGameObjectsWithTag("goal");
		goals_left = goal_counter.Length;
		Debug.Log ("Goals counted: " + goals_left);
	}
	
	static public void ZeroGoals()
	{
		goals_left = 0;
		Debug.Log ("Goals set to: " + goals_left);
	}
}
