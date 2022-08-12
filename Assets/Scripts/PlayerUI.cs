using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI carPositionText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI gameOverText;
    public CarController car;


    private void Update()
    {
        carPositionText.text = car.racePosition.ToString()+ " / " + 
            GameManager.instance.cars.Count.ToString();        
    }

    public void StartCountdownDisplay()
    {
        StartCoroutine(Countdown());

        IEnumerator Countdown()
        {
            countdownText.gameObject.SetActive(true);
            countdownText.text = "3";
            yield return new WaitForSeconds(1f);
            countdownText.text = "2";
            yield return new WaitForSeconds(1f);
            countdownText.text = "1";
            yield return new WaitForSeconds(1f);
            countdownText.text = "GO!";
            yield return new WaitForSeconds(1f);
            countdownText.gameObject.SetActive(false);
        }
    }

    public void GameOver(bool winner)
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.color = winner ? Color.yellow : Color.red;
        gameOverText.text = winner ? "You Win!" : "You Lost!";
    }
}
