using UnityEngine;
using System.Collections;

public class BumperUpgrader : ScoringObject 
{
	protected override void Initialize()
	{
		upgrade_id = "bumper";
	}

	public override void CalculateUpgradeCost()
	{
		upgrade_cost = (int)((Mathf.Pow((float)(upgrades+1), 1.5f) * 300));
	}

	public override void CalculatePointValue()
	{
		point_value = upgrades * 1;
	}
}
