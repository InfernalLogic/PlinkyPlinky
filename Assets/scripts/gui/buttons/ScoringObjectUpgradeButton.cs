using UnityEngine;
using System.Collections;

public class ScoringObjectUpgradeButton : UpgradeButton 
{
  private ScoringObject target_scoring_object;

  void Start()
  {
    target_scoring_object = (ScoringObject)target_upgrade;
  }

  protected override void DisplayTextLabel()
  {
    GUI.Label(label_display_rect.GetRect(), button_text + "\nCost: " + target_upgrade.GetUpgradeCost() +
      "\nincome/hit: " + target_scoring_object.GetPointValue(), label_style);
  }
}
