using UnityEngine;
using System.Collections;

public class MenuSelector : HUDField 
{
  private int selected_menu;
  private string[] selected_menu_text = new string[] {"Upgrades", "Unlockables",
																											"Achievements", "Options"};
  [SerializeField]
  private GUIStyle button_style;

  private Rect selection_grid_area;

  void Start()
  {
    base.Start();
    InitializeSelectionGridArea();
  }

  protected override void DisplayGUIElements()
  {
    LoadMenuSelectionGrid();
  }

  void LoadMenuSelectionGrid()
  {
    GUI.Box(new Rect(0, 0, display_rect.width, display_rect.height), "", background_style);

    selected_menu = GUI.SelectionGrid(selection_grid_area, selected_menu, selected_menu_text, 2, button_style);
  }

  public int GetSelectedMenu()
  {
    return selected_menu;
  }

  private void InitializeSelectionGridArea()
  {
    selection_grid_area.x = display_rect.width / 32;
    selection_grid_area.y = display_rect.height / 15; 
    selection_grid_area.width = display_rect.width - (display_rect.width / 16);
    selection_grid_area.height = display_rect.height - (display_rect.height / 7.5f);
  }
}
