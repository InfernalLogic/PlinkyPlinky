using UnityEngine;
using System.Collections;

abstract public class Button : MonoBehaviour 
{
  [SerializeField]
  protected ScalingRect display_rect;
  [SerializeField]
  protected string button_text;
  [SerializeField]
  protected Texture disabled_mask_texture;
  [SerializeField]
  protected GUIStyle button_style;
  [SerializeField]
  protected GUIStyle label_style;
  [SerializeField]
  protected ScalingRect label_display_rect;

  public abstract void Display();

  protected virtual void Awake()
  {
    ResizeText();
  }

  void OnEnable()
  {
    HUDEvents.OnScreenResize += this.ResizeText;
  }

  protected void DisplayDisabledMask()
  {
    GUI.DrawTexture(display_rect.GetRect(), disabled_mask_texture);
  }

  public void ResizeText()
  {
    label_style.fontSize = (int)Screen.height / 35;
  }
}
