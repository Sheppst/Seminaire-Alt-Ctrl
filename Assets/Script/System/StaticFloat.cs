using Unity.VisualScripting;
using UnityEngine;

public class StaticFloat : MonoBehaviour 
{
    public float DelaySubtext;
    public static StaticFloat instance;

    private void Start()
    {
        instance = this;
    }
}
