using System.Collections.Generic;
using System.Linq;
public class CardBuilder
{
    public List<Card> Build(List<Card> cardAtHand, List<Card> cardAtTable)
    {
        List<Card> cardToReturn = new List<Card>();
        List<Card> combinedCards = new List<Card>();

        List<Card> consecutiveCardList = new List<Card>();
        List<Card> sameSuitCardList = new List<Card>();
        List<Card> pairsCardList = new List<Card>();

        combinedCards.AddRange(cardAtHand);
        combinedCards.AddRange(cardAtTable);
        
        ChangeListToDescendingByValue(ref combinedCards);
        
        CheckForSameSuit(cardAtHand, cardAtTable, ref sameSuitCardList);
        CheckForConsecutive(cardAtHand, combinedCards, ref consecutiveCardList);
        CheckForPairs(combinedCards, ref pairsCardList);

        if(sameSuitCardList.Count > 0)
        {
            return sameSuitCardList;
        }
        if(consecutiveCardList.Count > 0)
        {
            return consecutiveCardList;
        }
        if(pairsCardList.Count > 0)
        {
            return pairsCardList;
        }
        return cardToReturn;
    }
    private void CheckForConsecutive(List<Card> originalHand, List<Card> cards, 
        ref List<Card> consecutiveCards)
    {
        List<Card> temporaryHolder = new List<Card>();
        int targetCount = 5;
        for(int i = 0; i < 3; i++)
        {
            for(int j = i; j < targetCount; j++)
            {
                if(j != targetCount - 1)
                {
                    int nextPredictedValue = cards[j + 1].Value;
                    if((cards[j].Value - 1) == nextPredictedValue)
                    {
                        if(!temporaryHolder.Contains(cards[j]))
                        {
                            temporaryHolder.Add(cards[j]);
                        }
                        if(!temporaryHolder.Contains(cards[j + 1]))
                        {
                            temporaryHolder.Add(cards[j + 1]);
                        }
                    }
                    else
                    {
                        temporaryHolder.Clear();
                        break;
                    }
                }
                else if(j == targetCount - 1)
                {
                    if(temporaryHolder.Contains(originalHand[0]) ||
                        temporaryHolder.Contains(originalHand[1]))
                    {
                        break;
                    }
                    else
                    {
                        temporaryHolder.Clear();
                        break;
                    }
                }
            }
            if(temporaryHolder.Count == 0)
            {
                targetCount += 1;
            }
            else
            {
                break;
            }
        }
        consecutiveCards = new List<Card>(temporaryHolder);
    }
    private void CheckForSameSuit(List<Card> cardAtHand, List<Card> cardsOnTable, 
        ref List<Card> sameSuitCards)
    {
        List<Card> temporaryCardHolder = new List<Card>();
        List<Card> hands = new List<Card>(cardAtHand);
        List<Card> table = new List<Card>(cardsOnTable);
        
        ChangeListToDescendingByValue(ref hands);
        ChangeListToDescendingByValue(ref table);

        for(int i = 0; i < hands.Count; i++)
        {
            temporaryCardHolder.Add(hands[i]);
            for(int j = 0; j < table.Count; j++)
            {
                if(temporaryCardHolder.Count != 5)
                {
                    if(hands[i].Suit == table[j].Suit)
                    {
                        temporaryCardHolder.Add(table[j]);
                    }
                }
                else if(temporaryCardHolder.Count == 5)
                {
                    break;
                }
            }
            if(temporaryCardHolder.Count == 5)
            {
                break;
            }
            else if(temporaryCardHolder.Count < 5)
            {
                temporaryCardHolder.Clear();
            }
        }
        if(temporaryCardHolder.Count > 0)
        {
            ChangeListToDescendingByValue(ref temporaryCardHolder);
        }
        sameSuitCards = new List<Card>(temporaryCardHolder);
    }
    private void CheckForPairs(List<Card> combinedCards, ref List<Card> pairsList)
    {
        List<Card> temporaryCombinedHolder = new List<Card>(combinedCards);
        List<Card> temporaryHolder = new List<Card>();
        foreach(var group in combinedCards.GroupBy(cards => cards.Value))
        {
            if(group.Count() >= 2)
            {
                temporaryHolder.AddRange(group.ToList());
            }
        }
        if(temporaryHolder.Count > 0)
        {
            for(int i = 0; i < temporaryHolder.Count; i++)
            {
                if(pairsList.Count < 5)
                {
                    temporaryCombinedHolder.Remove(temporaryHolder[i]);
                    pairsList.Add(temporaryHolder[i]);
                }
                if(pairsList.Count >= 5)
                {
                    break;
                }
            }
            for(int i = 0; i < temporaryCombinedHolder.Count; i++)
            {
                if(pairsList.Count < 5)
                {
                    pairsList.Add(temporaryCombinedHolder[i]);
                }
            }
            ChangeListToDescendingByValue(ref pairsList);
            ChangeListToDescendingByPairs(ref pairsList);
        }
    }
    private void ChangeListToDescendingByValue(ref List<Card> cardsList)
    {
        cardsList.Sort((cardsA, cardsB) => cardsA.Value.CompareTo(cardsB.Value));
        cardsList.Reverse();
    }
    private void ChangeListToDescendingByPairs(ref List<Card> cardsList)
    {
        List<Card> temporaryPairs = new List<Card>(cardsList);
        var sortedGroup = temporaryPairs.GroupBy(card => card.Value)
            .OrderByDescending(card => card.Count());
        cardsList.Clear();
        foreach(var group in sortedGroup)
        {
            List<Card> currentCardGroup = group.ToList();
            cardsList.AddRange(currentCardGroup);
        }
    }
}