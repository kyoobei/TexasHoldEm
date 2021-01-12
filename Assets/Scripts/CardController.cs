using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour, IController
{
    [SerializeField] private Pooler m_cardPooler = null;
    public void StartController()
    {
        Debug.Log("working as shit");
    }
}
