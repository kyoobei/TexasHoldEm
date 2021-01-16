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
            Debug.Log("goes in same suit");
            return sameSuitCardList;
        }
        if(consecutiveCardList.Count > 0)
        {
            Debug.Log("goes in consecutive");
            return consecutiveCardList;
        }
        if(pairsCardList.Count > 0)
        {
            Debug.Log("goes in pairs");
            return pairsCardList;
        }
        Debug.Log("goes in default");
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
        List<Card> temporaryHolder = new List<Card>(combinedCards);
        // List<Card> smallPairList = new List<Card>();
        List<Card> bigPairList = new List<Card>();
        foreach(var group in temporaryHolder.GroupBy(cards => cards.Value))
        {
            // if(group.Count() == 2)
            // {
            //     if(smallPairList.Count < 4)
            //     {
            //         smallPairList.AddRange(group.ToList());
            //     }
            // }
            //  else if(group.Count() > 2)
            if(group.Count() >= 3)
            {
                if(bigPairList.Count == 0)
                {
                    bigPairList.AddRange(group.ToList());
                }
            }
        }
        Debug.Log("pair list count: " + pairsList.Count);
        Debug.Log("big pair count: " + bigPairList.Count);
        if(bigPairList.Count > 0)
        {
            for(int i = 0; i < bigPairList.Count - 1; i++)
            {
                temporaryHolder.Remove(bigPairList[i]);
                
            }
            pairsList.AddRange(bigPairList);
            Debug.Log("temp count: " + temporaryHolder.Count);

            // if(!bigPairList.Contains(originalHand[0]) &&
            //     !bigPairList.Contains(originalHand[1]))
            // {
                
            // }
            // else
            // {
            //     Debug.Log("has atleast a pair");
            // }
        }
        Debug.Log("pair list count: " + pairsList.Count);

        // ChangeListToDescendingByValue(ref temporaryHolder);
        // ChangeListToDescendingByPairs(ref temporaryHolder);

        // for(int i = pairsList.Count - 1; i < 5; i++)
        // {
        //     pairsList.Add(temporaryHolder[i]);
        // }

        // ChangeListToDescendingByPairs(ref pairsList);

        // if(!pairsList.Contains(originalHand[0]) &&
        //     !pairsList.Contains(originalHand[1]))
        // {
        //     pairsList.RemoveAt(pairsList.Count - 1);
        //     pairsList.Add(originalHand[0]);
        // }

        // //there is a big pair 
        // if(bigPairList.Count > 0)
        // {
        //     //remove current list to temporary list
        //     for(int i = 0; i < bigPairList.Count; i++)
        //     {
        //         temporaryHolder.Remove(bigPairList[0]);
        //     }
        //     pairsList.AddRange(bigPairList);
        //     if(!bigPairList.Contains(originalHand[0]) 
        //         && !bigPairList.Contains(originalHand[1]))
        //     {
        //         Debug.Log("doesnt original hands");
        //         if(originalHand[0] == originalHand[1])
        //         {
        //             for(int i = pairsList.Count - 1; i < 5; i++)
        //             {
        //                 temporaryHolder.Remove(originalHand[i]);
        //                 pairsList.Add(originalHand[i]);
        //             }
        //         }
        //         else
        //         {
        //             temporaryHolder.Remove(originalHand[0]);
        //             pairsList.Add(originalHand[0]);
        //         }
        //     }
           
        // }
        // else if(bigPairList.Count == 0)
        // {
        //     //remove current list to temporary list
        //     for(int i = 0; i < smallPairList.Count; i++)
        //     {
        //         temporaryHolder.Remove(smallPairList[i]);
        //     }
        //     pairsList.AddRange(smallPairList);
        //     if(!smallPairList.Contains(originalHand[0]) 
        //         && !smallPairList.Contains(originalHand[1]))
        //     {
        //         if(originalHand[0] == originalHand[1])
        //         {
        //             for(int i = pairsList.Count - 1; i < 5; i++)
        //             {
        //                 temporaryHolder.Remove(originalHand[i]);
        //                 pairsList.Add(originalHand[i]);
        //             }
        //         }
        //         else
        //         {
        //             temporaryHolder.Remove(originalHand[0]);
        //             pairsList.Add(originalHand[0]);
        //         }
        //     }
        // }
        // //filler
        // for(int i = pairsList.Count - 1; i < 5; i++)
        // {
        //     pairsList.Add(temporaryHolder[0]);
        //     temporaryHolder.Remove(temporaryHolder[0]);
        //     continue;
        // }
        // ChangeListToDescendingByValue(ref pairsList);
        // ChangeListToDescendingByPairs(ref pairsList);
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