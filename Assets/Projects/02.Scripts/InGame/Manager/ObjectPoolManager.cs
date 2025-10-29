using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPoolManager : SingletonBase<ObjectPoolManager>
{
    
    private Dictionary<object, object> pools = new Dictionary<object, object>();
}
