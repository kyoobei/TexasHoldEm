using System.Collections.Generic;
public class CardCheckingFactory
{    
    private List<Rule> m_rulesList;
    
    public CardCheckingFactory()
    {
        m_rulesList = new List<Rule>()
        {
            new RoyalFlush(),
            new StraightFlush(),
            new Flush(),
            new Straight(),
            new FullHouse(),
            new FourOfAKind(),
            new TwoPair(),
            new ThreeOfAKind(),
            new OnePair()
        };
    }

    public Hand GetCardHand(List<Card> cardCombination)
    {
        Hand currentHand = Hand.HIGH_CARD;
        foreach(var rule in m_rulesList)
        {
            if(rule.ContainsHand(cardCombination))
            {
                currentHand = rule.GetRuleHand();
                break;
            }
        }
        return currentHand;  
    }
}