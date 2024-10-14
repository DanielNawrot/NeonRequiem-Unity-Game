using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public float xOffset;
    public float yOffset;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    private void LateUpdate()
    {
        if (transform.position.x - target.position.x > xOffset || transform.position.x - target.position.x < -xOffset)
        {

        }
        Vector3 targetPosition = target.position;
        transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
    }
}
