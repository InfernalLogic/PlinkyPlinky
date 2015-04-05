using UnityEngine;
using System.Collections;

public class PlinkagonUpgrade : UpgradeableObject
{
  [SerializeField]
  private float percent_chance_per_level = 2.0f;

  new protected virtual void OnEnable()
  {
    base.OnEnable();
    Events.PlinkagonRefundEvents += OnPlinkagonRefund;
  }

  new protected virtual void OnDisable()
  {
    base.OnDisable();
    Events.PlinkagonRefundEvents -= OnPlinkagonRefund;
  }

  new void Awake()
  {
    base.Awake();
    is_plinkagon_upgrade = true;
  }

  public override void Upgrade()
  {
    if (PlayerHasEnoughCurrency() && UpgradesNotMaxedOut())
    {
      MoneyTracker.Instance.SpendPlinkagonPoints(upgrade_cost);
      ++value;
      UpgradeEvents.Publish(UpgradeEvents.clone_balls_upgraded);
    }
  }

  public override void RecalculateUpgradeCost()
  {
    upgrade_cost = 1;
  }

  public float GetChanceToProc()
  {
    return percent_chance_per_level * (float)value;
  }

  public int RollProcs(int procs)
  {
    int new_procs = 0;

    while (ProcRolled(procs + new_procs))
      ++new_procs;

    return new_procs;
  }

  private bool ProcRolled(int procs)
  {
    return Random.Range(0.0f, 100.0f) < GetChanceToProc() - ((float)(procs) * 100.0);
  }

  private void OnPlinkagonRefund()
  {
    MoneyTracker.Instance.AddPlinkagonPoints(value);
    value = 0;
  }
}
