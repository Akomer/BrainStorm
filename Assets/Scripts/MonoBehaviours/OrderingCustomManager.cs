using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderingCustomManager : MonoBehaviour {

    public Slider widthSlider;
    public Slider heightSlider;
    [Tooltip("Main Menu Manager")]
    public OrderingMenuManager manager;
    
    public void StartCustomGame()
    {
        manager.StartCustomGame((int)widthSlider.value, (int)heightSlider.value);
    }
}
