using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class AdewgvcbweDsdf : MonoBehaviour
{
    public static AdewgvcbweDsdf Instance { get; private set; }

    [SerializeField] private PlasdWEbfgayer player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
   
    [SerializeField] private GameObject coinsObject;
    [SerializeField] private GameObject scoreObject;

    private int coinsCount=0;
    [SerializeField] private TextMeshProUGUI textCoins;
    
    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
       
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        coinsCount = 0;
        UpdateCoinsText();
        playButton.SetActive(false);
        gameOver.SetActive(false);
       
        coinsObject.SetActive(true);
        scoreObject.SetActive(true);


        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);
      
       

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();


        coinsCount +=5;
        UpdateCoinsText();
        ZolotoManager.Instance.AddZoloto(5);
    }
    public void UpdateCoinsText()
    {
        textCoins.text = coinsCount.ToString();  
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
   
  

   

}
