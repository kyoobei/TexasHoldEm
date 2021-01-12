using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CardPooler : MonoBehaviour
{
    private const int TOTAL_SUITS = 4;
    private const int TOTAL_CARDS_IN_DECK = 13;
    [SerializeField] private Queue<Card> shuffleCardQueue = new Queue<Card>();
    [SerializeField] private List<Card> shuffleCardClone = new List<Card>();
    [SerializeField] private List<Card> cardList = new List<Card>();
    private void Start()
    {
        for(int i = 0; i < TOTAL_SUITS; i++)
        {
            for(int j = 0; j < TOTAL_CARDS_IN_DECK; j++)
            {
                cardList.Add(new Card((Suit)i, j+1));
            }
        }
    }
    [ContextMenu("Test Shuffle")]
    public void Shuffle()
    {
        shuffleCardClone.Clear();
        while(cardList.Count > 0)
        {
            int randIndex = Random.Range(0, cardList.Count);
            shuffleCardQueue.Enqueue(cardList[randIndex]);
            cardList.RemoveAt(randIndex);
        }
        shuffleCardClone = shuffleCardQueue.ToList();
    }
    public Card GetCard(int index)
    {
        return cardList[index];
    }
}
