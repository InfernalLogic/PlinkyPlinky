using UnityEngine;
using System.Collections;

public class BGWallpaper : MonoBehaviour 
{
	public float alpha = 1f;
	Color color_changer;

	void Awake()
	{
		color_changer = transform.GetComponent<Renderer>().material.color;
		SetAlpha ();
	}

	public void SetAlpha()
	{
		color_changer.a = alpha;
		transform.GetComponent<Renderer>().material.color = color_changer;
	}
}
