using UnityEngine;
using System.Collections;

abstract public class Button : PlinkyObject 
{
  [SerializeField]
  protected string button_text;
  [SerializeField]
  protected GUIStyle button_style;
  [SerializeField]
  protected ScalingRect display_rect;

  public abstract void Display();
}
