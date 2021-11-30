using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject square;
    public GameObject endPanel;
    public Text thisScoreTxt;
    public Text maxScoreTxt;
    public Animator anim;


    public Text timeTxt;
    float alive = 0f;
    bool isRunning = true;

    public static gameManager I;
    
    void Awake()
    {
        I = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        InvokeRepeating("makeSquare", 0.0f, 0.5f);
    }

    // Update is called once per frame
 

    void Update()
    {
        if (isRunning)
        {
            alive += Time.deltaTime;
            timeTxt.text = alive.ToString("N2");
        }
    }

    void makeSquare()
    {
        Instantiate(square);
    }

    public void gameOver()
    {
        
        anim.SetBool("isDie", true);

        isRunning = false;
        Invoke("timeStop", 0.5f);
        thisScoreTxt.text = alive.ToString("N2");
        endPanel.SetActive(true);

        if (PlayerPrefs.HasKey("bestScore") == false)
        {
            PlayerPrefs.SetFloat("bestScore", alive);
        }
        else
        {
            if (PlayerPrefs.GetFloat("bestScore") < alive)
            {
                PlayerPrefs.SetFloat("bestScore", alive);
            }
        }
        float maxScore   = PlayerPrefs.GetFloat("bestScore");
        maxScoreTxt.text = maxScore.ToString("N2"); 
    }

    public void retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    void timeStop()
    {
        Time.timeScale = 0.0f;
    }
}

