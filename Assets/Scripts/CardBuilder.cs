using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
        CheckForPairs(cardAtHand, combinedCards, ref pairsCardList);

        if(sameSuitCardList.Count > 0)
        {
            return sameSuitCardList;
        }
        if(consecutiveCardList.Count > 0)
        {
            return consecutiveCardList;
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
    private void CheckForPairs(List<Card> originalHand ,List<Card> combinedCards,
        ref List<Card> pairsList)
    {
        List<Card> temporaryPairsList = new List<Card>();
        // var groupedCards = combinedCards.GroupBy(cards => cards.Value);
        // foreach(var group in groupedCards)
        // {
        //     if(group.Count() >= 2)
        //     {
        //         temporaryPairsList.AddRange(group.ToList());
        //     }
        // }
        if(originalHand[0].Value == originalHand[1].Value)
        {
            temporaryPairsList.AddRange(originalHand);
        }
        else
        {
            //look for pairs with original hands
            var groupedCards = combinedCards.GroupBy(cards => cards.Value);
            foreach(var groupElements in groupedCards)
            {

            }
        }
        // var groupedCardsByValue = cardList.GroupBy(cards => cards.Value);
        // foreach(var group in groupedCardsByValue)
        // {
        //     //check if a specific group of value has more than one
        //     if(group.Count() >= 2)
        //     {
        //         return true;
        //     }
        // }
        // return false;
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