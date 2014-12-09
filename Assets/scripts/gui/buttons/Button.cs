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

  private Subscriber<RescaleHUDEvent> rescale_events = new Subscriber<RescaleHUDEvent>();

  protected virtual void Awake()
  {
    ResizeText();
    HUDEvents.AddSubscriber(rescale_events);
  }

  void Update()
  {
    if (!rescale_events.IsEmpty())
    {
      ResizeText();
      rescale_events.DeleteNewestMessage();
    }
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
