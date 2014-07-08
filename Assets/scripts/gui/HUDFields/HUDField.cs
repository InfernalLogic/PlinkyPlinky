using UnityEngine;
using System.Collections;

public abstract class HUDField : PlinkyObject
{
	[SerializeField]
	protected float width_in_percent_of_screen;
	[SerializeField]
	protected float height_in_percent_of_screen;
	[SerializeField]
	protected float x_position_in_percent_of_screen;
	[SerializeField]
	protected float y_position_in_percent_of_screen;

	protected Rect display_rect;

  [SerializeField]
  protected GUIStyle background_style;

  protected abstract void DisplayGUIElements();

	protected virtual void Start()
	{
		InitializeDisplayArea();
	}

	private void InitializeDisplayArea()
	{
		display_rect.width = CalculateWidth ();
		display_rect.height = CalculateHeight ();

		display_rect.x = CalculateXPosition ();
		display_rect.y = CalculateYPosition ();
	}

	public void Display()
	{
		GUI.BeginGroup (display_rect);
		DisplayGUIElements();
		GUI.EndGroup();
	}

	float CalculateXPosition ()
	{
		return Screen.width * (x_position_in_percent_of_screen / 100.0f);
	}

	float CalculateYPosition ()
	{
		return Screen.height * (y_position_in_percent_of_screen / 100.0f);
	}

	float CalculateHeight ()
	{
		return Screen.height * (height_in_percent_of_screen / 100.0f);
	}

	float CalculateWidth ()
	{
		return Screen.width * (width_in_percent_of_screen / 100.0f);
	}

  public Rect GetDisplayRect()
  {
    return display_rect;
  }
}