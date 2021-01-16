using System.Collections.Generic;

public class RoyalFlush : Rule
{
    StraightFlush straightFlush = new StraightFlush();
    public override Hand GetRuleHand()
    {
        return Hand.ROYAL_FLUSH;
    }
    public override bool ContainsHand(List<Card> cardList)
    {
        if(straightFlush.ContainsHand(cardList))
        {
            if(cardList[0].Value == (int)HighCard.Ace)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}