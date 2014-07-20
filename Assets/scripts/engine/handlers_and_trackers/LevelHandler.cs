using UnityEngine;
using System.Collections;

public class LevelHandler : Singleton<LevelHandler> 
{
  [SerializeField]
  private bool disable_level_loading = false;

	public GameObject[] levels;
	public ArrayList unlocked_levels = new ArrayList();

	private GameObject loader;
	private int current_level;

	void Start()
	{
    CheckForDuplicates();
    PlayerStats.Instance().level_unlocker.SetMaxUpgrades(levels.Length);

    LoadUnlockedLevels(PlayerPrefs.GetInt(LevelUnlockerPlayerPrefsKey(), 3));
		LoadRandomLevel();
	}

  private int DefaultLevelsToUnlockOnReset()
  {
    return PlayerStats.Instance().level_unlocker.GetLevelsToUnlockOnReset();
  }

  private string LevelUnlockerPlayerPrefsKey()
  {
    return PlayerStats.Instance().level_unlocker.gameObject.name + "_upgrades";
  }

	public void LoadRandomLevel()
	{
		if (!disable_level_loading)
		{
      current_level = PickNewLevel();
      LoadLevel(current_level);
		}
	}

  public void LoadLevel(int target_level)
  {
    ClearLevel();

    current_level = target_level;

    loader = GameObject.Instantiate(levels[current_level],
                                    levels[current_level].transform.position,
                                    levels[current_level].transform.rotation) as GameObject;
    LevelCompleteChecker.Instance().CountCoins();
  }

  private void ClearLevel()
  {
    LevelCompleteChecker.Instance().SetCoinsLeftToZero();
    DestroyAllWithTag("bomb");
    DestroyAllWithTag("bumper");
    DestroyAllWithTag("peg");
    DestroyAllWithTag("level");
    DestroyAllWithTag("goal");
    Destroy(loader);
    
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

    for (int i = 0; i < PlayerStats.Instance().level_unlocker.GetTotalUnlockedLevels(); ++i)
    {
      unlocked_levels.Add(i);
      Debug.Log("added level: " + i);
    }
	}

  private void LoadUnlockedLevels(int total_unlocked_levels)
  {
    unlocked_levels.Clear();

    for (int i = 0; i < total_unlocked_levels; ++i)
    {
      unlocked_levels.Add(i);
      Debug.Log("added level: " + i);
    }
  }

  public int GetCurrentLevel()
	{
		return current_level;
	}
}
