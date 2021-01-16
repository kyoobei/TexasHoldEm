using System.Collections.Generic;
using System.Linq;
public class ThreeOfAKind : Rule
{
    public override Hand GetRuleHand()
    {
        return Hand.THREE_OF_A_KIND;
    }
    public override bool ContainsHand(List<Card> cardList)
    {
        foreach(var groupElement in cardList.GroupBy(cards => cards.Value))
        {
            if(groupElement.Count() == 3)
            {
                return true;
            }
        }
        return false;
    }
}