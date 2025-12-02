using System;
using DG.Tweening;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public static Wall Instance;
    
    [Header("Wall Settings")] 
    [SerializeField] private SpriteRenderer[] wallSpriteRenderers;
    private bool isHitting;
    
    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }
    

    public void HitWall()
    {
        if (isHitting)
            return;

        isHitting = true;

        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < wallSpriteRenderers.Length; i++)
        {
            var sr = wallSpriteRenderers[i];
            if (sr == null) continue;

            Color originalColor = sr.color;
            Color hitColor = new Color(1f, 0.3f, 0.3f, originalColor.a);

            seq.Join(
                sr.DOColor(hitColor, 0.1f)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(Ease.OutQuad)
            );
        }

        seq.OnComplete(() =>
        {
            isHitting = false;
        });
    }
}