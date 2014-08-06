using UnityEngine;
using System.Collections;

public class LevelHandler : Singleton<LevelHandler> 
{
  [HideInInspector]
  public bool load_newest_level_next = false;
  [SerializeField]
  private bool disable_level_loading = false;
  [SerializeField]
  private bool unlock_all_levels = false;

	public GameObject[] levels;
	public ArrayList unlocked_levels = new ArrayList();

	private GameObject loader;
	private int current_level;

	void Start()
	{
    CheckForDuplicates();
    FindObjectOfType<LevelUnlocker>().SetMaxUpgrades(levels.Length);

    if (unlock_all_levels)
      FindObjectOfType<LevelUnlocker>().UnlockAllLevels();

    LoadUnlockedLevels(PlayerPrefs.GetInt(LevelUnlockerPlayerPrefsKey(), 3));
		LoadRandomLevel();


	}

  private int DefaultLevelsToUnlockOnReset()
  {
    return FindObjectOfType<LevelUnlocker>().GetLevelsToUnlockOnReset();
  }

  private string LevelUnlockerPlayerPrefsKey()
  {
    return FindObjectOfType<LevelUnlocker>().GetKey();
  }

	public void LoadRandomLevel()
	{
		if (!disable_level_loading)
		{
      current_level = PickNewLevel();
      LoadLevel(current_level);
      PublishLevelLoadedMessage();
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

  private static void PublishLevelLoadedMessage()
  {
    GameEvents.Publish(GameEvents.level_loaded_event);
  }

  private void ClearLevel()
  {
    LevelCompleteChecker.Instance().SetCoinsLeftToZero();
    DestroyAllWithTag("bomb");
    DestroyAllWithTag("bumper");
    DestroyAllWithTag("peg");
    DestroyAllWithTag("level");
    DestroyAllWithTag("coin");
    Destroy(loader);
  }

	public int PickNewLevel()
	{


    if (!load_newest_level_next)
    {
      int new_level = (int)unlocked_levels[Random.Range(0, unlocked_levels.Count)];

      if (new_level == current_level && unlocked_levels.Count > 1)
        new_level = PickNewLevel();

      return new_level;
    }
    else
    {
      load_newest_level_next = false;
      return FindObjectOfType<LevelUnlocker>().GetNewestLevelNumber();
    }
	}

	public void DestroyAllWithTag(string target_tag)
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag(target_tag);

		foreach (GameObject element in objects)
		{
			Destroy (element.gameObject);
		}
	}

	public void UpdateUnlockedLevels()
	{
		unlocked_levels.Clear();

    for (int i = 0; i < FindObjectOfType<LevelUnlocker>().GetTotalUnlockedLevels(); ++i)
    {
      unlocked_levels.Add(i);
      Debug.Log("added level: " + i);
    }
	}

  private void LoadUnlockedLevels(int total_unlocked_levels)
  {
    Debug.Log("total_unlocked_levels: " + total_unlocked_levels);
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
