using UnityEngine.SceneManagement;
using UnityEngine;

public class transition1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Scene 2");
        }
    }
}
