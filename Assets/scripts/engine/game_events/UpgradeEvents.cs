using UnityEngine;
using System.Collections;

public static class UpgradeEvents
{
  public static GameEvent bomb_cooldown_upgraded = new GameEvent("bomb_cooldown_upgraded");
  public static GameEvent max_bombs_upgraded = new GameEvent("max_bombs_upgraded");

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
