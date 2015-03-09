﻿using UnityEngine;
using System.Collections;

abstract public class UpgradeableObject : SavedStat
{
  [SerializeField]
  protected int max_upgrades = 0;

  protected bool is_plinkagon_upgrade = false;

  protected ulong upgrade_cost;

  abstract public void Upgrade();
  abstract public void RecalculateUpgradeCost();

  new protected virtual void OnEnable()
  {
    Events.ResetEvents += OnReset;
  }

  new protected virtual void OnDisable()
  {
    Events.ResetEvents -= OnReset;
  }

  protected void Start()
  {
    RecalculateUpgradeCost();
  }

  void OnDestroy()
  {
    Save();
  }

  public int GetUpgrades()
  {
    return GetValue();
  }

  public ulong GetUpgradeCost()
  {
    return upgrade_cost;
  }

  public virtual bool PlayerHasEnoughCurrency()
  {
    if (!is_plinkagon_upgrade)
      return (MoneyTracker.Instance.GetCurrentMoney() >= upgrade_cost);
    else
      return (MoneyTracker.Instance.GetCurrentPlinkagonPoints() >= upgrade_cost);
  }

  public int GetUpgradesOnReset()
  {
    return GetDefaultValueOnReset();
  }

  public void SetMaxUpgrades(int max_upgrades)
  {
    this.max_upgrades = max_upgrades;
  }

  public bool UpgradesNotMaxedOut()
  {
    return value < (max_upgrades) || max_upgrades == 0;
  }

  public bool UpgradesMaxedOut()
  {
    return value >= (max_upgrades) || max_upgrades == 0;
  }

  new protected virtual void OnReset(ResetType type)
  {
    base.OnReset(type);
    RecalculateUpgradeCost();
  }
}