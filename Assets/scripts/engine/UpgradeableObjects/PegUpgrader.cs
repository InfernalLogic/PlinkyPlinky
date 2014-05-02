using UnityEngine;
using System.Collections;

public class PegUpgrader : ScoringObject 
{
	public override void CalculateUpgradeCost()
	{
		upgrade_cost = upgrades * 500 + 1000;
	}
	
	public override void CalculatePointValue()
	{
		point_value = upgrades * 1;
	}
}
