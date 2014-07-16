using UnityEngine;
using System.Collections;

public class CoinUpgrader : ScoringObject 
{
	protected override void Initialize()
	{
		upgrade_id = "coin";
		GetPointValue();
	}

	public override void RecalculateUpgradeCost()
	{
		upgrade_cost = (int)((Mathf.Pow((float)(upgrades + 1), 1.2f) * 50)); 
	}
	
	public override int GetPointValue()
	{
		return (upgrades * 5) + 10;
	}
}

