using System.Collections.Generic;
using System.Linq;
public class Flush : Rule
{
    public override Hand GetRuleHand()
    {
        return Hand.FLUSH;
    }
    public override bool ContainsHand(List<Card> cardList)
    {
        if(cardList.GroupBy(card => card.Suit).Count() == 1)
        {
            return true;
        }
        return false;
    }
}