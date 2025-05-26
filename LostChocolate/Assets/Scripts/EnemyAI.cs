using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public GameObject exclamationMark;
    public float investigateDuration = 3f;

    private bool isInvestigating = false;

    void Start()
    {
        if (exclamationMark != null)
            exclamationMark.SetActive(false);
    }

    public void InvestigateSound(Vector3 soundPosition)
    {
        if (!isInvestigating)
        {
            StartCoroutine(ExclamationMarkRoutine());
        }
    }

    IEnumerator ExclamationMarkRoutine()
    {
        isInvestigating = true;

        if (exclamationMark != null)
            exclamationMark.SetActive(true);

        yield return new WaitForSeconds(investigateDuration);

        if (exclamationMark != null)
            exclamationMark.SetActive(false);

        isInvestigating = false;
    }
}
