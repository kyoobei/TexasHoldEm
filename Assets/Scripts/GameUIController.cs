using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GameUIController : MonoBehaviour, IController
{
    public Action OnPressedStartGame;
    [SerializeField] private Pooler m_pooler = null;
    [SerializeField] private GameObject panelUIResult = null;
    [SerializeField] private Button btnPlayButton = null;
    [SerializeField] private List<Text> txtPlayerLogs = new List<Text>();
    [SerializeField] private Text txtResult = null;
    [SerializeField] private float m_showDelay = 0.0f;
    private string m_result = string.Empty;
    private void OnEnable()
    {
        m_pooler.OnClearPooler += StartGame;
    }
    private void OnDisable()
    {
        m_pooler.OnClearPooler -= StartGame;
    }
    public void StartController()
    {
        m_pooler.ReturnAllClone();     
    }
    public void StartGame()
    {
        OnPressedStartGame?.Invoke();
    }
    public void UpdatePlayerLogs(List<string> playerName, List<Hand> playerHands)
    {
        for(int i = 0; i < txtPlayerLogs.Count; i++)
        {
            string playerHandsName = playerHands[i].ToString();
            playerHandsName = playerHandsName.Replace("_", " ");
            txtPlayerLogs[i].text = string.Format($"{playerName[i]} has {playerHandsName}");
        }
    }
    public void StartDelayShowResult(string result)
    {
        m_result = result;
        Invoke("ShowResultScreen", m_showDelay);
    }
    private void ShowResultScreen()
    {
        txtResult.text = m_result;
        panelUIResult.gameObject.SetActive(true);
        btnPlayButton.gameObject.SetActive(true);
    }
}
