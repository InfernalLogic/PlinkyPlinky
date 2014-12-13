using UnityEngine;
using System.Collections;

public class MainMenuTransitioner : MonoBehaviour 
{
  [SerializeField]
  private string next_level = "Main";

  void OnGUI()
  {
    if (GUI.Button(new Rect(0.0f, 0.0f, Screen.width, Screen.height), "", GUIStyle.none))
    {
      Application.LoadLevel(next_level);
    }
  }
}
