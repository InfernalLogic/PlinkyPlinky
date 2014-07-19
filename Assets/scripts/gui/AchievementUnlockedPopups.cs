using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementUnlockedPopups : HUDField 
{
  [SerializeField]
  private float message_lifetime = 0.0f;

  private float expiration_timer = 0.0f;

  private Queue<string> message_queue = new Queue<string>();
  private string current_message = null;

  public void EnqueueAchievementPopup(string message)
  {
    message_queue.Enqueue(message);
  }

  protected override void DisplayGUIElements()
  {
    if (current_message != null)
    {
      GUI.Label(new Rect(0, 0, display_rect.GetRect().width, display_rect.GetRect().height), 
                "Achievement unlocked!\n" + current_message, background_style);
    }
  }

  void Update()
  {
    if (!MessageQueueIsEmpty() && CurrentMessageHasExpired())
    {
      current_message = message_queue.Dequeue();
      ResetExpirationTimer();
    }
    else if (CurrentMessageHasExpired())
    {
      current_message = null;
    }
  }

  private void ResetExpirationTimer()
  {
    expiration_timer = Time.time + message_lifetime;
  }

  private bool CurrentMessageHasExpired()
  {
    return expiration_timer < Time.time;
  }

  private bool MessageQueueIsEmpty()
  {
    return message_queue.Count <= 0;
  }
}
