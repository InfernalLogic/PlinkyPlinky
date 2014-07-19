using UnityEngine;
using System.Collections;

public class UnlockLevelsButton : Button 
{
  [SerializeField]
  private UpgradeableObject target_upgrade;
  [SerializeField]
  private GUIStyle mask_style;

  public override void Display()
  {
    if (PlayerStats.Instance().level_unlocker.UpgradesNotMaxedOut())
    {
      if (target_upgrade.PlayerHasEnoughMoney())
      {
        if (ButtonIsPressed())
        {
          target_upgrade.Upgrade();
        }
      }
      else
      {
        DisplayDisabledButtonWithMask();
      }
    }
    else
    {
      DisplayAllLevelsUnlocked();
    }
  }

  public bool ButtonIsPressed()
  {
    return GUI.Button(display_rect.GetRect(), button_text + "\nCost: " + target_upgrade.GetUpgradeCost(), button_style);
  }

  public void DisplayDisabledButtonWithMask()
  {
    DisplayDummyButton();
    DisplayDisabledMask();
  }

  private void DisplayDisabledMask()
  {
    GUI.Label(display_rect.GetRect(), "", mask_style);
  }

  private void DisplayDummyButton()
  {
    GUI.Label(display_rect.GetRect(), button_text + "\nCost: " + target_upgrade.GetUpgradeCost(), button_style);
  }

  private bool PlayerHasEnoughMoneyToUpgrade()
  {
    return target_upgrade.PlayerHasEnoughMoney();
  }

  private void DisplayAllLevelsUnlocked()
  {
    GUI.Label(display_rect.GetRect(), "All levels unlocked!", button_style);
  }

}
