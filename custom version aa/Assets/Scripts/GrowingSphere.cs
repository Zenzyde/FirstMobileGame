using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingSphere : MonoBehaviour
{
    [SerializeField] private GameObject backgroundSphere;

    void Start()
    {
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        while (transform.localScale.x < backgroundSphere.transform.localScale.x)
        {
            Vector3 local = transform.localScale;
            local += Vector3.one * 0.5f * Time.deltaTime;
            transform.localScale = local;
            yield return null;
        }
        MenuManager.Instance.EndGame();
    }

    public void Shrink()
    {
        Vector3 local = transform.localScale;
        if (local.magnitude > 1)
        {
            local -= Vector3.one * 10 * Time.deltaTime;
        }
        transform.localScale = local;
        PinSpawner.Instance.UpdateScore(1);
    }
}