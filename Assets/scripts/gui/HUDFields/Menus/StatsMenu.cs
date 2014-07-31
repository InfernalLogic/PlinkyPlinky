using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatsMenu : HUDField
{
  [SerializeField]
  private GUIStyle stat_display_style;
  [SerializeField]
  private ScalingRect view_rect;
  [SerializeField]
  private ScalingRect scroll_view_position_rect;
  [SerializeField]
  private ScalingRect initial_stat_display_rect;

  private StatDisplayer[] stat_displayers;
  private AchievementChainDisplayer[] achievement_chain_displayers;

  private Vector2 scroll_view_position = Vector2.zero;

  void Awake()
  {
    stat_displayers = GetComponentsInChildren<StatDisplayer>();
    achievement_chain_displayers = GetComponentsInChildren<AchievementChainDisplayer>();
  }

  protected override void DisplayGUIElements()
  {
    scroll_view_position = GUI.BeginScrollView(scroll_view_position_rect.GetRect(), scroll_view_position, view_rect.GetRect());
      foreach (StatDisplayer stat_displayer in stat_displayers)
      {
        stat_displayer.Display(stat_display_style);
      }

      foreach (AchievementChainDisplayer achievement_chain_displayer in achievement_chain_displayers)
      {
        achievement_chain_displayer.Display(stat_display_style);
      }

    GUI.EndScrollView();
  }
}
