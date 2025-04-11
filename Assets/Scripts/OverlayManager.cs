using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour gérer les scènes

public class MainMenuController : MonoBehaviour
{
    // Fonction appelée par le bouton "Start"
    public void VoitureJeu()
    {
        Debug.Log("Start"); // Fonctionne uniquement dans l'éditeur pour vérifier
        // Charge la scène de jeu (remplacez "GameScene" par le nom de votre scène)
        SceneManager.LoadSceneAsync("VehicleScene");
        SceneManager.UnloadSceneAsync("Overlay"); // Décharge la scène du menu principal
    }

    // Fonction appelée par le bouton "Exit"
    public void ExitGame()
    {
        // Quitte l'application
        Application.Quit();
        Debug.Log("Le jeu a été quitté."); // Fonctionne uniquement dans l'éditeur pour vérifier
    }
}
