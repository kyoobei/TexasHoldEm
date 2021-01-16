using System.Collections.Generic;
using System.Linq;
public class FourOfAKind : Rule
{
    public override Hand GetRuleHand()
    {
        return Hand.FOUR_OF_A_KIND;
    }
    public override bool ContainsHand(List<Card> cardList)
    {
        foreach(var groupElement in cardList.GroupBy(cards => cards.Value))
        {
            if(groupElement.Count() == 4)
            {
                return true;
            }
        }
        return false;
    }
}