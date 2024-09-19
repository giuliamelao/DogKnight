using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI cronometroText;
    private float tempoInicial = 30f; 
    private bool cronometroAtivo = false; 
    private float tempoRestante; 

    void Update()
    {
        if (cronometroAtivo)
        {
            tempoRestante -= Time.deltaTime; 
            if (tempoRestante <= 0f)
            {
                tempoRestante = 0f;
                cronometroAtivo = false;
            }

            AtualizarCronometro();
        }
    }

    void AtualizarCronometro()
    {
        int segundos = Mathf.RoundToInt(tempoRestante);
        string tempoFormatado = string.Format("{0:00}", segundos);
        cronometroText.text = "Modo Invencível: " + tempoFormatado;
    }

    public void IniciarCronometro(float duracao)
    {
        tempoRestante = duracao;
        cronometroAtivo = true;
    }

    public void PararCronometro()
    {
        cronometroAtivo = false;
    }
}
