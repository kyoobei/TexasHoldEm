using System.Collections.Generic;

public class FullHouse : Rule
{
    OnePair onePair = new OnePair();
    ThreeOfAKind threeOfAKind = new ThreeOfAKind();
    public override Hand GetRuleHand()
    {
        return Hand.FULL_HOUSE;
    }
    public override bool ContainsHand(List<Card> cardList)
    {
        if(threeOfAKind.ContainsHand(cardList) && onePair.ContainsHand(cardList))
        {
            return true;
        }
        return false;
    }
}