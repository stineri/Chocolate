using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private Vector3 startPosition;
    private bool isInvestigating = false;
    public float moveSpeed = 2f;
    public float investigateDuration = 3f;

    private enemyPatrol patrol;
    public GameObject exclamationMark; // ✅ Assign this in Inspector

    void Start()
    {
        startPosition = transform.position;
        patrol = GetComponent<enemyPatrol>(); // ✅ Reference to patrol script

        if (exclamationMark != null)
            exclamationMark.SetActive(false); // Hide "!" at start
    }

    public void InvestigateSound(Vector3 soundPosition)
    {
        if (!isInvestigating)
        {
            StartCoroutine(InvestigateRoutine(soundPosition));
        }
    }

    IEnumerator InvestigateRoutine(Vector3 target)
    {
        isInvestigating = true;

        if (patrol != null)
            patrol.isPaused = true;

        if (exclamationMark != null)
            exclamationMark.SetActive(true);

        float time = 0f;
        while (time < investigateDuration)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f); // Pause at sound location

        while ((transform.position - startPosition).sqrMagnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (exclamationMark != null)
            exclamationMark.SetActive(false);

        if (patrol != null)
            patrol.isPaused = false;

        isInvestigating = false;
    }
}
