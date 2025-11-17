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
        if (movement.IsStop) return;
        
        transform.Translate(Vector3.down * (movement.Speed * Time.deltaTime), Space.World);

        float distance = Vector2.Distance(transform.position, new Vector2(transform.position.x, -3f));
        
        if (distance < 0.1f) movement.IsStop = true;
    }
}
