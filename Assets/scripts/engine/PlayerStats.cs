using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour 
{
	const int LOCKED = 0;
	const int UNLOCKED = 1;
	
	private int current_money,
						  career_money;

	private int coin_value = 10;

	void Awake()
	{
		PlayerPrefs.GetInt ("current_money", 0);
		PlayerPrefs.GetInt ("career_money", 0);
		PlayerPrefs.GetInt ("coin_value", 10);
	}

	public int GetCurrentMoney()
	{
		return current_money;
	}

	public int GetCareerMoney()
	{
		return career_money;
	}

	public void CoinHit()
	{
		current_money += coin_value;
		career_money += coin_value;
	}
}
