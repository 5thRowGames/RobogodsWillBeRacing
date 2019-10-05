using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = (T) FindObjectOfType(typeof(T));

                if (!m_instance && Application.isEditor)
                    Debug.LogError("Instance of" + typeof(T) + " not found");
                else
                    DontDestroyOnLoad(m_instance);
                
            }

            return m_instance;
        }
    }
}
