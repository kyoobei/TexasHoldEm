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
        combinedCards.AddRange(cardAtHand);
        combinedCards.AddRange(cardAtTable);
        if(HasSameValueInList(cardAtHand[0], cardAtTable) || 
            HasSameValueInList(cardAtHand[1], cardAtTable))
        {
            List<Card> pairHolder = new List<Card>();
            var pairInGroups = combinedCards.GroupBy(group => group.Value);
            foreach(var pair in pairInGroups)
            {
                pairHolder.AddRange(pair.ToList());
            }
            ChangeListToDescendingByValue(ref pairHolder);
            //check if within the new pairs is the card at hand
            
        }
        else
        {
            Debug.Log("Has no pair in the table");
        }
        // bool cardOneAtTable = false;
        // cardAtTable.ForEach(card =>{
        //     if(card.Value == cardAtHand[0].Value)
        //         cardOneAtTable = true;
        // });
        // Debug.Log("at table[0]: " + cardOneAtTable + 
        // " at table[1]: " + cardAtTable.Contains(cardAtHand[1]));
        // List<Card> combinedCard = new List<Card>();
        // combinedCard.AddRange(cardAtHand);
        // combinedCard.AddRange(cardAtTable);
        
        // cardBuildHolder.AddRange(cardAtHand);
        // cardBuildHolder.AddRange(cardAtTable);
        return cardToReturn;
    }
    private bool HasSameValueInList(Card cardToCheck, List<Card> cardsList)
    {
        for(int i = 0; i < cardsList.Count - 1; i++)
        {
            if(cardsList[i].Value == cardToCheck.Value)
            {
                return true;
            }
        }
        return false;
    }
    private void ChangeListToDescendingByValue(ref List<Card> cardsList)
    {
        cardsList.Sort((cardsA, cardsB) => cardsA.Value.CompareTo(cardsB));
        cardsList.Reverse();
    }
}