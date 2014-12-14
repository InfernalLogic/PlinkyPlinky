using UnityEngine;
using System.Collections;

public class OptionsMenu : HUDField
{
  private bool display_reset_confirmation_window = false;
  private bool display_hard_reset_confirmation_window = false;
  private bool hide_level_number = false;

  [SerializeField]
  private GUIStyle button_style;
  [SerializeField]
  private GUIStyle label_style;

  [SerializeField]
  private ScalingRect reset_button_rect;
  [SerializeField]
  private ScalingRect mute_sound_effects_button_rect;
  [SerializeField]
  private ScalingRect mute_music_button_rect;
  [SerializeField]
  private ScalingRect current_level_display_rect;
  [SerializeField]
  private ScalingRect reset_confirmation_window_rect;
  [SerializeField]
  private ScalingRect hard_reset_button_rect;

  void Awake()
  {
    ResizeText();
  }

  void OnEnable()
  {
    HUDEvents.OnScreenResize += ResizeText;
  }

  private void ResizeText()
  {
    button_style.fontSize = Screen.height / 35;
  }

  protected override void DisplayGUIElements()
  {
    if (!display_reset_confirmation_window && !display_hard_reset_confirmation_window)
    {
      DisplayResetGameButton();
      DisplayHardResetGameButton();
      DisplayMuteSoundEffectsButton();
      DisplayMuteMusicButton();
      DisplayCurrentLevel();
    }
    else if (display_reset_confirmation_window)
    {
      LoadResetConfirmationWindow();
    }
    else if (display_hard_reset_confirmation_window)
    {
      LoadHardResetConfirmationWindow();
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

  private void DisplayHardResetGameButton()
  {
    if (HardResetButtonIsPressed())
    {
      display_hard_reset_confirmation_window = true;
      LoadResetConfirmationWindow();
    }
  }

  private void ResetGame()
  {
    UpgradeHandler.Instance().ResetStats();
    LevelHandler.Instance().LoadRandomLevel();
    Debug.Log("Game reset");
  }

  private void HardResetGame()
  {
    UpgradeHandler.Instance().HardResetStats();
    LevelHandler.Instance().LoadRandomLevel();
    Debug.Log("Game hard reset");
  }

  private bool ResetButtonIsPressed()
  {
    return GUI.Button(reset_button_rect.GetRect(), "Reset game", button_style);
  }

  private bool HardResetButtonIsPressed()
  {
    return GUI.Button(hard_reset_button_rect.GetRect(), "Delete all game data", button_style);
  }

  private void LoadResetConfirmationWindow()
  {
    GUI.Label(new Rect(0, 0, reset_confirmation_window_rect.GetRect().width, reset_confirmation_window_rect.GetRect().height), 
              "Restart the game from the beginning? You'll still have your plinkagon points.", label_style);

    reset_confirmation_window_rect.SetRect(GUI.Window(0, reset_confirmation_window_rect.GetRect(), LoadResetConfirmationButtons, "", GUIStyle.none));
  }

  private void LoadHardResetConfirmationWindow()
  {
    GUI.Label(new Rect(0, 0, reset_confirmation_window_rect.GetRect().width, reset_confirmation_window_rect.GetRect().height),
              "THIS WILL DELETE ALL OF YOUR PROGRESS AND ACHIEVEMENTS", label_style);

    reset_confirmation_window_rect.SetRect(GUI.Window(0, reset_confirmation_window_rect.GetRect(), LoadHardResetConfirmationButtons, "", GUIStyle.none));
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

  private void LoadHardResetConfirmationButtons(int window_id)
  {
    if (YesButtonIsPressed())
    {
      HardResetGame();
      display_hard_reset_confirmation_window = false;
    }
    if (NoButtonIsPressed())
    {
      display_hard_reset_confirmation_window = false;
    }
  }

  private bool YesButtonIsPressed()
  {
    return GUI.Button(new Rect(10, 10,
                               ((reset_confirmation_window_rect.GetRect().width / 2) - 20), ((reset_confirmation_window_rect.GetRect().height / 2) - 20)), 
                               "YES", button_style);
  }

  private bool NoButtonIsPressed()
  {
    return GUI.Button(new Rect((reset_confirmation_window_rect.GetRect().width / 2) + 10, 10,
                               ((reset_confirmation_window_rect.GetRect().width / 2) - 20), ((reset_confirmation_window_rect.GetRect().height / 2) - 20)), 
                               "NO", button_style);
  }

  private void DisplayMuteSoundEffectsButton()
  {
    if (MuteSoundEffectsButtonIsPressed())
    {
      if (AudioHandler.Instance().mute_sfx.IsTrue())
        UnmuteSoundEffects();
      else
        MuteSoundEffects();
    }
  }

  private void DisplayMuteMusicButton()
  {
    if (MuteMusicButtonIsPressed())
    {
      if (AudioHandler.Instance().mute_bgm.IsTrue())
        UnmuteMusic();
      else
        MuteMusic();
    }
  }

  private void MuteSoundEffects()
  {
    AudioHandler.Instance().MuteSFX();
  }

  private void UnmuteSoundEffects()
  {
    AudioHandler.Instance().UnmuteSFX();
  }

  private void MuteMusic()
  {
    AudioHandler.Instance().MuteBGM();
  }

  private void UnmuteMusic()
  {
    AudioHandler.Instance().UnmuteBGM();
  }

  private bool MuteSoundEffectsButtonIsPressed()
  {
    if (AudioHandler.Instance().mute_sfx.IsTrue())
      return GUI.Button(mute_sound_effects_button_rect.GetRect(), "Unmute sounds", button_style);
    else
      return GUI.Button(mute_sound_effects_button_rect.GetRect(), "Mute sounds", button_style);
  }

  private bool MuteMusicButtonIsPressed()
  {
    if (AudioHandler.Instance().mute_bgm.IsTrue())
      return GUI.Button(mute_music_button_rect.GetRect(), "Unmute music", button_style);
    else
      return GUI.Button(mute_music_button_rect.GetRect(), "Mute music", button_style);
  }

  void DisplayCurrentLevel()
  {
    GUI.Label(current_level_display_rect.GetRect(),
               "Playing stage: " + LevelHandler.Instance().GetCurrentLevel(), label_style);
  }

}
