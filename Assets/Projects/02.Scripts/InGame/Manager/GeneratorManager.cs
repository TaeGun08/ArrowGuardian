public class GeneratorManager : SingletonBase<GeneratorManager>
{
    public ArrowGenerator ArrowGenerator { get; private set; }
    public EnemyGenerator EnemyGenerator { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        ArrowGenerator = ComponentExtensions.FindOrAddComponent<ArrowGenerator>();
        EnemyGenerator = ComponentExtensions.FindOrAddComponent<EnemyGenerator>();
    }
}
