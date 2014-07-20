using UnityEngine;
using System.Collections;

public class MenuSelector : HUDField 
{
  private int selected_menu = 0;
  [SerializeField]
  private string[] selected_menu_text;
  [SerializeField]
  private GUIStyle button_style;
  [SerializeField]
  private ScalingRect selection_grid_rect;

  protected override void DisplayGUIElements()
  {
    LoadMenuSelectionGrid();
  }

  void LoadMenuSelectionGrid()
  {
    GUI.Box(new Rect(0, 0, display_rect.GetRect().width, display_rect.GetRect().height), "", background_style);

    selected_menu = GUI.SelectionGrid(selection_grid_rect.GetRect(), selected_menu, selected_menu_text, 2, button_style);
  }

  public int GetSelectedMenu()
  {
    return selected_menu;
  }
}
