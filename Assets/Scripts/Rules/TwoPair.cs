using System.Collections.Generic;
using System.Linq;
public class TwoPair : Rule
{
    public override Hand GetRuleHand()
    {
        return Hand.TWO_PAIR;
    }
    public override bool ContainsHand(List<Card> cardList)
    {
        List<Card> listOfOnePair = new List<Card>();
        foreach(var groupElement in cardList.GroupBy(cards => cards.Value))
        {
            if(groupElement.Count() == 2)
            {
                listOfOnePair.AddRange(groupElement.ToList());
            }
        }
        if(listOfOnePair.Count == 4)
        {
            return true;
        }
        return false;
    }
}