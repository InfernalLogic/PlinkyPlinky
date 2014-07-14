using UnityEngine;
using System.Collections;

public class LevelUnlocker : UpgradeableObject 
{
  protected override void Initialize()
  {
    upgrade_id = gameObject.name;
  }

  public override void Upgrade()
  {

    if (PlayerHasEnoughMoney() && UpgradesNotMaxedOut())
    {
      ++upgrades;
      engine.player_stats.SpendMoney(upgrade_cost);
      engine.level_handler.LoadUnlockedLevels();
      RecalculateUpgradeCost();
      LoadNewlyUnlockedLevel();
    }
    else
    {
      if (!PlayerHasEnoughMoney())
      {
        Debug.Log("Not enough moneys! " + gameObject.name);
      }

      if (!UpgradesNotMaxedOut())
      {
        Debug.Log("Max upgrades reached!" + gameObject.name);
        Debug.Log("upgrades: " + upgrades + " max_upgrades: " + max_upgrades);
      }
    }
  }

  private void LoadNewlyUnlockedLevel()
  {
    engine.level_handler.LoadLevel(upgrades - 1);
  }

  public override void Reset()
  {
    upgrades = GetLevelsToUnlockOnReset();
    RecalculateUpgradeCost();
  }

  public bool UpgradesNotMaxedOut()
  {
  return upgrades < (max_upgrades);
  }

  public void SetMaxUpgrades(int max_upgrades)
	{
		this.max_upgrades = max_upgrades;
	}

	public void Save()
	{
		upgrade_id = gameObject.name;
		PlayerPrefs.SetInt(upgrade_id +"_upgrades", upgrades);
    Debug.Log(upgrade_id + " saved as: " + PlayerPrefs.GetInt(upgrade_id + "_upgrades", GetLevelsToUnlockOnReset()));
	}

  public override void RecalculateUpgradeCost()
  {
    upgrade_cost = (int)(500.0f * Mathf.Pow((float)(upgrades - GetLevelsToUnlockOnReset() + 1), 1.2f));
  }

  public int GetTotalUnlockedLevels()
  {
    return upgrades;
  }

  public int GetLevelsToUnlockOnReset()
  {
    return base.GetUpgradesOnReset();
  }
}

