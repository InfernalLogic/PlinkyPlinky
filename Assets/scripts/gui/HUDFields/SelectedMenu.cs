using UnityEngine;
using System.Collections;

public class SelectedMenu : HUDField 
{
  public HUDField[] selectable_menus;

  public MenuSelector menu_selector;

  private enum Menus
  {
    UPGRADES,
    UNLOCKABLES,
    ACHIEVEMENTS,
    OPTIONS
  }

  protected override void DisplayGUIElements()
  {
    DisplayMenuBackground();
    LoadSelectedMenu(menu_selector.GetSelectedMenu());
  }

  private void DisplayMenuBackground()
  {
    GUI.Box(new Rect(0, 0, display_rect.GetRect().width, display_rect.GetRect().height), "", background_style);
  }

  private void LoadSelectedMenu(int selection)
  {
    selectable_menus[selection].Display();
  }
}