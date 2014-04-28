using UnityEngine;

[System.Serializable]
public class BG_Object_Property
{
	public float minimum;
	public float maximum;
	[HideInInspector]
	public float range;
	
	public BG_Object_Property()
	{
		Debug.Log ("constructed");
		range = maximum - minimum;
		Debug.Log (range);
	}

	public void CalculateRange()
	{
		range = maximum - minimum;
		Debug.Log (range);
	}
}
