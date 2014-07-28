using UnityEngine;
using System.Collections;

public abstract class HUDField : MonoBehaviour
{
	[SerializeField]
	protected ScalingRect display_rect;

  [SerializeField]
  protected GUIStyle background_style;

  protected abstract void DisplayGUIElements();

  public void Display()
  {
    GUI.BeginGroup(display_rect.GetRect());
    DisplayGUIElements();
    GUI.EndGroup();
  }


}