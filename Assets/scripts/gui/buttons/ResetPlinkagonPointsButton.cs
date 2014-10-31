using UnityEngine;
using System.Collections;

public class ResetPlinkagonPointsButton : Button 
{
  public override void Display()
  {
    if (GUI.Button(display_rect.GetRect(), button_text, button_style))
    {
      MoneyTracker.Instance().ResetPlinkagonPoints();
    }
  }
}
