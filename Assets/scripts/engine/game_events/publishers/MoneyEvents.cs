using UnityEngine;
using System.Collections;

public static class MoneyEventPublisher
{
  private static Publisher<GameEvent> publisher = new Publisher<GameEvent>();

  public static void AddSubscriber(Subscriber<GameEvent> subscriber)
  {
    publisher.AddSubscriber(subscriber);
  }

  public static void Publish(GameEvent message)
  {
    publisher.PublishMessage(message);
  }
}
