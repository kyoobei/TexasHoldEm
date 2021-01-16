using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class CardBuilder
{
    /*
        IDEA
        1. Check for pairs
        Possible result:
        A. Check if pairs contain card at hand
            If do:
                - add to special card area
        B. Check if card at hand is a pair
            If do:
                - add to special card area
        2. Check for Same suits
            If same suit is equal to 5
                arrange from highest to lowest
                check if it contains card at hand
                check if consecutive
                check if it starts at high card
        3. Check for consecutive
            Check both hands
    *
    *
    */
    public List<Card> Build(List<Card> cardAtHand, List<Card> cardAtTable)
    {
        List<Card> cardToReturn = new List<Card>();
        List<Card> combinedCards = new List<Card>();

        List<Card> consecutiveCardList = new List<Card>();
        List<Card> sameSuitCardList = new List<Card>();

        combinedCards.AddRange(cardAtHand);
        combinedCards.AddRange(cardAtTable);
        
        ChangeListToDescendingByValue(ref combinedCards);
        
        CheckForSameSuit(cardAtHand, cardAtTable, ref sameSuitCardList);
        CheckForConsecutive(cardAtHand, combinedCards, ref consecutiveCardList);

        // if(consecutiveCardList.Count > 0)
        // {
        //     cardToReturn = new List<Card>(consecutiveCardList);
        // }
        if(sameSuitCardList.Count > 0)
        {
            Debug.Log("its working");
            cardToReturn = new List<Card>(sameSuitCardList);
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
    private void CheckForPairs(ref List<Card> pairsList)
    {
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