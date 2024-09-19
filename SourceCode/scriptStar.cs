using UnityEngine;
using TMPro;

public class StarCollector : MonoBehaviour
{
    public AudioClip starCollectSound;
    public float invincibleDuration = 30f;  // Duração da invencibilidade em segundos

    private AudioSource audioSource;
    private LifesManager lifesManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource não encontrado no GameObject!");
        }

        lifesManager = FindObjectOfType<LifesManager>();
        if (lifesManager == null)
        {
            Debug.LogError("LifesManager não encontrado!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Star"))
        {
            Debug.Log("Estrela coletada!");

            if (starCollectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(starCollectSound);
            }
            else
            {
                Debug.LogWarning("starCollectSound ou audioSource não está configurado corretamente!");
            }

            lifesManager.ActivateInvincibility(invincibleDuration);

            Destroy(other.gameObject);
        }
    }
}
