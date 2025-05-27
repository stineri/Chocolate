using UnityEngine.SceneManagement;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Scene 3");
        }
    }
}
