using UnityEngine;
using System.Collections;

public class OptionsMenu : HUDField
{
  private bool display_reset_confirmation_window = false;
  private bool display_hard_reset_confirmation_window = false;

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
  [SerializeField]
  private ScalingRect show_exponents_button_rect;

  private LevelUnlocker level_unlocker;

  private void Awake()
  {
    ResizeText();
  }

  private void Start()
  {
    level_unlocker = FindObjectOfType<LevelUnlocker>();
  }

  private void OnEnable()
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
      DisplayExponentialButton();
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
    if (level_unlocker.UpgradesMaxedOut())
    {
      if (GUI.Button(reset_button_rect.GetRect(), "Prestige", button_style))
      {
        display_reset_confirmation_window = true;
        LoadResetConfirmationWindow();
      }
    }
    else
    {
      DisplayPrestigeDisabledMask();
      if (GUI.Button(reset_button_rect.GetRect(), "Unlock all levels to Prestige!", button_style))
        return;
    }
  }

  private void DisplayExponentialButton()
  {
    if (GUI.Button(show_exponents_button_rect.GetRect(), "Exponential Notation", button_style))
      FindObjectOfType<ScoreTicker>().ToggleExponents();
  }

  private void DisplayPrestigeDisabledMask()
  {

  }

  private void DisplayHardResetGameButton()
  {
    if (HardResetButtonIsPressed())
    {
      display_hard_reset_confirmation_window = true;
      LoadResetConfirmationWindow();
    }
  }

  private void SoftReset()
  {
    Events.PublishReset(ResetType.SOFT);
    Debug.Log("Game reset");
  }

  private void HardResetGame()
  {
    Events.PublishReset(ResetType.HARD);
    Debug.Log("Game hard reset");
  }

  private bool HardResetButtonIsPressed()
  {
    return GUI.Button(hard_reset_button_rect.GetRect(), "Delete all game data", button_style);
  }

  private void LoadResetConfirmationWindow()
  {
    GUI.Label(new Rect(0, 0, reset_confirmation_window_rect.GetRect().width, reset_confirmation_window_rect.GetRect().height),
              "Restart the game from the beginning? You'll still have your plinkagon points and level completion bonus.", label_style);

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
      SoftReset();
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
      if (AudioHandler.Instance.mute_sfx.IsTrue())
        UnmuteSoundEffects();
      else
        MuteSoundEffects();
    }
  }

  private void DisplayMuteMusicButton()
  {
    if (MuteMusicButtonIsPressed())
    {
      if (AudioHandler.Instance.mute_bgm.IsTrue())
        UnmuteMusic();
      else
        MuteMusic();
    }
  }

  private void MuteSoundEffects()
  {
    AudioHandler.Instance.MuteSFX();
  }

  private void UnmuteSoundEffects()
  {
    AudioHandler.Instance.UnmuteSFX();
  }

  private void MuteMusic()
  {
    AudioHandler.Instance.MuteBGM();
  }

  private void UnmuteMusic()
  {
    AudioHandler.Instance.UnmuteBGM();
  }

  private bool MuteSoundEffectsButtonIsPressed()
  {
    if (AudioHandler.Instance.mute_sfx.IsTrue())
      return GUI.Button(mute_sound_effects_button_rect.GetRect(), "Unmute sounds", button_style);
    else
      return GUI.Button(mute_sound_effects_button_rect.GetRect(), "Mute sounds", button_style);
  }

  private bool MuteMusicButtonIsPressed()
  {
    if (AudioHandler.Instance.mute_bgm.IsTrue())
      return GUI.Button(mute_music_button_rect.GetRect(), "Unmute music", button_style);
    else
      return GUI.Button(mute_music_button_rect.GetRect(), "Mute music", button_style);
  }

  void DisplayCurrentLevel()
  {
    GUI.Label(current_level_display_rect.GetRect(),
               "Playing stage: " + LevelHandler.Instance.GetCurrentLevel(), label_style);
  }

}