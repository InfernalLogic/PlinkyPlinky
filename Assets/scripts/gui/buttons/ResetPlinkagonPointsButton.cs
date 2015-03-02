using UnityEngine;
using System.Collections;

public class ResetPlinkagonPointsButton : Button 
{
  public override void Display()
  {
    if (GUI.Button(display_rect.GetRect(), "", button_style))
      Events.PublishPlinkagonRefund();

    DisplayTextLabel();
  }

  protected virtual void DisplayTextLabel()
  {
    GUI.Label(label_display_rect.GetRect(), button_text, label_style);
  }
}
