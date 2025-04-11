using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Assurez-vous d'avoir le package Input System installé
using System;

public class Timerscript : MonoBehaviour
{
    private PlayerControls _playerControls; // Référence aux contrôles du joueur
    [SerializeField] private Text timerText; // Texte pour afficher le timer
    [SerializeField] private Transform playerTransform; // Référence au joueur ou à l'objet suivi
    private float elapsedTime; // Temps écoulé
    private bool isTimerRunning; // Indique si le timer est actif

    // Coordonnées cibles où le timer doit s'arrêter
    private Vector3 targetCoordinates = new Vector3(140.2302f, 1f, 83.965f);

    void Start()
    {
        elapsedTime = 0f;
        isTimerRunning = false; // Le timer est inactif au départ
    }

private void Awake()
    {
        // Initialiser les contrôles du joueur
        _playerControls = new PlayerControls();
        _playerControls.Enable(); // Activer les contrôles
    }
    void Update()
    {


        if (_playerControls.Player.Accelerate.ReadValue<float>() != 0 && isTimerRunning == false && elapsedTime == 0f)
        {
            isTimerRunning = true;
            elapsedTime = 0f; // Réinitialiser le timer
        }
        Debug.Log(Vector3.Distance(playerTransform.position, targetCoordinates));
        
        // Arrêter le timer si le joueur atteint les coordonnées cibles
        if (isTimerRunning && Vector3.Distance(playerTransform.position, targetCoordinates) < 2f)
        {
            Debug.Log("Timer stopped at target coordinates.");
            isTimerRunning = false;
        }

        // Mettre à jour le timer uniquement s'il est actif
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
        timerText.text = string.Format("Time : {0:D2}:{1:D2}:{2:D3}", minutes, seconds, milliseconds);
    }
}