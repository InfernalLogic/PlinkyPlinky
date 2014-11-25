using UnityEngine;
using System.Collections;

public class PegUpgrader : ScoringObjectUpgrade 
{
	public override void RecalculateUpgradeCost()
	{
		upgrade_cost = (int)((Mathf.Pow((float)(value + 1), 1.4f) * 200));
	}

  public override void RecalculatePointValue()
  {
    point_value = value;
  }
}
