using UnityEngine;

public abstract class BaseMonoSingleton<T> : MonoBehaviour where T : BaseMonoSingleton<T>
{
    #region Methods

    public virtual void SetAsDontDestroy()
    {
        GameObject.DontDestroyOnLoad(gameObject);

        if (!name.Contains("DontDestroy"))
        {
            name += " [DontDestroy]";
        }
    }

    protected static T FindInstance()
    {
        return FindObjectOfType<T>();
    }

    protected static T CreateInstance()
    {
        GameObject obj = new GameObject();
        obj.name = typeof(T).Name;
        var instance = obj.AddComponent<T>();

        return instance;
    }

    #endregion Methods
}
