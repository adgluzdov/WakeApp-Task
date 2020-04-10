using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speedMove = 3f;
    public Vector3 offsetPosition;
    public Vector3 offsetRotation;

    void Update()
    {
        var z = Mathf.Lerp(transform.position.z - offsetPosition.z, target.transform.position.z, Time.deltaTime * speedMove);
        transform.position = offsetPosition + Vector3.forward * z;
        transform.rotation = Quaternion.Euler(offsetRotation);
    }
}
