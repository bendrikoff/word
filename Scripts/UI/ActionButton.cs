using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public Button Button;

    public void Init(string text, UnityAction action)
    {
        Text.text = text;
        Button.onClick.AddListener(action);
    }

    public void AddAction(UnityAction action) => Button.onClick.AddListener(action);
}
