using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugLogView : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject text;

    void Start()
    {
        var components = GetComponentsInChildren<TextMeshProUGUI>();
        if(components.Length > 1) {
            Debug.LogWarningFormat("multi text components not supported. length:{}", components.Length);
        }
        Application.logMessageReceived += OnReceived;
    }

    public void OnReceived(string str, string stacktrace, LogType type)
    {

        var components = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var component in components)
        {
            component.text += type + ":" + str + "\n";
            break;
        }

        // 強制スクロール
        GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
