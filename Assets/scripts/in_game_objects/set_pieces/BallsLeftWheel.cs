using UnityEngine;
using System.Collections;

public class BallsLeftWheel : ObjectWheel 
{
  private MaxBombsUpgrader max_bombs;

  private Subscriber<GameEvent> subscriber = new Subscriber<GameEvent>();

  void Awake()
  {
    max_bombs = FindObjectOfType<MaxBombsUpgrader>();
    UpgradeEvents.AddSubscriber(subscriber);
    GameEvents.AddSubscriber(subscriber);
  }

  void Start()
  {
    RespawnWheel();
  }

  void Update()
  {
    while (!subscriber.IsEmpty())
    {
      if (subscriber.ReadNewestMessage() == UpgradeEvents.max_bombs_upgraded || subscriber.ReadNewestMessage() == GameEvents.game_reset_event)
      {
        Debug.Log(subscriber.ReadNewestMessage().name);
        RespawnWheel();
      }

      subscriber.DeleteNewestMessage();
    }

    for (int i = 0; i < max_bombs.GetValue(); ++i)
    {
      if (i < BombScript.GetBombCount())
        children[i].transform.renderer.enabled = false;
      else
        children[i].transform.renderer.enabled = true;
    }

    RotateWheel();
  }

  public void RespawnWheel()
  {
    object_count = max_bombs.GetValue();
    DestroyAllChildren();
    SpawnWheelObjects();
  }

}
