using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public Camera Camera;
    public gameManager GameManager;
    public GameObject[] Spawners;
    public GameObject MenuPause;
    public GameObject ConfirmWindow;
    public Texture2D CursorTexture;
    
    [Space] public GameObject MenuLevel;
    
    [Space]public GameObject Rank;
    public Text RankText;

    [Space] public Text ScoreText;
    public GameObject NextButton;
    public GameObject ReplayButton;

    private bool _pause = false;
    private bool _confirm = false;

    private void Start()
    {
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        CheckLevelProgress();
        InputKey();
    }

    void CheckLevelProgress()
    {
        if (GameManager.lastWave
            && CheckFinish())
        {
            SuccessLevel();
        }
    }

    bool CheckFinish()
    {
        foreach (GameObject spawner in Spawners) {
            if (spawner.GetComponent<ennemySpawner>().isEmpty == false || spawner.transform.childCount > 1)
            {
                return false;
            }
        }

        return true;
    }

    void InputKey()
    {
        if (!_pause)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(CastVector.CastToVector2(mousePosition), Vector3.forward, 0.01f);

                if (hit
                    && hit.collider.gameObject.CompareTag("speedUI"))
                {
                    GameManager.changeSpeed(hit.collider.gameObject.GetComponent<ButtonSpeed>().Value);
                }
            }
            if (Input.GetKeyDown("escape"))
            {
                SwitchPause();
            }
        }
            
    }

    public void SuccessLevel()
    {
        CountRank();
        CountScore();
        MenuLevel.SetActive(true);
        Rank.SetActive(true);
        NextButton.SetActive(true);
    }

    public void GameOver()
    {
        CountScore();
        MenuLevel.SetActive(true);
        ReplayButton.SetActive(true);
    }

    void CountRank()
    {
        int score = GameManager.playerHp * 100 + GameManager.playerEnergy;

        if (score > 2500)
        {
            RankText.text = "A+";
        }
        else if (score > 1800)
        {
            RankText.text = "A";
        }
        else if (score > 1300)
        {
            RankText.text = "B";
        }
        else if (score > 800)
        {
            RankText.text = "C+";
        }
        else
        {
            RankText.text = "C";
        }
    }

    void CountScore()
    {
        ScoreText.text = GameManager.score.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ex01");
    }

    public void SwitchConfirm()
    {
        _confirm = !_confirm;
        ConfirmWindow.SetActive(_confirm);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("ex00");
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("ex02");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void SwitchPause()
    {
        _pause = !_pause;
        GameManager.pause(_pause);
        MenuPause.SetActive(_pause);
    }
}
