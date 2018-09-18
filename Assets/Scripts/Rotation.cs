using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using System.Text;

public class Rotation : MonoBehaviour
{
    public Transform target;
    public Vector3 direction;
    public int axisNum;
    private Vector3 targetPos;
    private float[] data = new float[6];
    private float[] angle = new float[6];

    public Slider S_Slider;
    public Slider L_Slider;
    public Slider U_Slider;
    public Slider R_Slider;
    public Slider B_Slider;
    public Slider T_Slider;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            angle[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        data[0] = S_Slider.value;
        data[1] = L_Slider.value;
        data[2] = U_Slider.value;
        data[3] = R_Slider.value;
        data[4] = B_Slider.value;
        data[5] = T_Slider.value;
        if (angle[axisNum] != data[axisNum])
        {
            targetPos = target.position;
            transform.RotateAround(targetPos, target.up, (data[axisNum] * 90) - angle[axisNum]);
            angle[axisNum] = data[axisNum] * 90;
        }
    }
}
