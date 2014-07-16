using UnityEngine;
using System.Collections;

public class CoinUpgrader : ScoringObject 
{

	public override void RecalculateUpgradeCost()
	{
		upgrade_cost = (int)((Mathf.Pow((float)(value + 1), 1.2f) * 50)); 
	}
	
	public override int GetPointValue()
	{
		return (value * 5) + 10;
	}
}

