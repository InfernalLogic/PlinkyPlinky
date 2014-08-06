using UnityEngine;
using System.Collections;

public static class GameEvents
{
  public static GameEvent bumper_hit_event = new GameEvent("bumper_hit_event");
  public static GameEvent peg_hit_event = new GameEvent("peg_hit_event");
  public static GameEvent coin_hit_event = new GameEvent("coin_hit_event");
  public static GameEvent bomb_dropped_event = new GameEvent("bomb_dropped_event");
  public static GameEvent level_loaded_event = new GameEvent("level_loaded_event");
  public static GameEvent level_completed_event = new GameEvent("level_completed_event");
  public static GameEvent multi_plink_event = new GameEvent("double_plink_event");
  public static GameEvent game_reset_event = new GameEvent("game_reset_event");

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
