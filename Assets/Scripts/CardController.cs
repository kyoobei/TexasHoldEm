using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour, IController
{
    // private const int NUM_PLAYER_CARDS = 2;
    // private const int NUM_MIDDLE_CARDS = 5;
    // [SerializeField] private Pooler m_cardPooler = null;
    // [SerializeField] List<Transform> m_playerPositions = new List<Transform>();
    // [SerializeField] Transform m_middlePosition = null;
    private Deck m_deck = new Deck();
    private void Start()
    {
        m_deck.OnFinishedShuffling += FinishedShuffling;
    }
    private void OnDisable()
    {
        m_deck.OnFinishedShuffling -= FinishedShuffling;
    }
    public void StartController()
    {
        m_deck.Shuffle();
    }
    private void FinishedShuffling()
    {
        Debug.Log("finished shuffling");
    }
}
