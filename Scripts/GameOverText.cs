using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverText : MonoBehaviour
{
    public static GameOverText instance;
    private bool gameOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void GameOver()
    {
        GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
        gameOver = true;
    }
}
