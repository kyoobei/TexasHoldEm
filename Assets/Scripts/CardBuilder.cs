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
    private List<Card> originalHand = new List<Card>();
    public List<Card> Build(List<Card> cardAtHand, List<Card> cardAtTable)
    {
        List<Card> cardToReturn = new List<Card>();
        List<Card> combinedCards = new List<Card>();

        List<Card> consecutiveCardList = new List<Card>();
        List<Card> sameSuitCardList = new List<Card>();

        originalHand.Clear();
        originalHand = cardAtHand;

        combinedCards.AddRange(cardAtHand);
        combinedCards.AddRange(cardAtTable);
        
        ChangeListToDescendingByValue(ref combinedCards);
        
        CheckForSameSuit(cardAtHand, cardAtTable, ref sameSuitCardList);
        CheckForConsecutive(combinedCards, ref consecutiveCardList);

        // if(consecutiveCardList.Count > 0)
        // {
        //     cardToReturn = new List<Card>(consecutiveCardList);
        // }
        if(sameSuitCardList.Count > 0)
        {
            Debug.Log("its working");
            cardToReturn = new List<Card>(sameSuitCardList);
        }


        // if
        // {
        //     Debug.Log("there is consecutive parts here");
        // }
        // if(HasSameSuits(ref combinedCards))
        // {
        //     Debug.Log("same suits");
        // }

        // if(IsConsecutive(cardAtHand[0], combinedCards))
        // {
        //     Debug.Log("consecutive on 0 card");
        // }
        // else
        // {
        //     Debug.Log("not consecutive on 0 card");
        // }
        // if(IsConsecutive(cardAtHand[1], combinedCards))
        // {
        //     Debug.Log("consecutive on 1 card");
        // }
        // else
        // {
        //      Debug.Log("not consecutive on 1 card");
        // }

        // if(HasPairs(ref combinedCards))
        // {
        //     ChangeListToDescendingByPairs(ref combinedCards);
        // }
        // else if(HasSameSuits(ref combinedCards))
        // {
        //     ChangeListToDescendingBySuit(ref combinedCards);
        // }

        // ChangeListToDescendingByValue(ref combinedCards);

        // if(HasSameValueInList(cardAtHand[0], cardAtTable) || 
        //     HasSameValueInList(cardAtHand[1], cardAtTable))
        // {
        //     Debug.Log("there are pairs");
        //     ChangeListToDescendingByPairs(ref combinedCards);
        //     if(cardAtHand[0].Value == cardAtHand[1].Value)
        //     {
        //         //they are a pair
        //         Debug.Log("hands are pairs");
        //     }
        //     else
        //     {

        //     }
        //     // List<Card> pairHolder = new List<Card>();
        //     // var pairInGroups = combinedCards.GroupBy(group => group.Value);
        //     // foreach(var pair in pairInGroups)
        //     // {
        //     //     pairHolder.AddRange(pair.ToList());
        //     // }
        //     // ChangeListToDescendingByValue(ref pairHolder);
        //     //check if within the new pairs is the card at hand
            
        // }
        // else
        // {
        //     Debug.Log("Has no pair in the table");
        //     ChangeListToDescendingByValue(ref combinedCards);
        // }

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

        //for testing remove this later on
        //cardToReturn = new List<Card>(consecutiveCardList);

        return cardToReturn;
    }
    private void CheckForConsecutive(List<Card> cards, ref List<Card> consecutiveCards)
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
    // private bool IsConsecutive(Card cardToCheck, List<Card> cards)
    // {
    //     int currentIndex = cards.IndexOf(cardToCheck);
    //     Debug.Log("current index:  " + currentIndex +  " " + cardToCheck.Value);
    //     int counter = 0;
    //     if(currentIndex >= 5)
    //     {
    //         //check array downwards
    //         for(int i = currentIndex; i <= 0; i--)
    //         {
    //             if(currentIndex != 0)
    //             {
    //                 if((cards[i].Value + 1) == cards[i-1].Value)
    //                 {
    //                     counter++;
    //                 }
    //             }
    //         }
    //     }
    //     else if(currentIndex < 5)
    //     {
    //         Debug.Log("less than 5: " + cardToCheck.Value);
    //         //start counting from
    //         for(int i = 0; i < 5; i++)
    //         {
    //             if(i != currentIndex-1)
    //             {
    //                 if((cards[i].Value - 1) == cards[i + 1].Value)
    //                 {
    //                     counter++;
    //                 }
    //             }
    //         } 
    //     }
    //     Debug.Log("counter: " + counter);
    //     if(counter >= 5)
    //     {
    //         return true;
    //     }
    //     return false;
    // }
    private bool HasPairs(ref List<Card> cardList)
    {
        var groupedCardsByValue = cardList.GroupBy(cards => cards.Value);
        foreach(var group in groupedCardsByValue)
        {
            //check if a specific group of value has more than one
            if(group.Count() >= 2)
            {
                return true;
            }
        }
        return false;
    }
    private bool HasSameSuits(ref List<Card> cardList)
    {
        var groupedCardsByValue = cardList.GroupBy(cards => cards.Suit);
        foreach(var group in groupedCardsByValue)
        {
            // //check if a specific group of value has more than one
            // if(group.Count() >= 5 && (group.Contains(m_originalCards[0]) ||
            //     group.Contains(m_originalCards[1])))
            // {
            //     return true;
            // }
        }
        return false;
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
        cardsList.Sort((cardsA, cardsB) => cardsA.Value.CompareTo(cardsB.Value));
        cardsList.Reverse();
    }
    private void ChangeListToDescendingBySuit(ref List<Card> cardsList)
    {
        cardsList.Sort((cardsA, cardsB) => cardsA.Value.CompareTo(cardsB.Suit));
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