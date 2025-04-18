using System.Collections;
using UnityEngine;
using UnityEngine.UI; // nécessaire pour le texte UI
using TMPro; // ✅ pour TextMeshPro

public class fin : MonoBehaviour
{
    public TMP_Text victoryText;                // Le texte à afficher (à assigner dans l’inspecteur)
    public Text Timer;                // Le texte à afficher (à assigner dans l’inspecteur)

    void Start()
    {
        victoryText.text = $"Super  ! tu as finis en {Timer.text} !";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
