using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayingGameSession : MonoBehaviour
{
    //cached references
    [SerializeField] private GameObject playerLivesTextComponent;
    [SerializeField] private GameObject levelNumberTextComponent;
    private GameSession gameSession;
    private TextMeshProUGUI playerLivesText;
    private TextMeshProUGUI levelNumberText;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        playerLivesText = playerLivesTextComponent.GetComponent<TextMeshProUGUI>();
        levelNumberText = levelNumberTextComponent.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        playerLivesText.text = gameSession.GetPlayerLivesCount().ToString();
        levelNumberText.text = gameSession.GetLevelNumber().ToString();
    }
}
