using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpawnExample : MonoBehaviour
{
    public Transform followUpTarget;
    public float FollowSpeed;
    public Vector3 offset;
    public LineRenderer LaserLine;
    public float RotationSmoothSpeed;

    public LayerMask Mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 direction = Camera.main.transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f, Mask))
        {
            //SmoothLookAt(hit);
            transform.LookAt(hit.point);
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, followUpTarget.position, FollowSpeed * Time.deltaTime);


        
    }

    private void SmoothLookAt(RaycastHit hit)
    {
        Vector3 lookDirection = (hit.point - transform.position);
        Quaternion rotationToLookAt = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToLookAt, Time.deltaTime * RotationSmoothSpeed);
    }
}
