using UnityEngine;
using System.Collections;

public abstract class SelectableMenu : HUDField 
{
  SelectedMenu parent_field;
  protected override abstract void DisplayGUIElements();

  
  void Start()
  {
    /*
    parent_field = gameObject.GetComponentInParent<SelectedMenu>() as SelectedMenu;

    InitializeDisplayArea();

    display_area.x = parent_field.GetDisplayArea().x;
    display_area.y = parent_field.GetDisplayArea().y;
    display_area.width = parent_field.GetDisplayArea().width;
    display_area.height = parent_field.GetDisplayArea().height;

    Debug.Log(parent_field.GetDisplayArea().width + " width for " + gameObject.name);
    */
  }

  private void InitializeDisplayArea()
  {
    display_rect.x = parent_field.GetDisplayRect().x;
    display_rect.y = parent_field.GetDisplayRect().y;
    display_rect.width = parent_field.GetDisplayRect().width;
    display_rect.height = parent_field.GetDisplayRect().height;
  }
  

  public void SetDisplayArea(Rect display_area)
  {
    this.display_rect.x = display_area.x;
    this.display_rect.x = display_area.x;
  }
}
