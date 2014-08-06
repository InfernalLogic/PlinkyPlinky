using UnityEngine;
using System.Collections;

public static class AchievementUnlockedEvents 
{
  private static Publisher<AchievementUnlocked> publisher = new Publisher<AchievementUnlocked>();

  public static void AddSubscriber(Subscriber<AchievementUnlocked> subscriber)
  {
    publisher.AddSubscriber(subscriber);
  }

  public static void Publish(AchievementUnlocked message)
  {
    publisher.PublishMessage(message);
  }
}
