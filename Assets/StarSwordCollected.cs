using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarSwordCollected : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StarSword"))
        {
            Debug.Log("Toca la espada");
            SceneManager.LoadScene("EndScene");
        }
    }
}


