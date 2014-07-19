using UnityEngine;
using System.Collections;

public class CheatButton : MonoBehaviour
{
  [SerializeField]
  private int amount_to_cheat = 1000;

  void OnGUI()
  {
    Display();
  }

  public void Display()
  {
    if (CheatButtonIsPressed())
    {
      PlayerStats.Instance().AddMoney(amount_to_cheat);
    }
  }

  private bool CheatButtonIsPressed()
  {
    return GUI.Button(new Rect(0, 0, 100, 100), "Press to cheat");
  }
}
