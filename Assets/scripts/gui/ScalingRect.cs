using UnityEngine;
using System.Collections;

public class ScalingRect : MonoBehaviour 
{
  [SerializeField]
  private string rect_name;
  [SerializeField]
  private Rect dimensions_in_percent_of_screen;

  private Rect scaled_rect;

  void Awake()
  {
    RecalculateDimensions();
  }

  public void RecalculateDimensions()
  {
    scaled_rect.width = CalculateWidth();
    scaled_rect.height = CalculateHeight();
    scaled_rect.x = CalculateXPosition();
    scaled_rect.y = CalculateYPosition();
  }

  private float CalculateWidth()
  {
    return Screen.width * (dimensions_in_percent_of_screen.width / 100.0f);
  }

  private float CalculateHeight()
  {
    return Screen.height * (dimensions_in_percent_of_screen.height / 100.0f);
  }

  private float CalculateXPosition()
  {
    return Screen.width * (dimensions_in_percent_of_screen.x / 100.0f);
  }

  private float CalculateYPosition()
  {
    return Screen.height * (dimensions_in_percent_of_screen.y / 100.0f);
  }

  public Rect GetRect()
  {
    return scaled_rect;
  }

  public void SetRect(Rect rect)
  {
    scaled_rect = rect;
  }
}
