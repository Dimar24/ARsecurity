using UnityEngine;

namespace Pattern
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance) 
                    return _instance;
                
                var findObjectsOfType = FindObjectsOfType(typeof(T)) as T[];
                if (findObjectsOfType == null || findObjectsOfType.Length <= 0)
                {
                    var newGo = new GameObject(typeof(T).Name, typeof(T));
                    _instance = newGo.GetComponent<T>();
                    DontDestroyOnLoad(newGo);

                    return _instance;
                }
            
                _instance = findObjectsOfType[0];
                var go = _instance.gameObject;
                go.name = typeof(T).Name;
                DontDestroyOnLoad(go);
            
                if (findObjectsOfType.Length == 1) 
                    return _instance;
            
                Debug.LogError("You have more than one " + typeof(T).Name +
                               " in the scene. You only need 1, it's a singleton!");

                for (var i = 1; i < findObjectsOfType.Length; ++i)
                    Destroy(findObjectsOfType[i].gameObject);

                return _instance;
            }
            set
            {
                if (_instance != null)
                {
                    Destroy(value.gameObject);
                    return;
                }
                _instance = value;
                if (Application.isPlaying)
                    DontDestroyOnLoad(value);
            }
        }
    }
}
