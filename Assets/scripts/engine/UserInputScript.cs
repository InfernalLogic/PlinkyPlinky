using UnityEngine;
using System.Collections;

public class UserInputScript : MonoBehaviour {

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

	void Update () 
  {
    if (Input.GetButtonDown("Fire1"))
    {
				plinker.DropBomb();
    }
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