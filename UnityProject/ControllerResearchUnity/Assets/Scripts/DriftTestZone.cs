using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class DriftTestZone : MonoBehaviour
{
    public float testDuration = 5f;
    public float resultShowTime = 4f;

    public TextMeshProUGUI resultText;
    public InputReader inputReader;

    float timer;
    bool testing;

    float maxDrift;
    float totalDrift;
    int sampleCount;

    void Start()
    {
        resultText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartTest();
        }
    }

    void StartTest()
    {
        testing = true;
        timer = testDuration;

        maxDrift = 0f;
        totalDrift = 0f;
        sampleCount = 0;

        resultText.gameObject.SetActive(false);

        Debug.Log("Drift test started");
    }

    void Update()
    {
        if (!testing) return;

        timer -= Time.deltaTime;

        Vector2 input = inputReader.Look; 
        float magnitude = input.magnitude;

        if (magnitude > maxDrift)
            maxDrift = magnitude;

        totalDrift += magnitude;
        sampleCount++;

        if (timer <= 0f)
        {
            FinishTest();
        }
    }

    void FinishTest()
    {
        testing = false;

        float averageDrift = totalDrift / sampleCount;

        resultText.text =
            "DRIFT TEST RESULT\n\n" +
            "MAX: " + maxDrift.ToString("F3") + "\n" +
            "AVG: " + averageDrift.ToString("F3");

        resultText.gameObject.SetActive(true);

        StartCoroutine(HideResult());
    }

    IEnumerator HideResult()
    {
        yield return new WaitForSeconds(resultShowTime);

        resultText.gameObject.SetActive(false);
    }
}