using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{

    public static int Difficulty;

    GameObject diffyculltSelection;
    GameObject playButton;
    GameObject quitButton;

    // Start is called before the first frame update
    private void Awake() 
    {
        playButton = GameObject.Find("PlayButton");
        quitButton = GameObject.Find("QuitButton");
        diffyculltSelection = GameObject.Find("DifficultSelection");
        diffyculltSelection.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void play()
    {
        playButton.SetActive(false);
        quitButton.SetActive(false);
        diffyculltSelection.SetActive(true);
        // SceneManager.LoadScene("SampleScene");
    }
    public void easymode()
    {
 
        diffyculltSelection.SetActive(false);
        Difficulty = 1;
        SceneManager.LoadScene("SampleScene");
        
         
    }
    public void hardmode()
    {

        diffyculltSelection.SetActive(false);
        Difficulty = 3;
        SceneManager.LoadScene("SampleScene");
        
        
    }
    public void returnButton(){
        diffyculltSelection.SetActive(false);
        playButton.SetActive(true);
        quitButton.SetActive(true);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
