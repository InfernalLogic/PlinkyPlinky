using UnityEngine;
using System.Collections;

public class PegUpgrader : ScoringObject 
{
	protected override void Initialize()
	{
		upgrade_id = "peg";
		GetPointValue();
	}

	public override void RecalculateUpgradeCost()
	{
		upgrade_cost = (int)((Mathf.Pow((float)(upgrades+1), 1.2f) * 200));
	}
	
	public override int GetPointValue()
	{
		return upgrades;
	}
}
