using System.Collections.Generic;

public class HighCard : Rule
{
    public override Hand GetRuleHand()
    {
        return Hand.HIGH_CARD;
    }
    public override bool ContainsHand(List<Card> cardList)
    {
        //always the default hand so true
        return true;
    }
}