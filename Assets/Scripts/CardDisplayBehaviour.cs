using System.Collections.Generic;
using UnityEngine;

public class CardDisplayBehaviour : MonoBehaviour
{
    [SerializeField] List<Material> m_suitsMaterialList = new List<Material>();
    [SerializeField] Renderer m_suitRenderer = null;
    [SerializeField] TextMesh m_suitValueText = null;

    public void UpdateCardDisplay(int cardValue, int cardSuit)
    {
        if(cardValue < (int)HighCard.Jack)
        {
            m_suitValueText.text = (cardValue + 1).ToString();
        }
        else if(cardValue >= (int)HighCard.Jack)
        {
            m_suitValueText.text = (((HighCard)cardSuit).ToString())[0].ToString();
        }
        m_suitRenderer.sharedMaterial = m_suitsMaterialList[cardSuit];
    }
}
