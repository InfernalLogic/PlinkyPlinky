using UnityEngine;
using System.Collections;

public class ResetPlinkagonPointsButton : Button 
{
  public override void Display()
  {
    if (GUI.Button(display_rect.GetRect(), "", button_style))
    {
      MoneyTracker.Instance().ResetPlinkagonPoints();
    }
    DisplayTextLabel();
  }

  protected virtual void DisplayTextLabel()
  {
    GUI.Label(label_display_rect.GetRect(), button_text, label_style);
  }
}
