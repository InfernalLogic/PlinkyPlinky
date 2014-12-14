using UnityEngine;
using System.Collections;

public class BuyPlinkagonPointButton : UpgradeButton 
{
  [SerializeField]
  private string disabled_text;
  private string enabled_text;

  LevelUnlocker level_unlocker;

  protected override void Awake()
  {
    enabled_text = button_text;
    base.Awake();
  }

  void Start()
  {
    level_unlocker = FindObjectOfType<LevelUnlocker>();
  }

  public override void Display()
  {
    if (level_unlocker.UpgradesMaxedOut())
    {
      button_text = enabled_text;
      if (target_upgrade.PlayerHasEnoughCurrency())
      {
        if (ButtonIsPressed())
          target_upgrade.Upgrade();

        DisplayTextLabel();
      }
      else
      {
        DisplayDisabledButtonWithMask();
      }
    }
    else
    {
      button_text = disabled_text;
      DisplayDisabledButtonWithMask();
    }
  }
}
