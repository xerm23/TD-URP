using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// script uses static functions to read and write highscore into playerprefs
/// </summary>
public class HighScore : MonoBehaviour
{
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    public static void SetHighScore(int score)
    {
        PlayerPrefs.SetInt("HighScore", score);
    }

    #region Used in start scene
    [SerializeField] TMPro.TextMeshProUGUI highScoreTMP;

    private void Awake()
    {
        if (GetHighScore() == 0)
            highScoreTMP.gameObject.SetActive(false);
        else highScoreTMP.text = "High Score: " + GetHighScore();
    }  

    //this is used for play button on main menu
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    #endregion

}
