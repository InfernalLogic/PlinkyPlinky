using UnityEngine;
using System.Collections;

public class BallsLeftWheel : ObjectWheel 
{
  private MaxBombsUpgrader max_bombs;

  private Subscriber<GameEvent> subscriber = new Subscriber<GameEvent>();

  private void OnEnable()
  {
    Events.ResetEvents += OnReset;
  }

  private void OnDisable()
  {
    Events.ResetEvents -= OnReset;
  }

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
      if (subscriber.ReadNewestMessage() == UpgradeEvents.max_bombs_upgraded)
      {
        RespawnWheel();
      }

      subscriber.DeleteNewestMessage();
    }

    for (int i = 0; i < max_bombs.GetValue(); ++i)
    {
      if (i < BombScript.BombCount)
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
    SpawnObjects();
  }

  private void OnReset(ResetType type)
  {
    StartCoroutine(RespawnNextFrame());
  }

  private IEnumerator RespawnNextFrame()
  {
    yield return new WaitForFixedUpdate();
    RespawnWheel();
  }
}
