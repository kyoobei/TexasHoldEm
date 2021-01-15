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
    
    public /*Hand*/void GetHandType(List<Card> originalHand, List<Card> otherHand)
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

        HasPairs();
        // //var pairs = from cards in m_combineCards where cards.Value
        
        //  var cardsByValue = m_combineCards.GroupBy(cards => cards.Value);
        //  List<Card> temporaryCardGroups = new List<Card>();
        // foreach(var groups in cardsByValue)
        // {
        //     if(groups.Count() == 2)
        //     {
                
        //     }
        //     if(groups.Count() == 3)
        //     {

        //     }   
        //     if(groups.Count() == 4)
        //     {

        //     } 
        // }

        // // if(winningCardList.Count > 0)
        // // {
        // //     for(int i = 0; i < winningCardList.Count; i++)
        // //     {
        // //         Debug.Log("this is the winning: " + winningCardList[i].Suit + " " + winningCardList[i].Value);
        // //     }
        // // }
        // // else
        // // {
        // //     Debug.Log("no winning list");
        // // }
        
        
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

    private void HasPairs()
    {
        var groupedCards = m_combineCards.GroupBy(cards => cards.Value);
        List<Card> temporaryCardPairs = new List<Card>();
        foreach(var group in groupedCards)
        {
            //check if a specific group of value has more than one
            if(group.Count() > 1)
            {
                List<Card> currentCardGroup = group.ToList();
                temporaryCardPairs.AddRange(currentCardGroup);
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
        }
        else
        {
            //pairs exceed the number of max cards for winning hands
            if(temporaryCardPairs.Count() > 5)
            {
                //maintain count to 4
                for(int i = temporaryCardPairs.Count - 1; i >= 4; i--)
                {
                    temporaryCardPairs.RemoveAt(i);
                }  
            }
            //if current pairs doesnt compare even one card from the hole cards
            if(!temporaryCardPairs.Contains(m_originalCards[0]))
            {
                Debug.Log("doesnt contain");
                temporaryCardPairs.Add(m_originalCards[0]);
            }
            else if(temporaryCardPairs.Contains(m_originalCards[0]))
            {
                //since combine cards are sorted descending then you can get the next highest card
                for(int i = 0; i < m_combineCards.Count(); i++)
                {
                    if(temporaryCardPairs.Count() < 5)
                    {
                        if(!temporaryCardPairs.Contains(m_combineCards[i]))
                        {
                            temporaryCardPairs.Add(m_combineCards[i]);
                        }
                    }
                    else if(temporaryCardPairs.Count() >= 5)
                    {
                        break;
                    }
                }
            }
        }
        winningCardList = new List<Card>(temporaryCardPairs);
    }
    // private bool IsPair()
    // {
    //     List<Card> temporaryPair = new List<Card>();
    //     for(int i = 0; i < m_combineCards.Count; i++)
    //     {

    //     }
    //     return false;
    // }
    // private bool IsTwoPair()
    // {
    //     return false;
    // }
    // private bool IsThreeOfKind()
    // {
    //     return false;
    // }
    // private bool IsFullHouse()
    // {
    //     return false;
    // }
}