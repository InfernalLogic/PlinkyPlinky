using UnityEngine;
using System.Collections;

public class UpgradeMenu : HUDField 
{
  [SerializeField]
  private GUIStyle button_style;

  [SerializeField]
  private GUIStyle button_disabled_mask;

  private enum upgrades
  {
    COIN,
    PEG,
    BUMPER
  };

  protected override void DisplayGUIElements()
  {
    DisplayCoinUpgradeButton();
    DisplayPegUpgradeButton();
    DisplayBumperUpgradeButton();
  }

  private void DisplayCoinUpgradeButton()
  {
    if (PlayerHasEnoughMoneyToUpgrade(upgrades.COIN))
    {
      if (CoinUpgradeButtonIsPressed())
      {
        engine.player_stats.coin_upgrader.Upgrade();
        Debug.Log("Upgraded coins");
      }
    }
    else
    {
      DisplayCoinDisabledLabel();
    }
  }

  private void DisplayCoinDisabledLabel()
  {
    
    GUI.Label(new Rect(10, 10, 300, 100), "More monies per coin!\n" + engine.player_stats.coin_upgrader.GetUpgradeCost() + " monies", button_style);
    GUI.Label(new Rect(10, 10, 300, 100), "", button_disabled_mask);
  }

  private bool CoinUpgradeButtonIsPressed()
  {
    return GUI.Button(new Rect(10, 10, 300, 100), "More monies per coin!\n" + engine.player_stats.coin_upgrader.GetUpgradeCost() + " monies", button_style);
  }

  private void DisplayPegUpgradeButton()
  {
    if (PlayerHasEnoughMoneyToUpgrade(upgrades.PEG))
    {
      if (PegUpgradeButtonIsPressed())
      {
        engine.player_stats.peg_upgrader.Upgrade();
        Debug.Log("Upgraded pegs");
      }
    }
    else
    {
      DisplayPegDisabledLabel();
    }
  }

  private void DisplayPegDisabledLabel()
  {
    GUI.Label(new Rect(10, 110, 300, 100), "More monies per peg!\n" + engine.player_stats.peg_upgrader.GetUpgradeCost() + " monies", button_style);
    GUI.Label(new Rect(10, 110, 300, 100), "", button_disabled_mask);
  }

  private bool PegUpgradeButtonIsPressed()
  {
    return GUI.Button(new Rect(10, 110, 300, 100), "More monies per peg!\n" + engine.player_stats.peg_upgrader.GetUpgradeCost() + " monies", button_style);
  }

  private void DisplayBumperUpgradeButton()
  {
    if (PlayerHasEnoughMoneyToUpgrade(upgrades.BUMPER))
    {
      if (BumperUpgradeButtonIsPressed())
      {
        engine.player_stats.bumper_upgrader.Upgrade();
        Debug.Log("Upgraded pegs");
      }
    }
    else
    {
      DisplayBumperDisabledLabel();
    }
  }

  private void DisplayBumperDisabledLabel()
  {
    GUI.Label(new Rect(10, 210, 300, 100), "More monies per bumper!\n" + engine.player_stats.bumper_upgrader.GetUpgradeCost() + " monies", 
                      button_style);

    GUI.Label(new Rect(10, 210, 300, 100), "", button_disabled_mask);
  }

  private bool BumperUpgradeButtonIsPressed()
  {
    return GUI.Button(new Rect(10, 210, 300, 100), "More monies per bumper!\n" + engine.player_stats.bumper_upgrader.GetUpgradeCost() + " monies", 
                      button_style);
  }

  private bool PlayerHasEnoughMoneyToUpgrade(upgrades target_button)
  {
    switch (target_button)
    {
      case upgrades.COIN:
        return engine.player_stats.coin_upgrader.PlayerHasEnoughMoney();
        break;

      case upgrades.PEG:
        return engine.player_stats.peg_upgrader.PlayerHasEnoughMoney();
        break;

      case upgrades.BUMPER:
        return engine.player_stats.bumper_upgrader.PlayerHasEnoughMoney();
        break;

      default:
        return false;
        break;
    }
    return false;
  }

  private void ResetGUIColor()
  {
    GUI.color = Color.white;
  }
}