using UnityEngine;
using System.Collections;

public class PrefabLinker : MonoBehaviour 
{
  [SerializeField]
	private GameObject linked_prefab;

  private GameObject new_object;

	void Awake()
	{
    //if the object has children, they could be outdated instances of the prefab this links to.
    if (ObjectHasChildren())
      DestroyPreviousVersionsOfPrefab();

    new_object = InstantiateUpdatedPrefab();
    NormalizeObjectRotation(new_object);
    MakeObjectAChild(new_object);

	}

  private GameObject InstantiateUpdatedPrefab()
  {
    return GameObject.Instantiate(linked_prefab,
                                           transform.position,
                                           Quaternion.identity) as GameObject;
  }

  private void NormalizeObjectRotation(GameObject target_object)
  {
    target_object.transform.Rotate(transform.eulerAngles / 2);
  }

  private void MakeObjectAChild(GameObject target_object)
  {
    target_object.transform.parent = gameObject.transform;
  }

  private void DestroyPreviousVersionsOfPrefab()
  {
    ArrayList children = new ArrayList();
    AddChildrenToBeDestroyed(children);
    DestroyAllChildren(children);

    if (ObjectHasChildren())
      Debug.Log(gameObject.name + "children failed to destroy. childcount: " + transform.childCount);
  }

  private void AddChildrenToBeDestroyed(ArrayList children)
  {
    foreach (Transform child in transform)
    {
      children.Add(child.gameObject);
    }
  }

  private static void DestroyAllChildren(ArrayList children)
  {
    foreach (GameObject child in children)
    {
      DestroyImmediate(child);
    }
  }

  private bool ObjectHasChildren()
  {
    return transform.childCount > 0;
  }
}
