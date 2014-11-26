using UnityEngine;
using System.Collections;

public class CheatButton : MonoBehaviour 
{

  void OnGUI()
  {
    if (GUI.Button(new Rect(0, 0, 100, 100), "cheat", GUIStyle.none))
      MoneyTracker.Instance().AddMoney(1000);
  }
}
