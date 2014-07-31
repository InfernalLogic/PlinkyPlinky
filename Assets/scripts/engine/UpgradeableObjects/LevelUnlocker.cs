using UnityEngine;
using System.Collections;

public class LevelUnlocker : UpgradeableObject
{
  public override void Upgrade()
  {
    if (PlayerHasEnoughMoney() && UpgradesNotMaxedOut())
    {
      ++value;
      SpendUpgradeCost();
      RecalculateUpgradeCost();
      
      GetComponentInChildren<CoinDestroyer>().DestroyAllCoins();

      LevelHandler.Instance().load_newest_level_next = true;
      LevelCompleteChecker.Instance().CountCoins();
      UpdateUnlockedLevels();
    }
  }

  private static void UpdateUnlockedLevels()
  {
    LevelHandler.Instance().UpdateUnlockedLevels();
  }

  private void SpendUpgradeCost()
  {
    MoneyTracker.Instance().SpendMoney(upgrade_cost);
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

  public int GetNewestLevelNumber()
  {
    return value - 1;
  }

  public void UnlockAllLevels()
  {
    Debug.Log("Unlocking all levels!");

    while (value < max_upgrades)
      ++value;
  }
}

