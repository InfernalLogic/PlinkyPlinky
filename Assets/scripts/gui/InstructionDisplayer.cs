using UnityEngine;
using System.Collections;

public class InstructionDisplayer : MonoBehaviour 
{
  [SerializeField]
  private SavedBool instructions_have_been_shown;
  [SerializeField]
  private FloatingText generator;

  private Subscriber<GameEvent> ball_dropped_listener = new Subscriber<GameEvent>();

  void Awake()
  {
    if (instructions_have_been_shown.IsTrue())
    {
      Destroy(this.gameObject);
    }

    GameEvents.AddSubscriber(ball_dropped_listener);
  }

  void Update()
  {
    if (instructions_have_been_shown.IsTrue())
    {
      Destroy(this.gameObject);
    }

    while (!ball_dropped_listener.IsEmpty())
    {
      if (ball_dropped_listener.ReadNewestMessage().name == "bomb_dropped_event")
        instructions_have_been_shown.Set(true);

      ball_dropped_listener.DeleteNewestMessage();
    }
  }
}
