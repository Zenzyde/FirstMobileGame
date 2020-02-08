using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PinSpawner : MonoBehaviour
{

    public GameObject pin;
    public static PinSpawner Instance;
    private bool pinSpawned = false;
    Score score;
    private List<PinNeedle> activeNotShotNeedles = new List<PinNeedle>();
    [SerializeField] private int maxMultiplier = 5;
    [SerializeField] private int hitCounterLimit = 3;
    [SerializeField] private float multiplierResetInterval = 3.5f;
    private int multiplier, hitCounter;
    private bool multiplierIncreased = false;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        StartCoroutine(SpawnWithDelay());
        StartCoroutine(SideToSide());
        StartCoroutine(MultiplierReset());
        score = new Score();
    }

    public void SpawnPin()
    {
        if (activeNotShotNeedles.Count == 1) return;
        PinNeedle needle = Instantiate(pin, transform.position, transform.rotation, transform).GetComponent<PinNeedle>();
        activeNotShotNeedles.Add(needle);
    }

    public void RemoveActivePin(PinNeedle needle)
    {
        activeNotShotNeedles.Remove(needle);
    }

    public Score GetScore()
    {
        return score;
    }

    public void UpdateScore(int addedScore)
    {
        score.points += addedScore;
    }

    public void IncreaseMultiplier()
    {
        multiplierIncreased = true;
        hitCounter++;
        if (hitCounter % hitCounterLimit == 0)
        {
            hitCounter = 0;
            multiplier += 1;
        }
        multiplier = Mathf.Clamp(multiplier, 1, maxMultiplier);
    }

    public void ResetMultiplier()
    {
        multiplier = 1;
    }

    public int GetMultiplier()
    {
        return multiplier;
    }

    IEnumerator SpawnWithDelay()
    {
        yield return new WaitUntil(() => MenuManager.Instance != null);
        MenuManager.Instance.SetScoreText();
        yield return new WaitForSeconds(.05f);
        SpawnPin();
    }

    IEnumerator SideToSide()
    {
        Vector3 newRight = transform.position + new Vector3(2, 0, 0);
        Vector3 newLeft = newRight - new Vector3(4, 0, 0);
        while (true)
        {
            while (Vector3.Distance(transform.position, newRight) > .5f)
            {
                transform.position += transform.right * 2 * Time.deltaTime;
                yield return null;
            }
            while (Vector3.Distance(transform.position, newLeft) > .5f)
            {
                transform.position -= transform.right * 2 * Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator MultiplierReset()
    {
        while (true)
        {
            float counter = 0f;
            while (!multiplierIncreased)
            {
                counter += Time.deltaTime;
                yield return null;
            }
            if (counter >= multiplierResetInterval)
            {
                ResetMultiplier();
            }
            counter = 0f;
            yield return null;
        }
    }
}
