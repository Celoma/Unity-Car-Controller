using UnityEngine;

public class boxbox : MonoBehaviour
{
    public AudioClip soundEffect; // Le son à jouer
    private AudioSource audioSource;

    // Start est appelé une fois avant la première exécution de Update
    void Start()
    {
        // Ajoute un AudioSource si ce n'est pas déjà fait
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundEffect;
    }

    // Appelé lorsqu'un autre objet entre dans le trigger
    void OnTriggerEnter(Collider other)
    {
        if (audioSource != null && soundEffect != null)
        {
            audioSource.Play();
        }
    }
}