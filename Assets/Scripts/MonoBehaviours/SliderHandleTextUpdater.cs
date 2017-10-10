using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SliderHandleTextUpdater : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}

    public void SliderValueChanged(float value)
    {
        text.text = ((int)value).ToString();
    }

}
