using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    private const float MIN_DISTANCE = 0.1f;
    [SerializeField] private float movementSpeed = 0.0f;
    [SerializeField] private CardDisplayBehaviour m_cardDisplayBehaviour = null;
    private Vector3 m_targetPosition = Vector3.zero;
    private bool m_shouldMove = false;
    private void Update()
    {
        if(!m_shouldMove)
            return;

        float distanceToTarget = Vector3.Distance(transform.position, m_targetPosition);
        if(distanceToTarget > MIN_DISTANCE)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_targetPosition, movementSpeed * Time.deltaTime);
        }
    }
    public void ReadyCard(Card card, Vector3 targetPosition)
    {
        m_cardDisplayBehaviour.UpdateCardDisplay(card.Value, (int)card.Suit);
        m_targetPosition = targetPosition;
        m_shouldMove = true;
    }
    public void ResetBehaviour()
    {
        m_targetPosition = Vector3.zero;
        m_shouldMove = false;
    }
}
