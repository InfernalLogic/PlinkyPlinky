using UnityEngine;
using System.Collections;

public class DevCheats : MonoBehaviour 
{
  private int target_level = 0;

	// Use this for initialization
	void Start () 
  {
    if (!Application.isEditor)
      Destroy(this);
	}
	
	// Update is called once per frame
	void Update () 
  {
	  if (Input.GetKeyDown(KeyCode.C))
      MoneyTracker.Instance.AddMoney(10000);

    if (Input.GetKeyDown(KeyCode.R))
      LevelHandler.Instance.LoadRandomLevel();

    if (Input.GetKeyDown(KeyCode.U))
      FindObjectOfType<LevelUnlocker>().UnlockAllLevels();

    if (Input.GetKeyDown(KeyCode.P))
      MoneyTracker.Instance.AddPlinkagonPoints(5);

    if (Input.GetKeyDown(KeyCode.N))
    {
      LevelHandler.Instance.LoadLevel(target_level++);

      if (target_level >= LevelHandler.Instance.levels.GetLength(0))
        target_level = 0;
    }
      
	}

  void OnGUI()
  {
    GUI.Label(new Rect(0, 0, 100, 100), "DEV MODE");
  }
}
