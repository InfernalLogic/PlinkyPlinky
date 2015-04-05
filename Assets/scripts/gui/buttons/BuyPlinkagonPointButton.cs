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
        {
          int attempts = 1;

          if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            attempts = 10;

          if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            attempts = 100;

          for (int i = 0; i < attempts; ++i)
          {
            if (target_upgrade.PlayerHasEnoughCurrency())
              target_upgrade.Upgrade();
          }
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
      button_text = disabled_text;
      DisplayDisabledButtonWithMask();
    }
  }
}
