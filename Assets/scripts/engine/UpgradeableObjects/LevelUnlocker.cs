using UnityEngine;
using System.Collections;

public class LevelUnlocker : UpgradeableObject
{
  public override void Upgrade()
  {
    if (PlayerHasEnoughMoney() && UpgradesNotMaxedOut())
    {
      ++value;
      PlayerStats.Instance().SpendMoney(upgrade_cost);
      LevelHandler.Instance().LoadUnlockedLevels();
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
        Debug.Log("upgrades: " + value + " max_upgrades: " + max_upgrades);
      }
    }
  }

  private void LoadNewlyUnlockedLevel()
  {
    LevelHandler.Instance().LoadLevel(value - 1);
  }

  public override void RecalculateUpgradeCost()
  {
    upgrade_cost = (int)(300.0f * Mathf.Pow((float)(value - GetLevelsToUnlockOnReset()), 1.2f)) + 200;
  }

  public int GetTotalUnlockedLevels()
  {
    return value;
  }

  public int GetLevelsToUnlockOnReset()
  {
    return base.GetUpgradesOnReset();
  }
}

