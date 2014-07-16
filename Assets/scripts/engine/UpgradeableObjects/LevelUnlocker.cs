﻿using UnityEngine;
using System.Collections;

public class LevelUnlocker : UpgradeableObject 
{
  public override void Upgrade()
  {
    if (PlayerHasEnoughMoney() && UpgradesNotMaxedOut())
    {
      ++value;
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
        Debug.Log("upgrades: " + value + " max_upgrades: " + max_upgrades);
      }
    }
  }

  private void LoadNewlyUnlockedLevel()
  {
    engine.level_handler.LoadLevel(value - 1);
  }

  public override void Reset()
  {
    value = GetLevelsToUnlockOnReset();
    RecalculateUpgradeCost();
  }

  public bool UpgradesNotMaxedOut()
  {
    return value < (max_upgrades);
  }

  public override void RecalculateUpgradeCost()
  {
    upgrade_cost = (int)(500.0f * Mathf.Pow((float)(value - GetLevelsToUnlockOnReset() + 1), 1.2f));
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

