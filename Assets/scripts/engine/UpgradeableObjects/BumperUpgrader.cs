using UnityEngine;
using System.Collections;

public class BumperUpgrader : ScoringObject 
{
	protected override void Initialize()
	{
		upgrade_id = "bumper";
		GetPointValue();
	}

	public override void RecalculateUpgradeCost()
	{
		upgrade_cost = (int)((Mathf.Pow((float)(upgrades+1), 1.2f) * 150));
	}

	public override int GetPointValue()
	{
		return upgrades;
	}
}
