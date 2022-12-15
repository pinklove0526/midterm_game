using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HUD : MonoBehaviour
{
    Player player;
    Text score;
    Text remainingLife;
    Text finalScore;
    GameObject results;
    // Start is called before the first frame update
    private void Awake() {
        
        player = GameObject.Find("Player").GetComponent<Player>();
        score = GameObject.Find("Score").GetComponent<Text>();
        finalScore = GameObject.Find("FinalScoreNumber").GetComponent<Text>();
        remainingLife = GameObject.Find("RemainingLife").GetComponent<Text>();
        results = GameObject.Find("Result");
        results.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int displayScore = Mathf.FloorToInt(player.distance);
        score.text = "Score: " + displayScore/5 + " m";
        if(player.gameOver)
        {
            results.SetActive(true);
            finalScore.text = displayScore/5 + "m";
        }
        int displayLife = player.playerLife;
        remainingLife.text = "Life: " + displayLife;

    }
    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
