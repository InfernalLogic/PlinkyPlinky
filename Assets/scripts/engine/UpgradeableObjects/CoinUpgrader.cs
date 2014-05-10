using UnityEngine;
using System.Collections;

public class CoinUpgrader : ScoringObject 
{
	protected override void Initialize()
	{
		upgrade_id = "coin";
	}

	public override void CalculateUpgradeCost()
	{
		upgrade_cost = (int)((Mathf.Pow((float)(upgrades+1), 1.5f) * 100)); 
	}
	
	public override void CalculatePointValue()
	{
		point_value = (upgrades * 5) + 10;
	}
}

