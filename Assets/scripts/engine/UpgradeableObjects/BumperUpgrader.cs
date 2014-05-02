using UnityEngine;
using System.Collections;

public class BumperUpgrader : ScoringObject 
{
	public override void CalculateUpgradeCost()
	{
		//upgrade_cost = upgrades * 400 + 200;
		upgrade_cost = upgrades * 10;
	}

	public override void CalculatePointValue()
	{
		point_value = upgrades * 1;
	}
}
