using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementTracker : PlinkyObject 
{
  private IDictionary<string, AchievementChain> achievement_chains;

  void Awake()
  {
    achievement_chains = new Dictionary<string, AchievementChain>();

    AchievementChain[] achievement_chains_found = GetComponentsInChildren<AchievementChain>();

    foreach (AchievementChain chain in achievement_chains_found)
    {
      achievement_chains.Add(chain.name, chain);
    }
  }

  public void IncrementAchievementStat(string stat_name)
  {
    if (achievement_chains.ContainsKey(stat_name))
    {
      achievement_chains[stat_name].Increment();
    }
    else
    {
      Debug.Log("IncrementAchievementStat could not find: " + stat_name);
    }
  }

}
