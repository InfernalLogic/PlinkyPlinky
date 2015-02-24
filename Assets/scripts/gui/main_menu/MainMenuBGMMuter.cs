using UnityEngine;
using System.Collections;

public class MainMenuBGMMuter : MonoBehaviour 
{
  public float default_music_volume = 0.75f;
  public SavedBool mute_bgm;

  void Start()
  {
    InitializeBGM();

    print(mute_bgm.GetKey() + " initialized to " + mute_bgm.GetValue());
  }

  private void InitializeBGM()
  {
    if (mute_bgm.IsTrue())
      GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().volume = 0.0f;
    else
      GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().volume = default_music_volume;
  }
}
