using UnityEngine;
using System.Collections;

public static class HUDEvents
{
  private static Publisher<RescaleHUDEvent> publisher = new Publisher<RescaleHUDEvent>();

  public static void AddSubscriber(Subscriber<RescaleHUDEvent> subscriber)
  {
    publisher.AddSubscriber(subscriber);
  }

  public static void Publish(RescaleHUDEvent message)
  {
    publisher.PublishMessage(message);
  }
}
