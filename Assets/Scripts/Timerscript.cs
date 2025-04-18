using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timerscript : MonoBehaviour
{

    public GameObject endScreen; // À assigner dans l’inspecteur
    private PlayerControls _playerControls;
    [SerializeField] private Text timerText;
    [SerializeField] private Transform playerTransform;
    private float elapsedTime;
    private bool isTimerRunning;

    private bool isFinished = false;
    private Vector3 targetCoordinates = new Vector3(140.2302f, 1f, 83.965f);

    void Start()
    {
        elapsedTime = 0f;
        isTimerRunning = false;
    }

private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
    }
    void Update()
    {


        if (_playerControls.Player.Accelerate.ReadValue<float>() != 0 && isTimerRunning == false && elapsedTime == 0f && !isFinished)
        {
            isTimerRunning = true;
            elapsedTime = 0f;
        }
        
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }

        
    }
    public void finish()
    {
        Debug.Log("Finish line crossed!");
        isFinished = true;
        isTimerRunning = false;
        StartCoroutine(ShowEndScreenAfterDelay(3f)); // Appel de la coroutine
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D3}", minutes, seconds, milliseconds);
    }
        private IEnumerator ShowEndScreenAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        endScreen.SetActive(true);
        Debug.Log("End screen displayed!");
    }
}

