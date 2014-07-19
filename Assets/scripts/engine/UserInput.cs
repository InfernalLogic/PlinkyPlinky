using UnityEngine;
using System.Collections;

public class UserInput : Singleton<UserInput> 
{
	private Plinker plinker = null;
	private GameObject plinker_object = null;
  [SerializeField]
  private ScalingRect play_field_rect;

	void Start()
	{
		plinker_object = GameObject.FindGameObjectWithTag("plinker");
		plinker = plinker_object.GetComponent<Plinker>();
	}

	void OnGUI()
	{
		LoadInvisibleDropBombButton();
	}

	public void LoadInvisibleDropBombButton()
	{
		if (GUI.Button (play_field_rect.GetRect(), "", GUIStyle.none))
		{
			plinker.DropBomb();
		}
	}

  public ScalingRect GetPlayFieldRect()
  {
    return play_field_rect;
  }
}