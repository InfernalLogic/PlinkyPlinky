using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatsMenu : HUDField
{
  [SerializeField]
  private GUIStyle stat_display_style;
  [SerializeField]
  private ScalingRect initial_stat_display_rect;
  [SerializeField]
  private ScrollViewInfo scroll_view;

  private StatDisplayer[] stat_displayers;
  private AchievementChainDisplayer[] achievement_chain_displayers;
  private Subscriber<RescaleHUDEvent> rescale_events = new Subscriber<RescaleHUDEvent>();

  void Awake()
  {
    stat_displayers = GetComponentsInChildren<StatDisplayer>();
    achievement_chain_displayers = GetComponentsInChildren<AchievementChainDisplayer>();
    HUDEvents.AddSubscriber(rescale_events);
  }

  void Start()
  {
    float view_height = 0.0f;

    foreach (StatDisplayer stat_displayer in stat_displayers)
    {
      view_height += stat_displayer.GetHeight();
    }

    foreach (AchievementChainDisplayer achievement_chain_displayer in achievement_chain_displayers)
    {
      view_height += achievement_chain_displayer.GetHeight();
    }

    scroll_view.SetViewHeight(view_height);
  }

  void Update()
  {
    if (!rescale_events.IsEmpty())
    {
      ResizeText();
      rescale_events.DeleteNewestMessage();
    }
  }

  protected override void DisplayGUIElements()
  {
    scroll_view.Begin();

      foreach (StatDisplayer stat_displayer in stat_displayers)
      {
        stat_displayer.Display(stat_display_style);
      }

      foreach (AchievementChainDisplayer achievement_chain_displayer in achievement_chain_displayers)
      {
        achievement_chain_displayer.Display(stat_display_style);
      }

    scroll_view.End();
  }

  public void ResizeText()
  {
    stat_display_style.fontSize = (int)Screen.height / 35;
  }
}
