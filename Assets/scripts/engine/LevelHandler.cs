using UnityEngine;
using System.Collections;

public class LevelHandler : PlinkyObject 
{
	public GameObject[] levels;
	private GameObject loader;
	private int current_level;

	void Start()
	{
		LoadRandomLevel ();
	}

	public void LoadRandomLevel()
	{
		if (!engine.disable_level_loading)
		{
		engine.score_tracker.ZeroGoals ();
		DestroyAllWithTag("bomb");
		DestroyAllWithTag("level");
		Destroy (loader);

		current_level = PickNewLevel();

		loader = GameObject.Instantiate (levels[current_level], 
			                                 levels[current_level].transform.position,
			                                 levels[current_level].transform.rotation) as GameObject;
		engine.score_tracker.CountGoals();
		}
	}

	public int PickNewLevel()
	{
		int new_level = current_level;
		new_level = Random.Range (0, levels.Length);

		if (new_level == current_level)
		{
			new_level = PickNewLevel();
		}

		return new_level;
	}

	public void DestroyAllWithTag(string target_tag)
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag(target_tag);

		foreach (GameObject element in objects)
		{
			Destroy (element.gameObject);
		}
	}
	
}
