using System;
using UnityEngine;

public class GameUIController : MonoBehaviour, IController
{
    public Action OnPressedStartGame;
    public void StartController()
    {
        OnPressedStartGame?.Invoke();
    }
}
