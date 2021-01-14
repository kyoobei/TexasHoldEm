using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    private const float MIN_DISTANCE = 0.1f;
    [SerializeField] private float movementSpeed = 0.0f;
    [SerializeField] private float rotationSpeed = 0.0f;
    [SerializeField] private CardDisplayBehaviour m_cardDisplayBehaviour = null;
    private Vector3 m_targetPosition = Vector3.zero;
    private bool m_shouldMove = false;
    private bool m_shouldRotate = false;
    private void Update()
    {
        if(m_shouldMove)
        {
            MoveTowardsTarget();
        }
        if(m_shouldRotate)
        {
            RotateCardToFaceUp();
        }  
    }
    public void UpdateDisplay(Card card)
    {
        m_cardDisplayBehaviour.UpdateCardDisplay(card.Value, (int)card.Suit);
    }
    public void MoveCard(Vector3 targetPosition)
    {
        m_targetPosition = targetPosition;
        m_shouldMove = true;
    }
    public void ResetBehaviour()
    {
        m_targetPosition = Vector3.zero;
        m_shouldRotate = false;
        m_shouldMove = false;
    }
    private void MoveTowardsTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, m_targetPosition);
        if(distanceToTarget > MIN_DISTANCE)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_targetPosition, movementSpeed * Time.deltaTime);
        }
        else if(distanceToTarget <= MIN_DISTANCE)
        {
            m_shouldMove = false;
            m_shouldRotate = true;
        }
    }
    private void RotateCardToFaceUp()
    {
        Quaternion cardFaceUpRotation = Quaternion.Euler(0,0f,180f);
        transform.rotation = Quaternion.Slerp(transform.rotation,
            cardFaceUpRotation, rotationSpeed * Time.deltaTime);
    }
}
