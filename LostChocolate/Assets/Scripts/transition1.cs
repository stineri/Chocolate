using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.SearchService;

public class transition1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene("Scene 2");
        }
    }
}
