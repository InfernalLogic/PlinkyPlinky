using UnityEngine;
using System.Collections.Generic;

public abstract class GeometricSpawner : MonoBehaviour
{
  protected abstract void SpawnObjects();

  protected virtual void Awake()
  {
    if (OutdatedChildrenFound())
      DestroyAllChildren();

    SpawnObjects();
  }

  protected bool OutdatedChildrenFound()
  {
    return transform.childCount > 0;
  }

  protected void DestroyAllChildren()
  {
    List<GameObject> children = new List<GameObject>();

    foreach (Transform child in transform)
      children.Add(child.gameObject);

    foreach (GameObject child in children)
      DestroyImmediate(child);
  }

  protected void MakeObjectAChild(GameObject target_object)
  {
    target_object.transform.parent = gameObject.transform;
  }
}
