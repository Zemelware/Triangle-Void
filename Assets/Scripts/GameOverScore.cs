using TMPro;
using UnityEngine;

public class GameOverScore : MonoBehaviour
{
    public Score score;
    public TextMeshProUGUI text;
    
    void Start()
    {
        text.text = $"Score: {score.score}";
    }
}
