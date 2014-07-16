using UnityEngine;
using System.Collections;

public class PlinkyObject : MonoBehaviour 
{
	protected PlinkyEngine engine;

	protected void Awake()
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

	public PlinkyEngine GetEngine()
	{
		return engine;
	}

	public void SetEngine(PlinkyEngine new_engine)
	{
		engine = new_engine;
	}
}
