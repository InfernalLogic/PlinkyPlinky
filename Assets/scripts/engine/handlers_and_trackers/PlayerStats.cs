using UnityEngine;
using System.Collections;

public class PlayerStats : PlinkyObject 
{
	const int LOCKED = 0;
	const int UNLOCKED = 1;

	public BumperUpgrader bumper_upgrader;
	public PegUpgrader peg_upgrader;
	public CoinUpgrader coin_upgrader;
  public LevelUnlocker level_unlocker;
	
	private int current_money,
						  career_money;

	void Start()
	{
		current_money = PlayerPrefs.GetInt ("current_money", 0);
		career_money = PlayerPrefs.GetInt ("career_money", 0);

		UpgradeableObject[] upgrades = Resources.FindObjectsOfTypeAll<UpgradeableObject>();

		foreach (UpgradeableObject element in upgrades)
		{
			element.RecalculateUpgradeCost();
		}
	}

	void OnDestroy()
	{
		PlayerPrefs.SetInt ("current_money", current_money);
		PlayerPrefs.SetInt ("career_money", career_money);
	}

	public void AddMoney(int income)
	{
		current_money += income;
		career_money += income;
	}

  public void BombDropped()
  {
    engine.achievement_tracker.IncrementAchievementStat("total_bombs_dropped");
  }

	public void CoinHit()
	{
		AddMoney(coin_upgrader.GetPointValue());
    engine.achievement_tracker.IncrementAchievementStat("total_coins_hit");
	}

	public void PegHit()
	{
    AddMoney(peg_upgrader.GetPointValue());
    engine.achievement_tracker.IncrementAchievementStat("total_pegs_hit");
	}

	public void BumperHit()
	{
    AddMoney(bumper_upgrader.GetPointValue());
    engine.achievement_tracker.IncrementAchievementStat("total_bumpers_hit");
	}

	public void ResetStats()
	{
		current_money = 0;
		career_money = 0;
		
		SavedStat[] stats = Resources.FindObjectsOfTypeAll<SavedStat>();

    foreach (SavedStat element in stats)
		{
			element.Reset();
		}

		engine.level_handler.LoadUnlockedLevels();
	
	}

	public void SpendMoney(int price)
	{
		current_money -= price;
	}

	public int GetCurrentMoney()
	{
		return current_money;
	}
	
	public int GetCareerMoney()
	{
		return career_money;
	}

}