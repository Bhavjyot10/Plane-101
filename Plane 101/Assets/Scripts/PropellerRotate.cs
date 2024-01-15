using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerRotate : MonoBehaviour
{
    public float propellerSpeed;
    public Vector3 rotation;
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * propellerSpeed);
    }
}
