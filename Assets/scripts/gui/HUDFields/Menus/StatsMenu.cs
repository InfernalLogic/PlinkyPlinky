using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatsMenu : HUDField
{
  [SerializeField]
  private GUIStyle stat_display_style;

  [SerializeField]
  private ScalingRect total_money_earned_rect;
  [SerializeField]
  private ScalingRect total_poppers_popped_rect;
  [SerializeField]
  private ScalingRect total_bumpers_bumped_rect;
  [SerializeField]
  private ScalingRect total_coins_coined_rect;
  [SerializeField]
  private ScalingRect total_balls_dropped_rect;

  [SerializeField]
  private AchievementChain pegs_hit_chain;
  [SerializeField]
  private AchievementChain bumpers_hit_chain;
  [SerializeField]
  private AchievementChain coins_hit_chain;
  [SerializeField]
  private AchievementChain bombs_dropped_chain;

  protected override void DisplayGUIElements()
  {
    DisplayTotalMoneyEarned();
    DisplayTotalPoppersPopped();
    DisplayTotalBumpersBumped();
    DisplayTotalCoinsCoined();
    DisplayTotalBallsDropped();
  }

  private void DisplayTotalMoneyEarned()
  {
    GUI.Label(total_money_earned_rect.GetRect(), "Total money gained: " + PlayerStats.Instance().GetCareerMoney(), stat_display_style);
  }

  private void DisplayTotalPoppersPopped()
  {
    GUI.Label(total_poppers_popped_rect.GetRect(), "Total poppers plinked: " + pegs_hit_chain.GetTrackedStatValue(), stat_display_style);
  }

  private void DisplayTotalBumpersBumped()
  {
    GUI.Label(total_bumpers_bumped_rect.GetRect(), "Total bumpers bumped: " + bumpers_hit_chain.GetTrackedStatValue(), stat_display_style);
  }

  private void DisplayTotalCoinsCoined()
  {
    GUI.Label(total_coins_coined_rect.GetRect(), "Total coins coined: " + coins_hit_chain.GetTrackedStatValue(), stat_display_style);
  }

  private void DisplayTotalBallsDropped()
  {
    GUI.Label(total_balls_dropped_rect.GetRect(), "Total balls dropped: " + bombs_dropped_chain.GetTrackedStatValue(), stat_display_style);
  }
}
