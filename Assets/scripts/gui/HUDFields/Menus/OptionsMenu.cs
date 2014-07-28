using UnityEngine;
using System.Collections;

public class OptionsMenu : HUDField
{

  private bool display_reset_confirmation_window= false;
  private bool hide_level_number = false;

  [SerializeField]
  private GUIStyle button_style;
  [SerializeField]
  private GUIStyle label_style;

  [SerializeField]
  ScalingRect reset_button_rect;
  [SerializeField]
  ScalingRect mute_sound_effects_button_rect;
  [SerializeField]
  ScalingRect mute_music_button_rect;
  [SerializeField]
  private ScalingRect reset_confirmation_window_rect;

  protected override void DisplayGUIElements()
  {
    if (!display_reset_confirmation_window)
    {
      DisplayResetGameButton();
      DisplayMuteSoundEffectsButton();
      DisplayMuteMusicButton();
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
    PlayerStats.Instance().ResetStats();
    LevelHandler.Instance().LoadRandomLevel();
    Debug.Log("Game reset");
  }

  private bool ResetButtonIsPressed()
  {
    return GUI.Button(reset_button_rect.GetRect(), "Reset game", button_style);
  }



  private void LoadResetConfirmationWindow()
  {
    GUI.Label(new Rect(0, 0, reset_confirmation_window_rect.GetRect().width, reset_confirmation_window_rect.GetRect().height), 
              "Are you sure you want to reset? All your progress will be lost.", label_style);

    reset_confirmation_window_rect.SetRect(GUI.Window(0, reset_confirmation_window_rect.GetRect(), LoadResetConfirmationButtons, "", GUIStyle.none));
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
      if (AudioHandler.Instance().mute_sound_effects)
        UnmuteSoundEffects();
      else
        MuteSoundEffects();
    }
  }

  private void DisplayMuteMusicButton()
  {
    if (MuteMusicButtonIsPressed())
    {
      if (AudioHandler.Instance().mute_music)
        UnmuteMusic();
      else
        MuteMusic();
    }
  }

  private void MuteSoundEffects()
  {
    AudioHandler.Instance().mute_sound_effects = true;
  }

  private void UnmuteSoundEffects()
  {
    AudioHandler.Instance().mute_sound_effects = false;
  }

  private bool MuteSoundEffectsButtonIsPressed()
  {
    if (AudioHandler.Instance().mute_sound_effects)
      return GUI.Button(mute_sound_effects_button_rect.GetRect(), "Unmute sounds", button_style);
    else
      return GUI.Button(mute_sound_effects_button_rect.GetRect(), "Mute sounds", button_style);
  }

  private bool MuteMusicButtonIsPressed()
  {
    if (AudioHandler.Instance().mute_music)
      return GUI.Button(mute_music_button_rect.GetRect(), "Unmute music", button_style);
    else
      return GUI.Button(mute_music_button_rect.GetRect(), "Mute music", button_style);
  }

  private void MuteMusic()
  {
    AudioHandler.Instance().MuteMusic();
  }

  private void UnmuteMusic()
  {
    AudioHandler.Instance().UnmuteMusic();
  }
}
