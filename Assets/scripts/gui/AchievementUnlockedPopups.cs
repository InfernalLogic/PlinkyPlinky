using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementUnlockedPopups : HUDField 
{
  [SerializeField]
  private float message_lifetime = 0.0f;

  private float expiration_timer = 0.0f;

  private Queue<Texture> message_queue = new Queue<Texture>();
  private Texture current_message = null;

  public void EnqueueAchievementPopup(Texture popup_texture)
  {
    message_queue.Enqueue(popup_texture);
  }

  protected override void DisplayGUIElements()
  {
    if (current_message != null)
    {
      GUI.DrawTexture(new Rect(0, 0, display_rect.GetRect().width, display_rect.GetRect().height), 
                current_message);
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
