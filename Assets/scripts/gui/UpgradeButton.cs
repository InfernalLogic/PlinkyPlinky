using UnityEngine;
using System.Collections;

public class UpgradeButton : PlinkyObject 
{
  [SerializeField]
  private UpgradeableObject target_upgrade;
  [SerializeField]
  private GUIContent content;
  [SerializeField]
  private GUIStyle style;

  [SerializeField]
  private ScalingRect display_rect;

  public void Display()
  {
  }

  public bool ButtonIsPressed()
  {
    return GUI.Button(display_rect.GetRect(), content, style);
  }

  public void DisplayDisabledButton()
  {
    GUI.Label(display_rect.GetRect(), content, style);
  }

  private bool PlayerHasEnoughMoneyToUpgrade()
  {
    return target_upgrade.PlayerHasEnoughMoney();
  }

}
