using UnityEngine;
using System.Collections;

public class ScoringObjectUpgradeButton : UpgradeButton 
{
  private ScoringObjectUpgrade target_scoring_object;

  void Start()
  {
    target_scoring_object = (ScoringObjectUpgrade)target_upgrade;
  }

  protected override void DisplayTextLabel()
  {
    GUI.Label(label_display_rect.GetRect(), button_text + "\nCost: " + target_upgrade.GetUpgradeCost() +
      "\nincome/hit: " + target_scoring_object.GetPointValue() + "\nNext level: " + target_scoring_object.GetNextPointValue(), label_style);
  }
}
