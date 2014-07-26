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
      UpdateUnlockedLevels();

      DestroyAllRemainingCoins();

      LevelHandler.Instance().load_newest_level_next = true;
      LevelCompleteChecker.Instance().CountCoins();
    }
  }

  private static void DestroyAllRemainingCoins()
  {
    CoinScript[] coins = FindObjectsOfType<CoinScript>();

    foreach (CoinScript coin in coins)
    {
      Debug.Log("coin destroyed by LevelUnlocker");
      coin.HitCoin();
    }

    //FOR SOME REASON. If there's only one coin left in the scene, it won't be destroyed on the first pass. It has to be ran again.
    coins = FindObjectsOfType<CoinScript>();

    if (coins.Length != 0)
    {
      Debug.LogError("Not all coins were destroyed on the first pass!");

      foreach (CoinScript coin in coins)
      {
        Debug.Log("coin destroyed by LevelUnlocker");
        coin.HitCoin();
      }
    }
  }

  private static void UpdateUnlockedLevels()
  {
    LevelHandler.Instance().UpdateUnlockedLevels();
  }

  private void SpendUpgradeCost()
  {
    PlayerStats.Instance().SpendMoney(upgrade_cost);
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
}

