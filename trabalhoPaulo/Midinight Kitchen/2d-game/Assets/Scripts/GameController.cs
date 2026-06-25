using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private int score;

    [SerializeField] private Text scoreText;

    [SerializeField] private Image voidBar;
    [SerializeField] private Image fullBar;

    [SerializeField] private GameObject gameOver;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += AoCarregarCena;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AoCarregarCena;
    }

    private void Start()
    {
        PrepararEstadoDaCena();
    }

    private void AoCarregarCena(Scene scene, LoadSceneMode mode)
    {
        PrepararEstadoDaCena();
    }

    private void Update()
    {
        if (fullBar != null && Player.instance != null)
        {
            fullBar.fillAmount = Player.instance.AmountHealth;
        }
    }

    private void PrepararEstadoDaCena()
    {
        Time.timeScale = 1;

        if (gameOver != null)
        {
            gameOver.SetActive(false);
        }

        AtualizarScoreHUD();
    }

    public void GetCoin()
    {
        score++;
        AtualizarScoreHUD();
    }

    private void AtualizarScoreHUD()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void NextLevel(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0;

        if (gameOver != null)
        {
            gameOver.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameController: o objeto GameOver não foi atribuído no Inspector.");
        }
    }

    public void OnRestart()
    {
        Time.timeScale = 1;

        if (gameOver != null)
        {
            gameOver.SetActive(false);
        }

        if (Player.instance != null)
        {
            Player.instance.ResetarEstadoDoPlayer();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}