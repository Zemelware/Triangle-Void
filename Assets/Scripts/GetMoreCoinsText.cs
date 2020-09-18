using UnityEngine;

public class GetMoreCoinsText : MonoBehaviour
{
    void Start()
    {
        
        #if !UNITY_IOS && !UNITY_ANDROID
        Destroy(gameObject);
        #endif
        
    }

}
