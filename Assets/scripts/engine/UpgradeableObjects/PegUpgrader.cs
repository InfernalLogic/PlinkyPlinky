﻿using UnityEngine;
using System.Collections;

public class PegUpgrader : ScoringObject 
{
	public override void CalculateUpgradeCost()
	{
		upgrade_cost = (int)((Mathf.Pow((float)(upgrades+1), 1.5f) * 500));
	}
	
	public override void CalculatePointValue()
	{
		point_value = upgrades * 1;
	}
}
