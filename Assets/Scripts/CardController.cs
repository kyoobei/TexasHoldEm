using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour, IController
{
    private const int NUM_PLAYER_CARDS = 2;
    private const int NUM_MIDDLE_CARDS = 5;
    // [SerializeField] private Pooler m_cardPooler = null;
    // [SerializeField] List<Transform> m_playerPositions = new List<Transform>();
    // [SerializeField] Transform m_middlePosition = null;
    public void StartController()
    {
        Debug.Log("working as shit");
        //NOTE create own scripts for card handler (players and middle area)
    }
}
