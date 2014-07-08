using UnityEngine;
using System.Collections;

public class OptionsMenu : HUDField
{
  private Rect reset_confirmation_window_rect;
  private bool display_reset_confirmation_window= false;

  [SerializeField]
  private GUIStyle button_style;
  [SerializeField]
  private GUIStyle label_style;

  void Start()
  {
    base.Start();
    InitializeResetConfirmationWindowRect();
  }

  protected override void DisplayGUIElements()
  {
    if (!display_reset_confirmation_window)
    {
      DisplayResetGameButton();
      DisplayCheatButton();
      DisplayNewRandomLevelButton();
    }
    else
    {
      LoadResetConfirmationWindow();
    }
  }

  private void DisplayResetGameButton()
  {
    if (ResetButtonIsPressed())
    {
      display_reset_confirmation_window = true;
      LoadResetConfirmationWindow();
    }
  }

  private void ResetGame()
  {
    engine.player_stats.ResetStats();
    engine.level_handler.LoadRandomLevel();
    Debug.Log("Game reset");
  }

  private bool ResetButtonIsPressed()
  {
    return GUI.Button(new Rect(10, 10, 300, 100), "Reset game", button_style);
  }

  private void LoadResetConfirmationWindow()
  {
    GUI.Label(new Rect(0, 0, reset_confirmation_window_rect.width, reset_confirmation_window_rect.height), 
              "Are you sure you want to reset? All your progress will be lost.", label_style);

    reset_confirmation_window_rect = GUI.ModalWindow(0, reset_confirmation_window_rect, LoadResetConfirmationButtons, "", GUIStyle.none);
  }

  private void LoadResetConfirmationButtons(int window_id)
  {
    if (YesButtonIsPressed())
    {
      ResetGame();
      display_reset_confirmation_window = false;
    }
    if (NoButtonIsPressed())
    {
      display_reset_confirmation_window = false;
    }
  }

  private bool YesButtonIsPressed()
  {
    return GUI.Button(new Rect(10, 10,
                               ((reset_confirmation_window_rect.width / 2) - 20), ((reset_confirmation_window_rect.height / 2) - 20)), 
                               "YES", button_style);
  }

  private bool NoButtonIsPressed()
  {
    return GUI.Button(new Rect((reset_confirmation_window_rect.width / 2) + 10, 10,
                               ((reset_confirmation_window_rect.width / 2) - 20), ((reset_confirmation_window_rect.height / 2) - 20)), 
                               "NO", button_style);
  }

  private void DisplayCheatButton()
  {
    if (CheatButtonIsPressed())
    {
      engine.player_stats.AddMoney(1000);
    }
  }

  private bool CheatButtonIsPressed()
  {
    return GUI.Button(new Rect(10, 110, 300, 100), "Press to cheat", button_style);
  }

  private void DisplayNewRandomLevelButton()
  {
    if (NewRandomLevelButtonIsPressed())
    {
      engine.level_handler.LoadRandomLevel();
    }
  }

  private bool NewRandomLevelButtonIsPressed()
  {
    return GUI.Button(new Rect(10, 210, 300, 100), "Go to random new level", button_style);
  }

  private void InitializeResetConfirmationWindowRect()
  {
    reset_confirmation_window_rect.width = Screen.width / 3;
    reset_confirmation_window_rect.height = Screen.height / 4;
    reset_confirmation_window_rect.x = Screen.width * (2f / 3f);
    reset_confirmation_window_rect.y = Screen.height / 4;
  }

  
}
