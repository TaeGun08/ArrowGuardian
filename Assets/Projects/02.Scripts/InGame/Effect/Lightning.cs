using UnityEngine;

public class Lightning : EffectBase
{
    public override int Id => 2;

    public IDamageAble Target { get; set; }
    
    private LineRenderer lineRenderer;
    private int chainCount = 8;
    private float offset = 0.3f;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
        
        transform.position = Target.Transform.position;
        
        lineRenderer.positionCount = chainCount + 2;

        for (int i = 0; i <= chainCount + 1; i++)
        {
            float t = (float)i / (chainCount + 1);

            Vector3 pos = Vector3.Lerp(transform.position, Target.Transform.position, t);

            pos.x += Random.Range(-offset, offset);
            pos.y += Random.Range(-offset, offset);

            lineRenderer.SetPosition(i, pos);
        }
    }

    public override void Deactivate()
    {
        Target = null;
        effectGenerator.ReturnPool(2, this);
    }
}
