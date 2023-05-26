using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCandItems : MonoBehaviour
{
    public List<string> npcMessages = new List<string> { "�De nuevo aqu�? �Te gusta el suelo o qu�?", 
        "Oye, parece que te caes mucho. �Necesitas un mapa?", 
        "Parece que est�s volviendo a caer. �Est�s buscando algo en el suelo?",
        "Te caes con tanta frecuencia que ya podr�amos poner un colch�n aqu�.",
        "Hasta las piedras aprenden despu�s de caer dos veces.",
        "Creo que los suelos de este juego te aman mucho.",
        "�Uy! Otra vez por aqu�... �Est�s seguro de que esto no es tu hobby secreto?",
        "Puedo notar una cierta afinidad entre t� y la ley de gravedad.",
        "�Sabes que el juego no se gana al caer m�s veces, verdad?",
    };

    public TMP_Text uiText;
    public GameObject textPanel;

    private bool firstInteraction = true;

    private void Start()
    {
        textPanel.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StarSword"))
        {
            Debug.Log("Toca la espada");
            SceneManager.LoadScene("EndScene");
        }

        if (collision.gameObject.CompareTag("NPCStart"))
        {
            if (firstInteraction)
            {
                uiText.text = "Dice la leyenda que en lo m�s alto...";
                StartCoroutine(ShowSecondMessage(5, "hay una espada legendaria, capaz de derrotar a todos. No creo que sea cierto..."));
            }
            else
            {
                string randomMessage = npcMessages[Random.Range(0, npcMessages.Count)];
                uiText.text = randomMessage;
                StartCoroutine(HideAfterSeconds(5));
            }
            textPanel.SetActive(true);
        }
    }

    IEnumerator ShowSecondMessage(int seconds, string message)
    {
        yield return new WaitForSeconds(seconds);
        uiText.text = message;
        StartCoroutine(HideAfterSeconds(5));
        firstInteraction = false;
    }

    IEnumerator HideAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        textPanel.SetActive(false);
    }
}
