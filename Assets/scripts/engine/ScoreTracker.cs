using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour 
{
	static int goals_left = 0;
	static int bombs_dropped = 0;

	// Use this for initialization
	public void Start () 
	{
		CountGoals ();
		Debug.Log (goals_left + " counted on initialization");
	}

	static public void GoalHit()
	{
		ScoreTracker.CountGoals();
		Debug.Log ("Goals left: " + goals_left);

		//can't get this to work unless it's set to 1...
		//CountGoals() keeps including the goal that just got hit, so it never goes below 1.
		//if I put it in OnDestroy(), then I get funky spawning behaviour.
		if (goals_left <= 1)
		{		
			//Application.LoadLevel("menu_main");
			GameObject.FindGameObjectWithTag("level_handler").GetComponent<LevelHandler>().LoadRandomLevel();
			Debug.Log ("Game Won with " + bombs_dropped + " bombs dropped");
		}
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
	}
}
