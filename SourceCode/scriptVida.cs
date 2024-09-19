using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LifesManager : MonoBehaviour
{
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI cronometroText; 
    private int vidas = 3;

    private bool isInvincible = false; 
    private float invincibleEndTime; 
    private Cronometro cronometro; 

    void Start()
    {
        UpdateLifeText();
        cronometro = FindObjectOfType<Cronometro>(); 
        if (cronometro == null)
        {
            Debug.LogError("Cronômetro não encontrado!");
        }
    }

    public void PerderVida()
    {
        if (!isInvincible)
        {
            vidas--;
            if (vidas <= 0)
            {
                Invoke("CarregarGameOverScene", 1f);
            }
            else
            {
                UpdateLifeText();
            }
        }
    }

    public void ActivateInvincibility(float duration)
    {
        isInvincible = true;
        invincibleEndTime = Time.time + duration; 
        Debug.Log("Modo invencível ativado por " + duration + " segundos!");

        cronometro.IniciarCronometro(duration);

        Invoke(nameof(DeactivateInvincibility), duration);
    }

    private void DeactivateInvincibility()
    {
        isInvincible = false;
        Debug.Log("Modo invencível desativado!");

        cronometro.PararCronometro();
    }

    private void UpdateLifeText()
    {
        string vidasString = "";
        for (int i = 0; i < vidas; i++)
        {
            vidasString += "L ";
        }
        lifeText.text = vidasString;
    }

    private void CarregarGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
