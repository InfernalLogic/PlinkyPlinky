using UnityEngine;
using System.Collections;

public class PrestigeButton : Button
{
  [SerializeField]
  private string disabled_text;
  private string enabled_text;

  LevelUnlocker level_unlocker;

  protected override void Awake()
  {
    enabled_text = button_text;
    base.Awake();
  }

  void Start()
  {
    level_unlocker = FindObjectOfType<LevelUnlocker>();
  }

  public override void Display()
  {
    if (level_unlocker.UpgradesMaxedOut())
    {
      button_text = enabled_text;
      if (ButtonIsPressed())
        Events.PublishReset(ResetType.SOFT);
    }
    else
    {
      button_text = disabled_text;
      DisplayDisabledButtonWithMask();
    }
  }

  public void DisplayDisabledButtonWithMask()
  {
    DisplayDummyButton();
    DisplayDisabledMask();
  }

  private void DisplayDummyButton()
  {
    if (ButtonIsPressed())
      return;
  }

  public bool ButtonIsPressed()
  {
    return GUI.Button(display_rect.GetRect(), button_text, button_style);
  }
}
