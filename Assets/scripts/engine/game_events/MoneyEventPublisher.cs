using UnityEngine;
using System.Collections;

public static class MoneyEventPublisher
{
  private static Publisher<GameEvent> publisher = new Publisher<GameEvent>();
}
