using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour 
{
	static int goals_left = 0;
	static int bombs_dropped = 0;

	private LevelHandler level_handler;
	
	// Use this for initialization
	void Start () 
	{
		level_handler = GameObject.FindGameObjectWithTag("level_handler").GetComponent<LevelHandler>();
		CountGoals ();
		Debug.Log (goals_left + " counted on initialization");
	}

	static public void GoalHit()
	{
		ScoreTracker.CountGoals();
		Debug.Log ("Goals left: " + goals_left);

		if (goals_left <= 0)
		{		
			//Application.LoadLevel("menu_main");
			GameObject.FindGameObjectWithTag("level_handler").GetComponent<LevelHandler>().LoadRandomLevel();
			Debug.Log ("Game Won with " + bombs_dropped + " bombs dropped");
		}
	}

	static public void BombDropped()
	{
		++bombs_dropped;
		Debug.Log (bombs_dropped + " bombs dropped");
	}

	static private void CountGoals()
	{
		GameObject[] goal_counter = GameObject.FindGameObjectsWithTag("goal");
		goals_left = goal_counter.Length;
	}
}
