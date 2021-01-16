using System.Collections.Generic;
public class StraightFlush : Rule
{
    Straight straight = new Straight();
    Flush flush = new Flush();
    public override Hand GetRuleHand()
    {
        return Hand.STRAIGHT_FLUSH;
    }
    public override bool ContainsHand(List<Card> cardList)
    {
        if(straight.ContainsHand(cardList) && flush.ContainsHand(cardList))
        {
            return true;
        }
        return false;
    }
}