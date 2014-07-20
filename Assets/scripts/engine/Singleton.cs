using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
  private static T instance;

  protected void Awake()
  {
    CheckForDuplicates();
  }

  public static T Instance()
  {
    if (instance == null)
    {
      instance = FindObjectOfType<T>();
      if (instance == null)
      {
        Debug.LogError("No instance of " + typeof(T).ToString() + " was found. Instantiating a new singleton.");
        GameObject new_instance = new GameObject();

        new_instance.AddComponent<T>();

        instance = new_instance.GetComponent<T>();
      }
    }

    return instance;
  }

  public void CheckForDuplicates()
  {
    if (FindObjectsOfType<T>().Length > 1)
    {
      Debug.LogException(new System.Exception("Duplicate " + typeof(T).ToString() + " singleton found!"));
    }
  }

}
