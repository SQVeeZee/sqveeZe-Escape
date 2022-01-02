using Zenject;

public abstract class ZenjectSingleton<T> : IInitializable, ILateDisposable where T : ZenjectSingleton<T>, new()
{
    #region Fields

    private static T _instance;

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
            // if (_instance == null)
            // {
            //     _instance = new T();
            // }

            return _instance;
        }
    }

    protected bool IsInstance => Instance == this;
    public static bool HasInstance => _instance != null;

    #endregion Properties

    #region Methods

    protected ZenjectSingleton()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
    }

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public virtual void Initialize()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
    }

    public void LateDispose()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    #endregion Methods
}
