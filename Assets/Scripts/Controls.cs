using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    void Start()
    {
#if !UNITY_IOS && !UNITY_ANDROID
        Destroy(gameObject);
#endif   
    }
}
