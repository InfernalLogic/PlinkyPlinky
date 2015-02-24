using UnityEngine;
using System.Collections;

public class UserInput : Singleton<UserInput> 
{
	private Plinker plinker = null;
	private GameObject plinker_object = null;
  [SerializeField]
  private ScalingRect play_field_rect;

  string url = "url";

  private bool is_pirated = true;

	void Start()
	{
		plinker_object = GameObject.FindGameObjectWithTag("plinker");
		plinker = plinker_object.GetComponent<Plinker>();

    if (Application.isWebPlayer)
    {
      url = Application.absoluteURL;

      if (url.Contains("http://www.kongregate.com")  || 
          url.Contains("https://www.kongregate.com") ||
          url.Contains("kongregate.com"))
        is_pirated = false;
    }

    if (Application.isEditor)
      is_pirated = false;
	}

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space) && !is_pirated)
      plinker.DropBomb();
  }

	void OnGUI()
	{
    if (!is_pirated)
		  LoadInvisibleDropBombButton();
	}

	public void LoadInvisibleDropBombButton()
	{
		if (GUI.Button (play_field_rect.GetRect(), "", GUIStyle.none))
			plinker.DropBomb();
	}

  public ScalingRect GetPlayFieldRect()
  {
    return play_field_rect;
  }
}