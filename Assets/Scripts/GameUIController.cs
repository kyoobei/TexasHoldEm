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
    public void ShowResultScreen(string result)
    {
        txtResult.text = result;
        panelUIResult.gameObject.SetActive(true);
        btnPlayButton.gameObject.SetActive(true);
    }
}
