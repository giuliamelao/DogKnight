using UnityEngine;

public class GemCollector : MonoBehaviour
{
    private ScoreManager scoreManager;
    private AudioSource audioSource;
    public AudioClip gemCollectSound;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager não encontrado!");
        }
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource não encontrado no GameObject!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            Debug.Log("Gema coletada!");
            
            if (gemCollectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(gemCollectSound);
            }
            else
            {
                Debug.LogError("gemCollectSound ou audioSource não está configurado corretamente!");
            }

            Destroy(other.gameObject);
            scoreManager.AddScore(1);
        }
    }
}
