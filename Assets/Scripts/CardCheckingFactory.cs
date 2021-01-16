public class CardCheckingFactory
{
    private RoyalFlush royalFlush;
    private StraightFlush straightFlush;
    private FourOfAKind fourOfAKind;
    private Flush flush;
    private FullHouse fullHouse;
    private Straight straight;
    private ThreeOfAKind threeOfAKind;
    private TwoPair twoPair;
    private OnePair onePair;
    
    public CardCheckingFactory()
    {
        royalFlush = new RoyalFlush();
        straightFlush = new StraightFlush();
        fourOfAKind = new FourOfAKind();
        flush = new Flush();
        fullHouse = new FullHouse();
        straight = new Straight();
        threeOfAKind = new ThreeOfAKind();
        twoPair = new TwoPair();
        onePair = new OnePair();
    }

    public Hand GetCurrentHand()
    {
        return Hand.HIGH_CARD;        
    }
}