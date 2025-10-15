using System;
using UnityEngine;

public abstract class Arrow : MonoBehaviour, IElementType
{
    [Header("Arrow Settings")]
    [SerializeField] protected ElementType elementType;
    public ElementType ElementType => elementType;

    protected void Awake()
    {
    }
}
