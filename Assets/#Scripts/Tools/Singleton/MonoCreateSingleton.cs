﻿public abstract class MonoCreateSingleton<T> : BaseMonoSingleton<T> where T : MonoCreateSingleton<T>
{
    #region Fields

    /// <summary>
    /// The instance.
    /// </summary>
    private static T instance;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Gets the instance.
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindInstance();

                if (instance == null)
                {
                    instance = CreateInstance();
                }
            }

            return instance;
        }
    }

    public bool IsInstance => Instance == this;
    public static bool HasInstance => instance != null;

    #endregion Properties

    #region Methods

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    #endregion Methods
}
