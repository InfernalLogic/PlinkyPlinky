using UnityEngine;

[System.Serializable]
public class AchievementPopupInfo
{
  public int plinkagon_point_value;
  public string achievement_text_verb;
  public Texture popup_background;
  [HideInInspector]
  public string achievement_name;
  [HideInInspector]
  public string achievement_text_number;
}
