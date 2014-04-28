using UnityEngine;
using System.Collections;

public class PlinkyObject : MonoBehaviour 
{
	protected PlinkyEngine engine;

	void Awake()
	{
		LoadEngine ();
	}

	protected void LoadEngine()
	{		
		engine = GameObject.FindGameObjectWithTag("engine").GetComponent<PlinkyEngine>();
		if (engine == null)
		{
			Debug.Log ("engine failed to initialize for " + gameObject.name);
		}
	}
}
