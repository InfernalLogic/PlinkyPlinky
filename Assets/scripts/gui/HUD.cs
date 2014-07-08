using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class HUD : PlinkyObject
{

  /*
  public void LoadCoinUpgradeButton()
  {
    if (GUI.Button (new Rect(((MenuDisplayField.width / 2) - (coin_upgrade_icon.width / 2)), 
                             0, upgrade_button_width, upgrade_button_height), 
                             coin_upgrade_icon, GUIStyle.none))
    {
      engine.player_stats.coin_upgrader.Upgrade();
    }

    GUI.Label (new Rect(((MenuDisplayField.width / 2) - (coin_upgrade_icon.width / 2)) + (upgrade_button_width / 4), 
                        0, upgrade_button_width, upgrade_button_height), "$/coin hit: ");
  }
	
  public void LoadPegUpgradeButton()
  {
    if (GUI.Button (new Rect(((MenuDisplayField.width / 2) - (peg_upgrade_icon.width / 2)), 
                             MenuDisplayField.height / 3, upgrade_button_width, upgrade_button_height), 
                             peg_upgrade_icon, GUIStyle.none))
    {
      engine.player_stats.peg_upgrader.Upgrade();
    }
  }
	
  public void LoadBumperUpgradeButton()
  {
    if (GUI.Button (new Rect(((MenuDisplayField.width / 2) - (bumper_upgrade_icon.width / 2)), 
                             (MenuDisplayField.height * 2 )/ 3, upgrade_button_width, upgrade_button_height), 
                    peg_upgrade_icon, GUIStyle.none))
    {
      engine.player_stats.bumper_upgrader.Upgrade();
    }
  }
*/
  public HUDField[] HUD_fields;

  void OnGUI()
  {
    foreach (HUDField field in HUD_fields)
    {
      field.Display();
    }
  }
}