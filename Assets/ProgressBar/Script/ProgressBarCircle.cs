using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]

public class ProgressBarCircle : MonoBehaviour
{

    public Color BarColor;

    private Image bar;

    [Range(0f, 100f)]
    public float barValue;

    private void Start()
    {
        bar = transform.Find("BarCircle").GetComponent<Image>();
        bar.color = BarColor;
        //barValue = 1;

    }

    private void Update()
    {
        bar.fillAmount = barValue / 100;
        //print(barValue / 100);

    }


}
