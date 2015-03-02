using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
  public static T Instance { get { return SafeInstance(); } }
  private static T instance;

  protected void Awake()
  {
    CheckForDuplicates();
  }

  private static T SafeInstance()
  {
    if (!instance)
    {
      instance = FindObjectOfType<T>();
      if (!instance)
      {
        Debug.LogWarning("No instance of " + typeof(T).ToString() + " was found. Creating new instance...");
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
