using UnityEngine;
using System.Collections;

public class UpgradeButton : Button 
{
  [SerializeField]
  protected UpgradeableObject target_upgrade;
  [SerializeField]
  protected string max_upgrades_reached_message;

  public override void Display()
  {
    if (target_upgrade.UpgradesNotMaxedOut())
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
      DisplayMaxUpgradesReached();
    }
  }

  public bool ButtonIsPressed()
  {
    return GUI.Button(display_rect.GetRect(), "", button_style);
  }

  public void DisplayDisabledButtonWithMask()
  {
    DisplayDummyButton();
    DisplayDisabledMask();
  }

  private void DisplayDummyButton()
  {
    GUI.Label(display_rect.GetRect(), "", button_style);
    DisplayTextLabel();
  }

  private bool PlayerHasEnoughMoneyToUpgrade()
  {
    return target_upgrade.PlayerHasEnoughCurrency();
  }

  protected virtual void DisplayMaxUpgradesReached()
  {
    GUI.Label(display_rect.GetRect(), "", button_style);
    GUI.Label(label_display_rect.GetRect(), max_upgrades_reached_message, label_style);
  }

  protected virtual void DisplayTextLabel()
  {
    GUI.Label(label_display_rect.GetRect(), button_text + "\nCost: " + 
              target_upgrade.GetUpgradeCost(), label_style);
  }
}
