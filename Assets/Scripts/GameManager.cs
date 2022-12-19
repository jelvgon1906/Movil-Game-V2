using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playNumber = 5;

    public int playScore;


    public int PlayScore { get => playScore; set => playScore = value; }
    private void Awake()
    {
        int instancesNumber = FindObjectsOfType<GameManager>().Length;

        /*if (instancesNumber != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }*/
    }


    public void OnClickGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

