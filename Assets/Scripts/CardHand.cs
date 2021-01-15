using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CardHand
{    
    public List<Card> m_combineCards; // <-- public for now need to be private later
    public List<Card> winningCardList = new List<Card>(); // <-- will be used for comparison to other cards.. public for now
    private List<Card> m_originalCards; // <-- original hole cards or cards in the hands of players
    private List<Card> m_otherCards; // <-- community cards or cards in the middle table
    private int numberOfPairs = 0;
    private int numberOfTri = 0;

    public Hand GetHandType(List<Card> originalHand, List<Card> otherHand)
    {
        m_combineCards = new List<Card>();
        m_originalCards = new List<Card>(originalHand);
        m_originalCards.Sort((handA, handB) => handA.Value.CompareTo(handB.Value));
        m_originalCards.Reverse();
        m_otherCards = new List<Card>(otherHand);
        originalHand.ForEach(hand => m_combineCards.Add(hand));
        otherHand.ForEach(hand => m_combineCards.Add(hand));
        m_combineCards.Sort((handA, handB) => handA.Value.CompareTo(handB.Value));
        m_combineCards.Reverse();

        List<Card> cardPairsByValue = new List<Card>();
        if(HasPairs(ref cardPairsByValue))
        {
            if(HasFullHouse(ref cardPairsByValue))
            {
                winningCardList = new List<Card>(cardPairsByValue);
                return Hand.FULL_HOUSE;
            }
            if(HasOnePair(ref cardPairsByValue))
            {
                winningCardList = new List<Card>(cardPairsByValue);
                return Hand.ONE_PAIR;
            }
            if(HasTwoPair(ref cardPairsByValue))
            {
                winningCardList = new List<Card>(cardPairsByValue);
                return Hand.TWO_PAIR;
            }
            if(HasThreeOfAKind(ref cardPairsByValue))
            {
                winningCardList = new List<Card>(cardPairsByValue);
                return Hand.THREE_OF_A_KIND;
            }
            if(HasFourOfAKind(ref cardPairsByValue))
            {
                winningCardList = new List<Card>(cardPairsByValue);
                return Hand.FOUR_OF_A_KIND;
            }
        }
        // foreach(var group in groupedCardsByValue)
        // {
        //     //check if a specific group of value has more than one
        //     if(group.Count() >= 2)
        //     {
        //         List<Card> currentCardGroup = group.ToList();
        //         cardPairsByValue.AddRange(currentCardGroup);
        //     }
        // }
        //pairs exceed the number of max cards for winning hands
        // if(cardPairsByValue.Count() > 5)
        // {
        //     //maintain count to 4
        //     for(int i = cardPairsByValue.Count - 1; i >= 4; i--)
        //     {
        //         cardPairsByValue.RemoveAt(i);
        //     }  
        // }
        // //there are pairs in value
        // if(cardPairsByValue.Count() > 0)
        // {
            
        // }


        #region WORKING STRAIGHT, FLUSH, ROYAL FLUSH IN ASCENDING
        // if(IsStraight())
        // {
        //     if(IsFlush())
        //     {
        //         if(winningCardList[winningCardList.Count - 1].Value == (int)HighCard.Ace)
        //         {
        //             return Hand.ROYAL_FLUSH;
        //         }
        //         return Hand.FLUSH;
        //     }
        //     return Hand.STRAIGHT;
        // }
        // if(IsFlush())
        // {
        //     return Hand.FLUSH;
        // }

        //return Hand.HIGH_CARD;
        #endregion

        //HasPairs();
        return Hand.HIGH_CARD;
    }

    #region WORKING FORMULA FOR STRAIGHT AND FLUSH (ASCENDING ORDER)
    // private bool IsStraight()
    // {
    //     //TODO: if there will be same number but different suit there will be a problem or bug
    //     List<Card> straightCard = new List<Card>();
    //     for(int i = 0; i < m_combineCards.Count; i++)
    //     {
    //         if(i != m_combineCards.Count - 1)
    //         {
    //             if((m_combineCards[i].Value + 1) == m_combineCards[i + 1].Value)
    //             {
    //                 if(straightCard.Count < 5)
    //                 {
    //                     if(!straightCard.Contains(m_combineCards[i]))
    //                     {
    //                         straightCard.Add(m_combineCards[i]);
    //                     }
    //                     if(!straightCard.Contains(m_combineCards[i + 1]))
    //                     {
    //                         straightCard.Add(m_combineCards[i + 1]); 
    //                     }                 
    //                 }
    //             }
    //             else if((m_combineCards[i].Value + 1) != m_combineCards[i + 1].Value)
    //             {
    //                 if(straightCard.Count < 5)
    //                 {
    //                     straightCard.Clear();
    //                 }
    //             }
    //         }
    //     }
    //     if(straightCard.Count >= 5)
    //     {
    //         if(straightCard.Contains(m_originalCards[0]) || 
    //             straightCard.Contains(m_originalCards[1]))
    //         {
    //             winningCardList = new List<Card>(straightCard);
    //             return true;
    //         }
    //     }
    //     return false;
    // }
    // private bool IsFlush()
    // {
    //     if(winningCardList.Count > 0)
    //     {
    //         return winningCardList.GroupBy(card => card.Suit).Count() == 1;
    //     }
    //     else if(winningCardList.Count <= 0)
    //     {
    //         foreach(var groups in m_combineCards.GroupBy(card => card.Suit))
    //         {
    //             if(groups.Count() == 5 && (groups.Contains(m_originalCards[0]) ||
    //                 groups.Contains(m_originalCards[1])))
    //             {
    //                 winningCardList = new List<Card>(groups);
    //                 return true;
    //             }
    //         }
    //     }
    //     return false;
    // }
    // private void CheckForPairs()
    // {
    //     List<Card> temporaryPairsHolder = new List<Card>();
    //     int tempNumber = 0; 
    //     for(int i = 0; i < m_combineCards.Count; i++)
    //     {
    //         for(int j = 0; j < m_combineCards.Count; j++)
    //         {
    //             if((m_combineCards[j].Suit != m_combineCards[i].Suit) &&
    //                 (m_combineCards[j].Value == m_combineCards[i].Value))
    //             {
    //                 tempNumber++;
                    
    //             }
    //         }
    //         tempNumber = 0;
    //     }
    // }
    #endregion

    private void HasPairs2()
    {
        var groupedCards = m_combineCards.GroupBy(cards => cards.Value);
        List<Card> temporaryCardPairs = new List<Card>();
        foreach(var group in groupedCards)
        {
            //check if a specific group of value has more than one
            if(group.Count() >= 2)
            {
                List<Card> currentCardGroup = group.ToList();
                temporaryCardPairs.AddRange(currentCardGroup);
            }
        }
        //pairs exceed the number of max cards for winning hands
        if(temporaryCardPairs.Count() > 5)
        {
            //maintain count to 4
            for(int i = temporaryCardPairs.Count - 1; i >= 4; i--)
            {
                temporaryCardPairs.RemoveAt(i);
            }  
        }
        //sort full house by number of pairs... trio and duo
        if(temporaryCardPairs.Count() == 5)
        {
            List<Card> temporaryFullHouse = new List<Card>(temporaryCardPairs);
            var sortedGroup = temporaryFullHouse.GroupBy(card => card.Value)
                .OrderByDescending(card => card.Count());
            temporaryCardPairs.Clear();
            foreach(var group in sortedGroup)
            {
                List<Card> currentCardGroup = group.ToList();
                temporaryCardPairs.AddRange(currentCardGroup);
            }
            //currentHand = Hand.FULL_HOUSE;
        }
        else if(temporaryCardPairs.Count() >= 2)
        {
            List<Card> fillerCard = new List<Card>();
            int numberOfMissingCards = 5 - temporaryCardPairs.Count();
            //if current pairs doesnt compare even one card from the hole cards
            if(!temporaryCardPairs.Contains(m_originalCards[0]))
            {
                //temporaryCardPairs.Add(m_originalCards[0]);
                fillerCard.Add(m_originalCards[0]);
            }
            //since combine cards are sorted descending then you can get the next highest card
            for(int i = 0; i < m_combineCards.Count(); i++)
            {
                if(fillerCard.Count() < numberOfMissingCards)
                {
                    if(!temporaryCardPairs.Contains(m_combineCards[i]))
                    {
                        fillerCard.Add(m_combineCards[i]);
                    }
                }
                else if(fillerCard.Count() >= numberOfMissingCards)
                {
                    break;
                }
            }
            fillerCard.Sort((handA, handB) => handA.Value.CompareTo(handB.Value));
            fillerCard.Reverse();
            temporaryCardPairs.AddRange(fillerCard);
        }
        winningCardList = new List<Card>(temporaryCardPairs);
    }
    private bool HasPairs(ref List<Card> cardPairs)
    {
        var groupedCardsByValue = m_combineCards.GroupBy(cards => cards.Value);
        foreach(var group in groupedCardsByValue)
        {
            //check if a specific group of value has more than one
            if(group.Count() >= 2)
            {
                List<Card> currentCardGroup = group.ToList();
                cardPairs.AddRange(currentCardGroup);
            }
        }
        //remove pairs that are more than 5
        if(cardPairs.Count() > 5)
        {
            RemoveCards(ref cardPairs, 4);
        }
        if(cardPairs.Count() <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool HasOnePair(ref List<Card> currentCardPairs)
    {
        if(currentCardPairs.Count == 2)
        {
            FillRemainingCardArea(ref currentCardPairs);
            return true;
        }
        return false;
    }
    private bool HasTwoPair(ref List<Card> currentCardPairs)
    {
        if(currentCardPairs.Count == 4)
        {
            FillRemainingCardArea(ref currentCardPairs);
            return true;
        }
        return false;
    }
    private bool HasThreeOfAKind(ref List<Card> currentCardPairs)
    {
        if(currentCardPairs.Count == 3)
        {
            FillRemainingCardArea(ref currentCardPairs);
            return true;
        }
        return false;
    }
    private bool HasFourOfAKind(ref List<Card> currentCardPairs)
    {
        if(currentCardPairs.Count == 4)
        {
            FillRemainingCardArea(ref currentCardPairs);
            return true;
        }
        return false;
    }
    private bool HasFullHouse(ref List<Card> currentCardPairs)
    {
        if(currentCardPairs.Count == 5)
        {
            if(currentCardPairs.Contains(m_originalCards[0]) 
                || currentCardPairs.Contains(m_originalCards[1]))
            {
                List<Card> temporaryFullHouse = new List<Card>(currentCardPairs);
                var sortedGroup = temporaryFullHouse.GroupBy(card => card.Value)
                    .OrderByDescending(card => card.Count());
                currentCardPairs.Clear();
                foreach(var group in sortedGroup)
                {
                    List<Card> currentCardGroup = group.ToList();
                    currentCardPairs.AddRange(currentCardGroup);
                }
                return true;
            }
            else
            {
                RemoveCards(ref currentCardPairs, 3);
                return false;
            }
        }
        return false;
    }
    private void RemoveCards(ref List<Card> currentCards, int removeOffSet)
    {
        List<Card> temporaryHolder = new List<Card>(currentCards);
        for(int i = temporaryHolder.Count - 1; i >= removeOffSet; i--)
        {
            currentCards.RemoveAt(i);
        }
    }
    private void FillRemainingCardArea(ref List<Card> currentCardPairs)
    {
        List<Card> filler = new List<Card>();
        //add the biggest number on holder hands
        if(!currentCardPairs.Contains(m_originalCards[0]))
        {

        }
        //return filler;
    }
}