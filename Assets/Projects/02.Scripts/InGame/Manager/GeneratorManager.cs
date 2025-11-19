public class GeneratorManager : SingletonBase<GeneratorManager>
{
    public ArrowGenerator ArrowGenerator { get; private set; }
    public EnemyGenerator EnemyGenerator { get; private set; }
    public UIGenerator UIGenerator { get; private set; }
    public SkillGenerator SkillGenerator { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        ArrowGenerator = ComponentExtensions.FindOrAddComponent<ArrowGenerator>();
        EnemyGenerator = ComponentExtensions.FindOrAddComponent<EnemyGenerator>();
        UIGenerator =  ComponentExtensions.FindOrAddComponent<UIGenerator>();
        SkillGenerator =  ComponentExtensions.FindOrAddComponent<SkillGenerator>();
    }
}
