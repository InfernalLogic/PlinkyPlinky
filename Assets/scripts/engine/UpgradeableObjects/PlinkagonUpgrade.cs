﻿using UnityEngine;
using System.Collections;

public class PlinkagonUpgrade : UpgradeableObject
{
  [SerializeField]
  private float percent_chance_per_level = 2.0f;

  new void Awake()
  {
    base.Awake();
    is_plinkagon_upgrade = true;
  }

  public override void Upgrade()
  {
    if (PlayerHasEnoughCurrency() && UpgradesNotMaxedOut())
    {
      MoneyTracker.Instance().SpendPlinkagonPoints(upgrade_cost);
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
    while (ProcRolled(procs))
    {
      ++procs;
      Debug.Log(name + " rolled: " + procs);
    }

    Debug.Log(name + " rolled: " + procs);

    return procs;
  }

  private bool ProcRolled(int procs)
  {
    return Random.Range(0.0f, 100.0f) <= GetChanceToProc() - ((float)(procs) * 100.0);
  }
}