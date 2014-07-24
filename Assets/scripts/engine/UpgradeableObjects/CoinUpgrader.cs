using UnityEngine;
using System.Collections;

public class CoinUpgrader : ScoringObject 
{
	public override void RecalculateUpgradeCost()
	{
		upgrade_cost = (int)((Mathf.Pow((float)(value + 1), 1.3f) * 50)); 
	}
	
	public override int GetPointValue()
	{
    return point_value;
	}

  public override void RecalculatePointValue()
  {
    point_value = (value * 5) + 10;
  }
}

