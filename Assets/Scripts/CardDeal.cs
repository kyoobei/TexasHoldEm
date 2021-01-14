using System.Collections.Generic;
using UnityEngine;

public class CardDeal : MonoBehaviour
{
    [SerializeField] private Pooler m_cardPooler = null;
    [SerializeField] private Transform m_originPosition = null;
    [SerializeField] private int m_numberOfCardsToDeal = 0;
    [SerializeField] private float m_paddingBetweenCards = 0.0f;
    [SerializeField] private List<Card> m_currentCardsList;
    public int NumberOfCardsToDeal => m_numberOfCardsToDeal;
    public void SetCardValue(List<Card> cards)
    {
        m_currentCardsList.Clear();
        m_currentCardsList = new List<Card>(cards);
        UpdatePooledCards();
    }
    private void UpdatePooledCards()
    {
        Vector3 startingPosition = m_originPosition.position;
        for(int i = 0; i < m_numberOfCardsToDeal; i++)
        {
            GameObject cardObject = m_cardPooler.GetClone();
            cardObject.SetActive(true);
            CardBehaviour behaviour = cardObject.GetComponent<CardBehaviour>();
            behaviour.ResetBehaviour();
            behaviour.UpdateDisplay(m_currentCardsList[i]);
            behaviour.MoveCard(startingPosition);
            //update position
            startingPosition = new Vector3
            (
                startingPosition.x + m_paddingBetweenCards,
                startingPosition.y,
                startingPosition.z
            );
        }
    }
}
