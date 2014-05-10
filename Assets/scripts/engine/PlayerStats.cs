using UnityEngine;
using System.Collections;

public class PlayerStats : PlinkyObject 
{
	const int LOCKED = 0;
	const int UNLOCKED = 1;

	public BumperUpgrader bumper_upgrader;
	public PegUpgrader peg_upgrader;
	public CoinUpgrader coin_upgrader;
	
	private int current_money,
						  career_money;

	void Start()
	{
		current_money = PlayerPrefs.GetInt ("current_money", 0);
		career_money = PlayerPrefs.GetInt ("career_money", 0);

		engine.hud.UpdateMoney();

		UpgradeableObject[] upgrades = Resources.FindObjectsOfTypeAll<UpgradeableObject>();

		foreach (UpgradeableObject element in upgrades)
		{
			element.CalculateUpgradeCost();
		}
	}

	void OnDestroy()
	{
		PlayerPrefs.SetInt ("current_money", current_money);
		PlayerPrefs.SetInt ("career_money", career_money);
	}

	public void AddMoney(int income)
	{
		current_money += income;
		career_money += income;
		engine.hud.UpdateMoney();
	}

	public void CoinHit()
	{
		coin_upgrader.Scored();
	}

	public void PegHit()
	{
		peg_upgrader.Scored();
	}

	public void BumperHit()
	{
		bumper_upgrader.Scored();
	}

	public void ResetStats()
	{
		current_money = 0;
		career_money = 0;
		
		UpgradeableObject[] upgrades = Resources.FindObjectsOfTypeAll<UpgradeableObject>();
		
		foreach (UpgradeableObject element in upgrades)
		{
			element.Reset();
		}
		
		engine.hud.UpdateMoney();
	}

	public void SpendMoney(int price)
	{
			current_money -= price;

	}

	public int GetCurrentMoney()
	{
		return current_money;
	}
	
	public int GetCareerMoney()
	{
		return career_money;
	}

}