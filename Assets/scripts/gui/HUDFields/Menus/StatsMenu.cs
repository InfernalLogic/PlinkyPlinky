using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatsMenu : HUDField
{
  [SerializeField]
  private GUIStyle stat_display_style;

  private StatDisplayer[] stat_displayers;
  AchievementChainDisplayer[] achievement_chain_displayers;

  void Awake()
  {
    stat_displayers = GetComponentsInChildren<StatDisplayer>();
    achievement_chain_displayers = GetComponentsInChildren<AchievementChainDisplayer>();
  }

  protected override void DisplayGUIElements()
  {
    foreach (StatDisplayer stat_displayer in stat_displayers)
    {
      stat_displayer.Display(stat_display_style);
    }

    foreach (AchievementChainDisplayer achievement_chain_displayer in achievement_chain_displayers)
    {
      achievement_chain_displayer.Display(stat_display_style);
    }
  }
}
