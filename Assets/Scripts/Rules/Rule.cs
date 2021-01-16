using System.Collections.Generic;
public abstract class Rule
{
    public abstract Hand GetRuleHand();
    public abstract bool ContainsHand(List<Card> cardList);
}