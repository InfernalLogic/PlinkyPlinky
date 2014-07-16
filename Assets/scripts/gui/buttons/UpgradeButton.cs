using UnityEngine;
using System.Collections;

public class UpgradeButton : Button 
{
  [SerializeField]
  private UpgradeableObject target_upgrade;
  [SerializeField]
  private GUIStyle mask_style;
  [SerializeField]
  private GUIStyle label_style;
  [SerializeField]
  private ScalingRect indented_label_display_rect;

  private ScoringObject target_scoring_object;

  void Start()
  {
    target_scoring_object = (ScoringObject)target_upgrade;
  }

  public override void Display()
  {
    if (target_upgrade.PlayerHasEnoughMoney())
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

  public bool ButtonIsPressed()
  {

    return GUI.Button(display_rect.GetRect(), "", button_style);
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
    GUI.Label(display_rect.GetRect(), "", button_style);
    DisplayTextLabel();
  }

  private void DisplayTextLabel()
  {
    GUI.Label(indented_label_display_rect.GetRect(), button_text + "\nCost: " + target_upgrade.GetUpgradeCost() +
      "\nincome/hit: " + target_scoring_object.GetPointValue(), label_style);
  }

  private bool PlayerHasEnoughMoneyToUpgrade()
  {
    return target_upgrade.PlayerHasEnoughMoney();
  }

}
