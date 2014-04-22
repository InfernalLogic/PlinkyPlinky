using UnityEngine;
using System.Collections;

public class LevelHandler : MonoBehaviour 
{
	public GameObject[] levels;
	private GameObject loader;
	private int current_level;

	void Start()
	{
		LoadRandomLevel ();
	}

	public void LoadRandomLevel()
	{
		DestroyAllBombs();
		Destroy (loader);
		current_level = Random.Range (0, levels.Length);
		loader = GameObject.Instantiate (levels[current_level], 
		                                 levels[current_level].transform.position,
		                                 levels[current_level].transform.rotation) as GameObject;
	}

	public void DestroyAllBombs()
	{
		GameObject[] bombs = GameObject.FindGameObjectsWithTag("bomb");

		foreach (GameObject element in bombs)
		{
			Destroy (element.gameObject);
		}
	}
}
