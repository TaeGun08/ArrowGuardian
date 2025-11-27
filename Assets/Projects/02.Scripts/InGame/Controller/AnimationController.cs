using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    
    private Animator animator;
    
    public enum AnimState
    {
        Idle,
        Walking,
    }
    
    [SerializeField] private AnimState state;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        ChangeAnimation(state);
    }

    public void ChangeAnimation(AnimState state)
    {
        switch (state)
        {
            case AnimState.Idle:
                animator.SetTrigger(Idle);
                break;
            case AnimState.Walking:
                animator.SetTrigger(Walk);
                break;
        }
    }
}
