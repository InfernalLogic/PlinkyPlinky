using UnityEngine;
using System.Collections;

public class UnlockablesMenu : HUDField
{
  protected override void DisplayGUIElements()
  {
    if (GUI.Button(new Rect(0, 0, 100, 100), "Unlockables"))
    {
      Debug.Log("Unlockables");
    }
  }
}
