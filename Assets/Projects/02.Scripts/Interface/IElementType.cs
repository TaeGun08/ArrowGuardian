using UnityEngine;

public enum ElementType
{
    None,
    Flame,
    Water,
    Wind,
    Earth,
    Lightning,
    Dark,
    Light,
}

public interface IElementType
{
    public ElementType ElementType { get; }
}
