using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasketDetector : MonoBehaviour
{
    bool hasEnter;
    private GameManager gameManager;
    [SerializeField ]private int countBall;
    [SerializeField] private TextMeshProUGUI txtGameScore;

    private void Start()
    {
        gameManager = GameObject.Find(nameof(GameManager)).GetComponent<GameManager>();
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BasketEnter"))
        {
            hasEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BasketExit") && hasEnter)
        {
            hasEnter = false;
            countBall++;
            gameManager.PlayScore = countBall;
            txtGameScore.text = "Score: " + gameManager.PlayScore.ToString();
            Debug.Log(countBall);
        }
    }
}
