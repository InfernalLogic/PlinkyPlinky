using UnityEngine;
using System.Collections;

public class RandomLevelButton : Button 
{
  void OnGUI()
  {
    Display();
  }

  public override void Display()
  {
    if (NewRandomLevelButtonIsPressed())
    {
      LevelHandler.Instance().LoadRandomLevel();
    }
  }

  private bool NewRandomLevelButtonIsPressed()
  {
    return GUI.Button(new Rect(100, 0, 100, 100), "Go to random new level");
  }
}
