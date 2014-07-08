using UnityEngine;
using System.Collections;

public class ScalingRect : MonoBehaviour 
{
  [SerializeField]
  private string name;
  [SerializeField]
  private float x_in_percent;
  [SerializeField]
  private float y_in_percent;
  [SerializeField]
  private float width_in_percent;
  [SerializeField]
  private float height_in_percent;

  private Rect scaled_rect;

  void Awake()
  {
    CalculateDimensions();
  }

  public void CalculateDimensions()
  {
    scaled_rect.width = CalculateWidth();
    scaled_rect.height = CalculateHeight();

    scaled_rect.x = CalculateXPosition();
    scaled_rect.y = CalculateYPosition();
  }

  private float CalculateXPosition()
  {
    return Screen.width * (x_in_percent / 100.0f);
  }

  private float CalculateYPosition()
  {
    return Screen.height * (y_in_percent / 100.0f);
  }

  private float CalculateHeight()
  {
    return Screen.height * (height_in_percent / 100.0f);
  }

  private float CalculateWidth()
  {
    return Screen.width * (width_in_percent / 100.0f);
  }

  public Rect GetRect()
  {
    return scaled_rect;
  }
}
