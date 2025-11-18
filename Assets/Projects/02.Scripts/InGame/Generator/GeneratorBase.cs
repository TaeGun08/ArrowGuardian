using UnityEngine;

public abstract class GeneratorBase<T> : MonoBehaviour where T : MonoBehaviour
{
    protected abstract ObjectPool<T> GetNewPoolPrefab(int arrowIndex, int initialCount);

    public abstract Arrow CreateAndGetPool(int arrowIndex);

    public abstract void ReturnArrow(Arrow arrow);
}
