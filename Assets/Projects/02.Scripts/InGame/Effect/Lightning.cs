using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Lightning : EffectBase
{
    public override int Id => 2;

    private LineRenderer lineRenderer;
    private int segmentCount = 8;
    private float offset = 0.3f;

    private Vector3 startPos;
    private Vector3 endPos;

    private float timer;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Setup(Vector3 start, Vector3 end)
    {
        startPos = start;
        endPos = end;
    }

    public override void Activate()
    {
        gameObject.SetActive(true);

        lineRenderer.positionCount = segmentCount + 2;

        for (int i = 0; i <= segmentCount + 1; i++)
        {
            float t = (float)i / (segmentCount + 1);

            Vector3 pos = Vector3.Lerp(startPos, endPos, t);

            if (i != 0 && i != segmentCount + 1)
            {
                Vector2 random = Random.insideUnitCircle * offset;
                pos.x += random.x;
                pos.y += random.y;
            }

            lineRenderer.SetPosition(i, pos);
        }
    }

    private void LateUpdate()
    {
        timer += Time.deltaTime;
        if (timer <= 0.2f) return;
        Deactivate();
    }

    public override void Deactivate()
    {
        effectGenerator.ReturnPool(Id, this);
    }
}