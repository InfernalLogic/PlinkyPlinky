﻿using UnityEngine;
using System.Collections;

public class MaxBombsUpgradeButton : UpgradeButton 
{
  private MaxBombsUpgrader max_bombs_upgrader;

  void Awake()
  {
    max_bombs_upgrader = FindObjectOfType<MaxBombsUpgrader>();
  }

  protected override void DisplayTextLabel()
  {
    GUI.Label(label_display_rect.GetRect(),
              button_text + "\nCost: " + target_upgrade.GetUpgradeCost() + "\nMax: " + max_bombs_upgrader.GetValue(), 
              label_style);
  }
}