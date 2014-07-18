using UnityEngine;

[System.Serializable]
public class BGObjectProperty
{
	public float minimum;
	public float maximum;
	[HideInInspector]
	public float range;

	public void CalculateRange()
	{
		range = maximum - minimum;
	}
}
