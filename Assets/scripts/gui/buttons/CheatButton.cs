using UnityEngine;
using System.Collections;

public class CheatButton : PlinkyObject
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
      engine.player_stats.AddMoney(amount_to_cheat);
    }
  }

  private bool CheatButtonIsPressed()
  {
    return GUI.Button(new Rect(0, 0, 100, 100), "Press to cheat");
  }
}
