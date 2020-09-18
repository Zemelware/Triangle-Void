using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI text;
    int highScore;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);

        text.text = "Best: " + highScore;
    }
}
