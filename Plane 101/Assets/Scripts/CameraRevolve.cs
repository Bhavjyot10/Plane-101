using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRevolve : MonoBehaviour
{
    public float camSpeed;
    void Update()
    {
        transform.Rotate(new Vector3(0f, 360f * Time.deltaTime * camSpeed, 0f));       
    }
}
