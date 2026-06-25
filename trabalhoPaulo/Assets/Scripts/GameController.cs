using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private int score;
    [SerializeField]private int pasta;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text pastaText;

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
       
    }

    private void PrepararEstadoDaCena()
    {
        Time.timeScale = 1;

        if (gameOver != null)
        {
            gameOver.SetActive(false);
        }

        AtualizarCollectablesHUD();
    }

    public void GetCoin()
    {
        score++;
        AtualizarCollectablesHUD();
    }

    public void GetPasta()
    {
        pasta++;
        AtualizarCollectablesHUD();
    }



    private void AtualizarCollectablesHUD()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }

        if (pastaText != null)
        {
            pastaText.text = pasta.ToString();
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
