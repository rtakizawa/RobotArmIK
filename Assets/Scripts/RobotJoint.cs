using UnityEngine;

public class RobotJoint : MonoBehaviour
{
    [SerializeField]
    public Vector3 AxisOffset;

    public Vector3 Axis;

    public Vector3 StartOffset;

    public float MinAngle = 0f;
    public float MaxAngle = 180f;

    public float LocalRot;

    void Update()
    {
        StartOffset = transform.localPosition;
        if (AxisOffset.z == 1)
        {
            Axis = transform.forward;
            LocalRot = transform.localEulerAngles.z;
        }
        else if (AxisOffset.y == 1)
        {
            Axis = transform.up;
            LocalRot = transform.localEulerAngles.y;
        }
        else
        {
            Axis = transform.right;
            LocalRot = transform.localEulerAngles.x;
        }
        if (LocalRot > 180f)
        {
            LocalRot -= 360;
        }
    }

    public float GetLocalRotation()
    {
        if (AxisOffset.z == 1)
        {
            LocalRot = transform.localEulerAngles.z;
        }
        else if (AxisOffset.y == 1)
        {
            LocalRot = transform.localEulerAngles.y;
        }
        else
        {
            LocalRot = transform.localEulerAngles.x;
        }
        if (LocalRot > 180f)
        {
            LocalRot -= 360;
        }

        return LocalRot;
    }

    public void SetLocalRotation()
    {
        float localRot;
        if (AxisOffset.z == 1)
        {
            localRot = transform.localEulerAngles.z;
            if (localRot > 180f)
            {
                localRot -= 360;
            }
            transform.localEulerAngles = Vector3.forward * Mathf.Clamp(localRot, MinAngle, MaxAngle);
        }
        else if (AxisOffset.y == 1)
        {
            localRot = transform.localEulerAngles.y;
            if (localRot > 180f)
            {
                localRot -= 360;
            }
            transform.localEulerAngles = Vector3.up * Mathf.Clamp(localRot, MinAngle, MaxAngle);
        }
        else
        {
            localRot = transform.localEulerAngles.x;
            if (localRot > 180f)
            {
                localRot -= 360;
            }
            transform.localEulerAngles = Vector3.right * Mathf.Clamp(localRot, MinAngle, MaxAngle);
        }
    }
}
