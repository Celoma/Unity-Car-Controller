using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    private PlayerControls _playerControls;
    private CarController _carController;
    public float accel;
    public float handBrake;
    public float turn;
    public UnityEvent onFinish;

    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Rigidbody _rigidbody;

    public static Action<bool> OnPause;

    private bool _hasFinished; // Track if the finish line has been crossed

    private void Awake()
    {
        if (pauseMenuUI == null)
        {
            Debug.LogError("Pause menu UI is not assigned in the inspector!");
        }
        else
        {
            pauseMenuUI.SetActive(false);
        }

        _playerControls = new PlayerControls();
        _playerControls.Player.Enable();
        _carController = GetComponent<CarController>();
        _rigidbody = GetComponent<Rigidbody>();

        if (_carController == null)
        {
            Debug.LogError("CarController component not found!");
        }

        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody component not found!");
        }

        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _hasFinished = false;

        _playerControls.Player.Reset.performed += _ => Respawn();
        _playerControls.Player.Pause.performed += Pause_performed;
    }

    private void OnEnable()
    {
        _playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Player.Disable();
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        accel = _playerControls.Player.Accelerate.ReadValue<float>();
        turn = _playerControls.Player.Turn.ReadValue<float>();
        handBrake = _playerControls.Player.HandBrake.ReadValue<float>();
        // Check finish line condition and ensure it's only triggered once
        if (!_hasFinished && Mathf.Abs(transform.position.x - (_startPosition.x - 5)) < 3f && Mathf.Abs(transform.position.z - _startPosition.z) < 20f)
        {
            _hasFinished = true; // Set the flag to prevent multiple triggers
            onFinish.Invoke(); // Trigger the finish event
        }
    }

    private void FixedUpdate()
    {
        _carController.Move(turn, accel, accel, handBrake);
    }

    public void Respawn()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _hasFinished = false; // Reset the finish flag when respawning
    }
}
