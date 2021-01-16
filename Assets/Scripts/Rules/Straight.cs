using System.Collections.Generic;

public class Straight: Rule
{
    public override Hand GetRuleHand()
    {
        return Hand.STRAIGHT;
    }
    public override bool ContainsHand(List<Card> cardList)
    {
        for(int i = 0; i < cardList.Count; i++)
        {
            //don't check last count
            if(i != cardList.Count - 1)
            {
                int nextCardValue = cardList[i + 1].Value;
                int expectedCardValue = cardList[i].Value - 1;
                if(expectedCardValue.Equals(nextCardValue))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
}