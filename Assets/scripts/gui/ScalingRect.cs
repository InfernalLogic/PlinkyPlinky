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
    RecalculateRect();
  }

  public void RecalculateRect()
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

  private void RecalculateDimensions()
  {
    dimensions_in_percent_of_screen.y = 100 * scaled_rect.y / Screen.height;
    dimensions_in_percent_of_screen.x = 100 * scaled_rect.x / Screen.width;
    dimensions_in_percent_of_screen.width = 100 * scaled_rect.width / Screen.width;
    dimensions_in_percent_of_screen.height = 100 * scaled_rect.height / Screen.height;
  }

  public Rect GetDimensions()
  {
    return dimensions_in_percent_of_screen;
  }

  public Rect GetRect()
  {
    return scaled_rect;
  }

  public void SetRect(Rect rect)
  {
    scaled_rect = rect;
    RecalculateRect();
  }

  public void SetX(float x)
  {
    dimensions_in_percent_of_screen.x = x;
    RecalculateRect();
  }

  public void SetY(float y)
  {
    dimensions_in_percent_of_screen.y = y;
    RecalculateRect();
  }

  public void SetWidth(float width)
  {
    dimensions_in_percent_of_screen.width = width;
    RecalculateRect();
  }

  public void SetHeight(float height)
  {
    dimensions_in_percent_of_screen.height = height;
    RecalculateRect();
  }
}
