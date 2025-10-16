using System;
using UnityEngine;

public abstract class Arrow : MonoBehaviour, IElementType
{
    protected Camera mainCamera;
    
    [Header("Arrow Settings")]
    [SerializeField] protected ElementType elementType;
    public ElementType ElementType => elementType;
    [SerializeField] protected float arrowSpeed = 20f;
    
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag($"Enemy")) return;
        Destroy(gameObject);
    }

    protected void Start()
    {
        mainCamera = Camera.main;
    }
    
    protected void Update()
    {
        transform.position += transform.up * (arrowSpeed * Time.deltaTime);
    }

    protected void LateUpdate()
    {
        Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);
        
        if (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1)
        {
            Destroy(gameObject);
        }
    }
}
