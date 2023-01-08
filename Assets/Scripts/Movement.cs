using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Speed;

    public void Translate(Vector2 moveDirection)
    {
        this.transform.Translate(Speed * Time.deltaTime * moveDirection);
    }
}
