using System;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    public static GlobalReferences Instance { get; private set; }
    
    public GameObject bulletImpactEffect;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
