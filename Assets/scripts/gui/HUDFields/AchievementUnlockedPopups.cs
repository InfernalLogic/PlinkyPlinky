using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementUnlockedPopups : HUDField 
{
  [SerializeField]
  private ScalingRect popup_rect;
  [SerializeField]
  private ScalingRect label_rect;
  [SerializeField]
  private Timer message_timer;
  [SerializeField]
  private GUIStyle label_style;

  private Subscriber<AchievementUnlocked> subscriber = new Subscriber<AchievementUnlocked>();

  private AchievementPopupInfo current_popup;

  void Awake()
  {
    AchievementUnlockedEvents.AddSubscriber(subscriber);
  }

  protected override void DisplayGUIElements()
  {
    if (current_popup != null)
    {
      GUI.DrawTexture(popup_rect.GetRect(), current_popup.popup_background);
      GUI.Label(label_rect.GetRect(), current_popup.achievement_text_number + " " + current_popup.achievement_text_verb, label_style);
    }
  }

  void Update()
  {
    if (!subscriber.IsEmpty() && message_timer.IsExpired())
    {
      current_popup = subscriber.ReadNewestMessage().popup_info;
      subscriber.DeleteNewestMessage();
      message_timer.Reset();
    }
    else if (message_timer.IsExpired())
    {
      current_popup = null;
    }
  }
}
