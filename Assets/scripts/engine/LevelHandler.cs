using UnityEngine;
using System.Collections;

public class LevelHandler : PlinkyObject 
{
	public GameObject[] levels;
	public ArrayList unlocked_levels = new ArrayList();

	private GameObject loader;
	private int current_level;

	void Start()
	{
		LoadUnlockedLevels();

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
		int new_level = (int)unlocked_levels[Random.Range (0, unlocked_levels.Count)];

		if (new_level == current_level && unlocked_levels.Count > 1)
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

	public void LoadUnlockedLevels()
	{
		unlocked_levels.Clear ();

		int i = 0;

		foreach (GameObject element in levels)
		{
			if (element.gameObject.GetComponent<LevelUpgrader>().IsUnlocked())
			{
				unlocked_levels.Add (i);
			}
			Debug.Log (element.gameObject.name + " unlocked set to " + element.gameObject.GetComponent<LevelUpgrader>().IsUnlocked());
			++i;
		}
	}

	public int GetCurrentLevel()
	{
		return current_level;
	}
}
