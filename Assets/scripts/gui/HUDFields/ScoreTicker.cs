using UnityEngine;
using System.Collections;

public class ScoreTicker : HUDField
{
  [SerializeField]
  private GUIStyle label_style;
  [SerializeField]
  private ScalingRect money_display_rect;

  private TextAnchor original_alignment;

  void Awake()
  {
    ResizeText();
    original_alignment = label_style.alignment;
  }

  void OnEnable()
  {
    HUDEvents.OnScreenResize += ResizeText;
  }

	protected override void DisplayGUIElements()
	{
		DisplayBackgroundBox ();
		DisplayCurrentMoney ();
	}

	void DisplayBackgroundBox ()
	{
    GUI.Box(new Rect(0, 0, display_rect.GetRect().width, display_rect.GetRect().height), "", background_style);
	}

	void DisplayCurrentMoney ()
	{
    GUI.Label(money_display_rect.GetRect(), "$: ", label_style);

    label_style.alignment = TextAnchor.MiddleRight;
    GUI.Label(money_display_rect.GetRect(), MoneyTracker.Instance().GetCurrentMoney().ToString(), label_style);
    label_style.alignment = original_alignment;
	}

  public void ResizeText()
  {
    label_style.fontSize = (int)Screen.height / 15;
  }
}
