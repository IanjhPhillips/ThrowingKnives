using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
	public static Manager manager;

	public GameObject canvas;
	public Text scoreText;
	public GameObject menuText;
	public Text highScoreText;
	public Text timeText;
	public Image[] knives; 

	private int score;
	private int highScore;
	private float time;
	private bool gameOver;
	private Scene currentScene;

	private int lives;

	//handles singleton creation and tracking
	//relays important info from old manager to new manager
	void Awake () {

		if (manager == null) {
			manager = this;
			this.highScore = 0;
			DontDestroyOnLoad(this.gameObject);
		}
		else if (manager != this) {
			this.highScore = manager.getHighScore();
			Destroy(manager.gameObject);
			manager = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

    void Start()
    {
    	currentScene = SceneManager.GetActiveScene();
        score = 0;
        time = 15f;
        gameOver = false;
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
    	//countdown time
    	//if time is at 0, activate end text and set gameOver to true
    	//pressing return while game is over reloads scene
        time -= Time.deltaTime;
        if (time <= 0f) {
        	time = 0.00f;
        	gameOver = true;
        	menuText.SetActive(true);
        	highScoreText.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown("return") && gameOver)  {
        	SceneManager.LoadScene(currentScene.name);
        }

        //displays score and other info
        timeText.text = "Time remaining: " + time.ToString("#0.00") + "s";
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }

    public bool isGameOver () {
    	return gameOver;
    }

    //disables one life sprite
    //if lives is at 0 ---> end game
    public void LoseLife () {
    	if (lives > 0) {
    		lives--;
    		knives[lives].enabled = false;
    	}
    	if (lives <= 0) {
    		time = 0;
    		gameOver = true;
    	}
    }

    public void IncreaseScore (int i) {
    	score += i;
    	if (score > highScore)
    		highScore = score;
    }

    public int getHighScore () {
    	return highScore;
    }
}
