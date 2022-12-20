using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ThrowController : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody2D pivot;
    public float timeCutSpringJoin;
    public float timeNewGame;

    private Camera cam;
    private Rigidbody2D ballRigidbody;
    private SpringJoint2D ballSpringJoin;
    private bool isStreching;
    [SerializeField] private GameObject Score;
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private GameManager gameManager;

    private Vector3 startpos;
    private Vector3 startcampos;

    private void Start()
    {
        gameManager = GameObject.Find(nameof(GameManager)).GetComponent<GameManager>();
        startpos = ball.transform.position;
        cam = Camera.main;
        startcampos = cam.transform.position;
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballSpringJoin = ball.GetComponent<SpringJoint2D>();
        //Reconnect springJoin to Pivot
        ballSpringJoin.connectedBody = pivot;
    }

    private void Update()
    {
        if(!isStreching)
        camControl();
        //if the ball has not rigidbody dont do anything
        if (ballRigidbody == null) return;

        //if not touching the screen , it means that you just throw
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            //if it is Streching
            if (isStreching )
            {
                ThrowBall();

            }
            if(Vector3.Distance(pivot.transform.position, ballRigidbody.transform.position) >= 1f)
            {
                CutSpringJoin();
            }
            //update isStreching to false
            isStreching = false;
            
            return;
        }
        
        
        //if is touching the screen for the first time
        isStreching = true;

        //take the ball controll with its rigidbody
        ballRigidbody.isKinematic = true;

        //Get touch position
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        //get the position from the world 
        Vector3 worldPosition = cam.ScreenToWorldPoint(touchPosition);

        //Apply worldposition to ball RigidBody
        ballRigidbody.position = worldPosition;
    }

    private void ThrowBall()
    {
        //get back the control of rigidbody to the ball
        ballRigidbody.isKinematic = false;
        ballRigidbody = null;
        

        //Apply time to desactivate SpringJoin
        Invoke(nameof(CutSpringJoin), timeCutSpringJoin);
    }

    void camControl()
    {
        cam.transform.position = new Vector3 (ball.transform.position.x + 10, ball.transform.position.y, ball.transform.position.z -10);
    }

    private void CutSpringJoin()
    {
        ballSpringJoin.enabled = false;
        //ballSpringJoin = null;

        //Time to new game
        if (gameManager.playNumber >= 0)
            Invoke(nameof(RestartGame), timeNewGame);
        else
        {
            Invoke(nameof(ScoreBoard), timeNewGame);
        }
    }

    private void ScoreBoard()
    {
        Score.SetActive(true);
        txtScore.text = gameManager.PlayScore.ToString();
    }

    

    private void RestartGame()
    {
        gameManager.playNumber--;
        
        ball.transform.position = startpos;
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballRigidbody.velocity = Vector2.zero;
        ballRigidbody.isKinematic = true;
        ballSpringJoin.enabled = true;
        isStreching = false;
        //SceneManager.LoadScene("Game");
    }
}
