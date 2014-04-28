using UnityEngine;
using System.Collections;

public class PlayerStats : PlinkyObject 
{
	const int LOCKED = 0;
	const int UNLOCKED = 1;
	
	private int current_money,
						  career_money;

	private int coin_upgrades,
							peg_upgrades,
							bumper_upgrades;

	private int coin_value,
							peg_value,
							bumper_value;

	private int coin_upgrade_cost,
							peg_upgrade_cost,
							bumper_upgrade_cost;

	void Start()
	{
		current_money = PlayerPrefs.GetInt ("current_money", 0);
		career_money = PlayerPrefs.GetInt ("career_money", 0);
		
		coin_upgrades = PlayerPrefs.GetInt ("coin_upgrades", 0);
		peg_upgrades = PlayerPrefs.GetInt ("peg_upgrades", 0);
		bumper_upgrades = PlayerPrefs.GetInt ("bumper_upgrades", 0);
		
		coin_upgrade_cost = PlayerPrefs.GetInt ("coin_upgrade_cost", 0);
		peg_upgrade_cost = PlayerPrefs.GetInt ("peg_upgrade_cost", 0);
		bumper_upgrade_cost = PlayerPrefs.GetInt ("bumper_upgrade_cost", 0);

		CalculateCoinValue();
		CalculateCoinUpgradeCost();
		engine.hud.UpdateMoney();
	}

	void OnDestroy()
	{
		PlayerPrefs.SetInt ("current_money", current_money);
		PlayerPrefs.SetInt ("career_money", career_money);

		PlayerPrefs.SetInt ("coin_upgrades", coin_upgrades);
		PlayerPrefs.SetInt ("peg_upgrades", peg_upgrades);
		PlayerPrefs.SetInt ("bumper_upgrades", bumper_upgrades);

		PlayerPrefs.SetInt ("coin_upgrade_cost", coin_upgrade_cost);
		PlayerPrefs.SetInt ("peg_upgrade_cost", peg_upgrade_cost);
		PlayerPrefs.SetInt ("bumper_upgrade_cost", bumper_upgrade_cost);

	}

	public void AddMoney(int income)
	{
		current_money += income;
		career_money += income;
		engine.hud.UpdateMoney();
	}

	public void CoinHit()
	{
		AddMoney (coin_value);
	}
	public void PegHit()
	{
		AddMoney (peg_value);
	}

	public void BumperHit()
	{
		AddMoney (bumper_value);
	}

	public void UpgradeCoin()
	{
		if (current_money >= coin_upgrade_cost)
		{
		++coin_upgrades;
		current_money -= coin_upgrade_cost;
		CalculateCoinValue();
		CalculateCoinUpgradeCost();
		engine.hud.UpdateMoney();
		}
		else
		{
			Debug.Log("Not enough moneys!");
		}
	}

	public void UpgradePeg()
	{
		if (current_money >= peg_upgrade_cost)
		{
			++peg_upgrades;
			current_money -= peg_upgrade_cost;
			CalculatePegValue();
			CalculatePegUpgradeCost();
			engine.hud.UpdateMoney();
		}
		else
		{
			Debug.Log("Not enough moneys!");
		}
	}

	public void UpgradeBumper()
	{
		if (current_money >= bumper_upgrade_cost)
		{
			++bumper_upgrades;
			current_money -= bumper_upgrade_cost;
			CalculateBumperValue();
			CalculateBumperUpgradeCost();
			engine.hud.UpdateMoney();
		}
		else
		{
			Debug.Log("Not enough moneys!");
		}
	}

	private void CalculateCoinValue()
	{
		coin_value = (coin_upgrades * 5) + 10;
	}

	private void CalculatePegValue()
	{
		peg_value = peg_upgrades;
	}

	private void CalculateBumperValue()
	{
		bumper_value = bumper_upgrades * 2;
	}

	private void CalculateCoinUpgradeCost()
	{
		coin_upgrade_cost = (int)((Mathf.Pow((float)coin_upgrades, 1.5f) * 100) + 100); 
	}

	private void CalculatePegUpgradeCost()
	{
		peg_upgrade_cost = peg_upgrades * 200;
	}

	private void CalculateBumperUpgradeCost()
	{
		bumper_upgrade_cost = bumper_upgrades * 400;
	}

	public int GetCurrentMoney()
	{
		return current_money;
	}
	
	public int GetCareerMoney()
	{
		return career_money;
	}

	public int GetCoinUpgradeCost()
	{
		return coin_upgrade_cost;
	}

	public int GetPegUpgradeCost()
	{
		return peg_upgrade_cost;
	}

	public int GetBumperUpgradeCost()
	{
		return bumper_upgrade_cost;
	}

	public void ResetStats()
	{
		current_money = 0;
		career_money = 0;
		coin_upgrades = 0;
		peg_upgrades = 0;
		bumper_upgrades = 0;

		CalculateCoinValue();
		CalculateCoinUpgradeCost();

		CalculatePegValue();
		CalculatePegUpgradeCost();

		CalculateBumperValue();
		CalculateBumperUpgradeCost();

		engine.hud.UpdateMoney();
	}
}