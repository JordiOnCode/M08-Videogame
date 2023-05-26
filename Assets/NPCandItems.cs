using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCandItems : MonoBehaviour
{
    public List<string> npcMessages = new List<string> { "¿De nuevo aquí? ¿Te gusta el suelo o qué?", 
        "Oye, parece que te caes mucho. ¿Necesitas un mapa?", 
        "Parece que estás volviendo a caer. ¿Estás buscando algo en el suelo?",
        "Te caes con tanta frecuencia que ya podríamos poner un colchón aquí.",
        "Hasta las piedras aprenden después de caer dos veces.",
        "Creo que los suelos de este juego te aman mucho.",
        "¡Uy! Otra vez por aquí... ¿Estás seguro de que esto no es tu hobby secreto?",
        "Puedo notar una cierta afinidad entre tú y la ley de gravedad.",
        "¿Sabes que el juego no se gana al caer más veces, verdad?",
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
                uiText.text = "Dice la leyenda que en lo más alto...";
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
