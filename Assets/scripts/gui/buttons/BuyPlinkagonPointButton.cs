using UnityEngine;
using System.Collections;

public class BuyPlinkagonPointButton : UpgradeButton 
{
  LevelUnlocker level_unlocker;

  void Start()
  {
    level_unlocker = FindObjectOfType<LevelUnlocker>();
  }

  public override void Display()
  {
    if (level_unlocker.UpgradesMaxedOut())
    {
      if (target_upgrade.PlayerHasEnoughCurrency())
      {
        if (ButtonIsPressed())
        {
          target_upgrade.Upgrade();
        }
        DisplayTextLabel();
      }
      else
      {
        DisplayDisabledButtonWithMask();
      }
    }
    else
    {
      DisplayDisabledButtonWithMask();
    }
  }
}
