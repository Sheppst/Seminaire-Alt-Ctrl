using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWorld : MonoBehaviour
{
    Dictionary<string, Action> actions = new Dictionary<string, Action>();
    float delay;

    public TextMeshProUGUI PlayerWText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        delay = StaticFloat.instance.DelaySubtext;
    }
}
