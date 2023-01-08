using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float pivotSpeed;
    public Transform pivot;
    [SerializeField] private Vector3 pivotOffset = new Vector3(10, 10, 0);
    public float CurrentRotation = 0f;
    void Start()
    {
        
    }

    void RotateAround()
    {
        if (pivot == null) return;

        CurrentRotation = (CurrentRotation + pivotSpeed * Time.deltaTime) % 360;
        var position = new Vector3
        {
            x = pivot.position.x + pivotOffset.x * Mathf.Sin(CurrentRotation * Mathf.Deg2Rad),
            y = pivot.position.y + pivotOffset.y * Mathf.Cos(CurrentRotation * Mathf.Deg2Rad),
            z = pivot.position.z
        };
        transform.position = position;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime), Space.Self);
        RotateAround();   
    }
}
