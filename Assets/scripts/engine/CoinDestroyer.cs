using UnityEngine;
using System.Collections;

public class CoinDestroyer : MonoBehaviour 
{
  [SerializeField]
  private float cooldown = 0.0f;

  private bool destruction_in_progress = false;
  private float cooldown_timer = 0.0f;

  private CoinScript target_coin = null;

  void Start()
  {
    cooldown_timer = Time.time;
  }

  void Update()
  {
    if (destruction_in_progress)
    {
      if (CooldownComplete())
      {
        target_coin = FindObjectOfType<CoinScript>();
        if (target_coin != null)
        {
          target_coin.HitCoin();
          ResetCooldownTimer();
        }
        else
        {
          destruction_in_progress = false;
          LevelCompleteChecker.Instance().CountCoins();
        }
      }
    }
  }

  public void DestroyAllCoins()
  {
    destruction_in_progress = true;
    ResetCooldownTimer();
  }

  public bool IsDestructionInProgress()
  {
    return destruction_in_progress;
  }

  private bool CooldownComplete()
  {
    return cooldown_timer <= Time.time;
  }

  private void ResetCooldownTimer()
  {
    cooldown_timer = Time.time + cooldown;
  }
}
