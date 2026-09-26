using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class EnergyMeterLogic : MonoBehaviour
{
    public int meter_state = 0; // Max = 8 (0-indexed)
    public Tile[] meter_tiles;
    [SerializeField] private GameObject player;
    private Movement movement;
    public Canvas gameUICanvas;
    public Canvas gameOverUICanvas;
    [SerializeField] private Timer timer;
    public InputController inputController;
    public string nextScene;
    
    void Start()
    {
        movement = player.GetComponent<Movement>();
        gameOverUICanvas.enabled = false;
    } 

    void Update()
    {
        if(inputController.isSpacePressed && gameOverUICanvas.enabled) {
            Debug.Log("Space is pressed, loading next scene!");
            SceneManager.LoadScene(nextScene);
        }
    }

    public void FinishStage() {
        timer.StopTimer();
        
        movement.enabled = false;
        gameUICanvas.enabled = false;
        gameOverUICanvas.enabled = true;
        gameOverUICanvas.GetComponentInChildren<TMP_Text>().text = "Your time:\n" + timer.timerString;
    }
}
