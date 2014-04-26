using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PrefabLinker : MonoBehaviour 
{
	public GameObject prefab;

	private GameObject new_object;

	void Awake()
	{
		//var children = new List<GameObject>();
		//foreach (Transform child in transform) children.Add(child.gameObject)
		//children.ForEach(child => Destroy(child));

		if(transform.childCount > 0)
		{
			ArrayList children = new ArrayList();
			foreach (Transform child in transform)
			{
				children.Add (child.gameObject);
			}

			foreach (GameObject child in children)
			{
				DestroyImmediate(child);
			}
		}

		if (transform.childCount != 0)
		{
		Debug.Log (gameObject.name + "children failed to destroy. childcount: "  + transform.childCount);
		}

		new_object = GameObject.Instantiate (prefab, 
		                                     transform.position, 
		                                     transform.rotation) as GameObject;

		new_object.transform.parent = gameObject.transform;
	}
}
