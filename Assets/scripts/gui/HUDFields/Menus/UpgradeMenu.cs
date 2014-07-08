using UnityEngine;
using System.Collections;

public class UpgradeMenu : HUDField 
{
  [SerializeField]
  private GUIStyle button_style;

  protected override void DisplayGUIElements()
  {
    DisplayCoinUpgradeButton();
    DisplayPegUpgradeButton();
    DisplayBumperUpgradeButton();
  }

  private void DisplayCoinUpgradeButton()
  {
    if (CoinUpgradeButtonIsPressed())
    {
      engine.player_stats.coin_upgrader.Upgrade();
      Debug.Log("Upgraded coins");
    }
  }

  private bool CoinUpgradeButtonIsPressed()
  {
    return GUI.Button(new Rect(10, 10, 300, 100), "More monies per coin!", button_style);
  }

  private void DisplayPegUpgradeButton()
  {
    if (PegUpgradeButtonIsPressed())
    {
      engine.player_stats.peg_upgrader.Upgrade();
    }
  }

  private bool PegUpgradeButtonIsPressed()
  {
    return GUI.Button(new Rect(10, 110, 300, 100), "More monies per peg!", button_style);
  }

  private void DisplayBumperUpgradeButton()
  {
    if (BumperUpgradeButtonIsPressed())
    {
      engine.player_stats.bumper_upgrader.Upgrade();
    }
  }

  private bool BumperUpgradeButtonIsPressed()
  {
    return GUI.Button(new Rect(10, 210, 300, 100), "More monies per bumper!", button_style);
  }
}
