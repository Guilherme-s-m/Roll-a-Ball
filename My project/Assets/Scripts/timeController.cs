using UnityEngine;
using TMPro; // Não esqueça de incluir esta biblioteca para usar o TextMeshPro
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 10; // Tempo inicial em segundos
    public TextMeshProUGUI countdownText; // Referência ao componente de texto

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateCountdownText();
        }
        else
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    void UpdateCountdownText()
    {
        // Formata o tempo restante em minutos:segundos
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
