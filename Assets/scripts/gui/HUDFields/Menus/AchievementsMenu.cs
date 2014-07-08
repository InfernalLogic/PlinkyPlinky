using UnityEngine;
using System.Collections;

public class AchievementsMenu : HUDField
{
  protected override void DisplayGUIElements()
  {
    if (GUI.Button(new Rect(0, 0, 100, 100), "Achievements"))
    {
      Debug.Log("Achievements");
    }
  }
}
