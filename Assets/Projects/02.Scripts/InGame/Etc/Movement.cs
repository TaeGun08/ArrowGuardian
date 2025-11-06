using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private IMovement movement;

    private void Start()
    {
        movement = GetComponent<IMovement>();
    }

    private void Update()
    {
        transform.Translate(Vector3.down * (movement.Speed * Time.deltaTime), Space.World);
    }
}
