using UnityEngine;
using System.Collections;

public class Sitelocker : MonoBehaviour
{
  string url = "url";
  string src = "src";
  string quit_message = "not quitting";

  void Start()
  {
    if (Application.isWebPlayer)
    {
      url = Application.absoluteURL;
      src = Application.srcValue;

      if (src != "should_fail.unity3d")
        Destroy(this.gameObject);
    }
  }

  private void OnGUI()
  {
    GUI.Label(new Rect(0, 0, 300, 100), url);
    GUI.Label(new Rect(0, 50, 300, 100), src);
    GUI.Label(new Rect(0, 100, 300, 100), quit_message);
  }

  private IEnumerator DelayedQuit(float delay)
  {
    quit_message = "quitting";
    yield return new WaitForSeconds(delay);
    Application.Quit();
  }
}