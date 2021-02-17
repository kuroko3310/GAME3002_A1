using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_ScoreText = null;
    [SerializeField]
    private TextMeshProUGUI m_BallLeftText = null;
    [SerializeField]
    private TextMeshProUGUI m_GameOverText = null;
    [SerializeField]
    private TextMeshProUGUI m_WinText = null;
   

    public void OnRequestUpdateUI(int iScore, int iBallLeft)
    {
        UpdateParams(iScore, iBallLeft);
    }

    private void UpdateParams(int iScore, int iBallLeft)
    {
        m_ScoreText.text = "Score: " + iScore;
        m_BallLeftText.text = "Ball Left: " + iBallLeft;
    }

    public void OnRequestGameOverUI()
    {
        UpdateGameOverUI();
    }

    private void UpdateGameOverUI()
    {
        m_GameOverText.text = "GAME OVER";
    }


    public void OnRequestWinUI()
    {
        UpdateWinUI();
    }

    private void UpdateWinUI()
    {
        m_WinText.text = "YOU WON!";
    }

}
