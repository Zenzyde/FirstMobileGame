using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSpawner : MonoBehaviour
{
    [SerializeField] private bool moveLeft;
    [SerializeField] private Bar bar;

    private bool pause;
    private float lastBarLength;

    void Start()
    {
        StartCoroutine(SpawnBar());
    }

    IEnumerator SpawnBar()
    {
        yield return new WaitForSeconds(UnityEngine.Random.value);
        while (true)
        {
            yield return new WaitUntil(() => !pause); //busywait
            yield return new WaitForSeconds(lastBarLength + UnityEngine.Random.Range(.2f, 1.6f));
            Instantiate(bar, transform.position, moveLeft ? Quaternion.Euler(new Vector3(0, 180, 0)) : Quaternion.identity).SetSpawner(this);
            pause = true;
        }
    }

    public void SignalContinue(float barLength)
    {
        lastBarLength = barLength;
        pause = false;
    }
}
