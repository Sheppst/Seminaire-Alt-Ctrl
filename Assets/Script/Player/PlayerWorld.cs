using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Windows.Speech;

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
