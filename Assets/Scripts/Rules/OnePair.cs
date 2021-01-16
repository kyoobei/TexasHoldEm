using System.Collections.Generic;
using System.Linq;
public class OnePair : Rule
{
    public override Hand GetRuleHand()
    {
        return Hand.ONE_PAIR;
    }
    public override bool ContainsHand(List<Card> cardList)
    {
        foreach(var groupElement in cardList.GroupBy(cards => cards.Value))
        {
            if(groupElement.Count() == 2)
            {
                return true;
            }
        }
        return false;
    }
}