using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementFloatingTextFactory : Singleton<AchievementFloatingTextFactory>
{
  [SerializeField]
  private Timer popup_display_timer;
  [SerializeField]
  private FloatingText plinkagon_floating_text;

  private Subscriber<AchievementUnlocked> achievement_listener = new Subscriber<AchievementUnlocked>();
  private static Queue<string> announcements = new Queue<string>();

  new void Awake()
  {
    base.Awake();
    AchievementUnlockedEvents.AddSubscriber(achievement_listener);
  }

  void Update()
  {
    if (!achievement_listener.IsEmpty())
    {
      AchievementPopupInfo popup_info = achievement_listener.ReadNewestMessage().popup_info;

      AddAnnouncement(GeneratePopupText(popup_info));

      PublishPlinkagonPointEarnedEvent();
      achievement_listener.DeleteNewestMessage();
    }

    if (announcements.Count > 0 && popup_display_timer.IsExpired())
    {
      DisplayAnnouncement(announcements.Peek());
      popup_display_timer.Reset();
      announcements.Dequeue();
    }
  }

  public void AddAnnouncement(string text)
  {
    announcements.Enqueue(text);
  }

  private void DisplayAnnouncement(string text)
  {
    FloatingText new_text = (FloatingText)Instantiate(plinkagon_floating_text, Vector3.zero, transform.rotation);
    new_text.SetText(text);
  }

  private static string GeneratePopupText(AchievementPopupInfo popup_info)
  {
    return "  " + popup_info.achievement_text_number + " " + popup_info.achievement_text + "\n+" + popup_info.plinkagon_point_value + " plinkagon points!";
  }

  private void PublishPlinkagonPointEarnedEvent()
  {
    GameEvents.Publish(CurrentPlinkagonPointEvent());
  }

  private MoneyEarnedGameEvent CurrentPlinkagonPointEvent()
  {
    return new MoneyEarnedGameEvent(achievement_listener.ReadNewestMessage().popup_info.plinkagon_point_value, "plinkagon_point_earned_event");
  }
}
