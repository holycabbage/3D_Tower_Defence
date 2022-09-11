using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T mInstance;
    public static T Instance
    {
        get
        {
            if (null == mInstance)
            {
                mInstance = FindObjectOfType(typeof(T)) as T; 
                if (null == mInstance)
                {
                    GameObject singleton = new GameObject(typeof(T).ToString());
                    mInstance = singleton.AddComponent<T>(); 
                    DontDestroyOnLoad(singleton);
                }
            }
            return mInstance;
        }
        
    }
}