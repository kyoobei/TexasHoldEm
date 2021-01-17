using System.Collections.Generic;
using UnityEngine;

public class CardDeal : MonoBehaviour
{
    [SerializeField] protected Pooler m_cardPooler = null;
    [SerializeField] protected Transform m_originPosition = null;
    [SerializeField] protected int m_numberOfCardsToDeal = 0;
    [SerializeField] protected float m_paddingBetweenCards = 0.0f;
    protected List<Card> m_currentCardsList = new List<Card>();
    public List<Card> CurrentCards => m_currentCardsList;
    public int NumberOfCardsToDeal => m_numberOfCardsToDeal;
    public virtual void SetCardValue(List<Card> cards)
    {
        m_currentCardsList.Clear();
        m_currentCardsList = new List<Card>(cards);
        UpdatePooledCards();
    }
    protected virtual void UpdatePooledCards()
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
