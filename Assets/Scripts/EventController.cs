using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField] GameUIController m_gameUIController = null;
    [SerializeField] CardController m_cardController = null;
    
    #region UNITY METHODS
    private void OnEnable()
    {
        m_gameUIController.OnPressedStartGame += m_cardController.StartController;
    }
    private void OnDisable()
    {
        m_gameUIController.OnPressedStartGame -= m_cardController.StartController;
    }
    #endregion
}
