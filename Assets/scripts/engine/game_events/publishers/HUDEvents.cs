using UnityEngine;
using System.Collections;

public class HUDEvents : MonoBehaviour
{
  [SerializeField]
  float poll_rate = 0.33f;

  public delegate void ScreenResize();
  public static event ScreenResize OnScreenResize;

  static float last_screen_width;

  void Start()
  {
    StartCoroutine(CheckForScreenResize());
  }

  private IEnumerator CheckForScreenResize()
  {
    if (ScreenDimensionsHaveBeenChanged())
    {
      OnScreenResize();
      ResetLastScreenWidth();
    }

    yield return new WaitForSeconds(poll_rate);
    StartCoroutine(CheckForScreenResize());
  }

  private bool ScreenDimensionsHaveBeenChanged()
  {
    return last_screen_width != Screen.width;
  }

  private void ResetLastScreenWidth()
  {
    last_screen_width = Screen.width;
  }


}
