using UnityEngine;
using System.Collections;

public class BGWallpaper : MonoBehaviour 
{
	public float alpha = 1f;
	Color color_changer;

	void Awake()
	{
		color_changer = transform.renderer.material.color;
		SetAlpha ();
	}

	public void SetAlpha()
	{
		color_changer.a = alpha;
		transform.renderer.material.color = color_changer;
	}
}
