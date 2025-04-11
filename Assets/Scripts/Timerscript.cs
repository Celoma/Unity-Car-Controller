using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class Timerscript : MonoBehaviour
{
    private PlayerControls _playerControls;
    [SerializeField] private Text timerText;
    [SerializeField] private Transform playerTransform;
    private float elapsedTime;
    private bool isTimerRunning;

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


        if (_playerControls.Player.Accelerate.ReadValue<float>() != 0 && isTimerRunning == false && elapsedTime == 0f)
        {
            isTimerRunning = true;
            elapsedTime = 0f;
        }
        Debug.Log(Vector3.Distance(playerTransform.position, targetCoordinates));

        if (isTimerRunning && Vector3.Distance(playerTransform.position, targetCoordinates) < 2f)
        {
            isTimerRunning = false;
        }

        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }

        
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D3}", minutes, seconds, milliseconds);
    }
}