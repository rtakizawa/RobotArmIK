using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using System.Text;

public class ModelRotationIK : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    public Transform effector;
    public Vector3 direction;
    public int axisNum;
    private float[] data = new float[6];
    private float[] angle = new float[6];
    

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
        
        GetIK(target, effector, gameObject);
        Debug.DrawLine(target.position, effector.position, Color.red);
    }
    
    public void GetIK(Transform target, Transform effector, GameObject axisT)
    {
        RobotJoint[] robotJoints = axisT.GetComponentsInParent<RobotJoint>();
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < robotJoints.Length ; j++)
            {
                var targetPos = target.position;
                var effectorPos = effector.position;

                if (Vector3.Distance(targetPos, effectorPos) <= 0.1f)
                {
                    break;
                }

                var joint = robotJoints[j].gameObject;
                var axis = robotJoints[j].Axis;
                var pos = robotJoints[j].transform.position;

                var localRot = robotJoints[j].LocalRot;
                if (j == 5)
                {
                    localRot -= 90;
                }

                //if (localRot < robotJoints[j].MinAngle || localRot > robotJoints[j].MaxAngle)
                //{
                //    break;
                //}


                Vector3 parentPos = joint.transform.position;

                Vector3 dirToEffector = (effectorPos - parentPos).normalized;
                Vector3 dirToTarget = (targetPos - parentPos).normalized;

                Vector3 dirToProjectEffector = Vector3.ProjectOnPlane(dirToEffector, axis).normalized;
                Vector3 dirToProjectTarget = Vector3.ProjectOnPlane(dirToTarget, axis).normalized;


                float rotationDotProduct = Vector3.Dot(dirToProjectEffector, dirToProjectTarget);
                float rotationAngle = (float)Mathf.Acos(rotationDotProduct) * Mathf.Rad2Deg;
                //rotationAngle = Mathf.Clamp(rotationAngle, m_MinAngle, m_MaxAngle);
                if (rotationAngle > 90f)
                {
                    rotationAngle = 180 - rotationAngle;
                }
                

                if (rotationAngle > 1f)
                {
                    // 回転軸
                    Vector3 rotationAxis = Vector3.Cross(dirToProjectTarget, dirToProjectEffector).normalized;
                    

                    robotJoints[j].transform.RotateAround(parentPos, rotationAxis, -rotationAngle * Time.deltaTime);

                    robotJoints[j].SetLocalRotation();
                }
                else
                {
                }
            }
            
        } 
        
    }
}