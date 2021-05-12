using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popupMessage : Singleton<popupMessage> {
    
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
        initialValue();
    }

    public void Appearance(string value)
    {
        _text.text = value;
    }
    public void initialValue()
    {
        _text.text = null;
    }
}
