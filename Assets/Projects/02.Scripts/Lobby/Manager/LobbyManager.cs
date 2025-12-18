using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : SingletonBase<LobbyManager>
{
    [Header("Lobby Settings")]
    [SerializeField] private ButtonEventBase[] buttons;
}
