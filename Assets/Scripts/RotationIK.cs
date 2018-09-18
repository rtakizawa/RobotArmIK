using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using System.Text;

public class RotationIK : MonoBehaviour
{
    public Transform target;
    public Transform effector;
    public Vector3 direction;
    public int axisNum;
    private Vector3 targetPos;
    private float[] data = new float[6];
    private float[] angle = new float[6];

    private float m_MinAngle = 0f;
    private float m_MaxAngle = 90f;
    

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
        //if (angle[axisNum] != data[axisNum])
        //{
        //    targetPos = target.position;
        //    transform.RotateAround(targetPos, target.up, (data[axisNum] * 90) - angle[axisNum]);
        //    angle[axisNum] = data[axisNum] * 90;
        //}

        GetIK(target.position, effector.position, gameObject);
    }
    
    public void GetIK(Vector3 targetPos, Vector3 effectorPos, GameObject axisT)
    {
        for (GameObject axis = axisT; axis != null; axis = axis.transform.parent.gameObject)
        {
            if (axis.transform.parent.parent == null)
            {
                Vector3 targetDir = (targetPos - axis.transform.position);
                Vector3 newTargetDir = Vector3.ProjectOnPlane(targetDir, axis.transform.forward).normalized;
                float rotDot = Vector3.Dot(axis.transform.right, newTargetDir);
                float rotAngle = (float)Mathf.Acos(rotDot);
                Vector3 rotAxis = Vector3.Cross(axis.transform.right, newTargetDir).normalized;
                axis.transform.RotateAround(axis.transform.position, rotAxis, rotAngle * Time.deltaTime);

                break;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            for (GameObject axis = axisT; axis != null; axis = axis.transform.parent.gameObject)
            {
                if (axis.transform.parent.parent == null)
                {
                    break;
                }

                Vector3 parentPos = axis.transform.parent.position;

                Vector3 dirToEffector = (effectorPos - parentPos).normalized;
                Vector3 dirToTarget = (targetPos - parentPos).normalized;

                float rotationDotProduct = Vector3.Dot(dirToEffector, dirToTarget);
                float rotationAngle = (float)Mathf.Acos(rotationDotProduct);
                rotationAngle = Mathf.Clamp(rotationAngle, m_MinAngle, m_MaxAngle);
                if (rotationAngle > 1.0e-5f)
                {
                    // 回転軸
                    Vector3 rotationAxis = Vector3.Cross(dirToEffector, dirToTarget).normalized;

                    axis.transform.RotateAround(parentPos, rotationAxis, rotationAngle * Time.deltaTime);
                }
                
            }

            if (Vector3.Distance(targetPos, effectorPos) <= 0.001f)
            {
                return;
            }
        } 
        
    }
}