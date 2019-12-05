using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{

    [SerializeField] private BarSpawner spawner;

    private bool move, hasGrown;
    private Renderer rend;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        float compareValue = UnityEngine.Random.Range(.5f, 2.5f);
        while (transform.localScale.x < compareValue)
        {
            Vector3 local = transform.localScale;
            local.x += Time.deltaTime;
            transform.localScale = local;
            yield return null;
        }
        move = true;
        hasGrown = true;
        spawner.SignalContinue(compareValue);
    }

    public void SetSpawner(BarSpawner spawner)
    {
        this.spawner = spawner;
    }

    void Update()
    {
        if (!IsVisible() && hasGrown)
        {
            Destroy(gameObject);
        }

        if (move)
        {
            transform.Translate(transform.InverseTransformDirection(transform.right) * Time.deltaTime, Space.Self);
        }
    }

    bool IsVisible()
    {
        return rend.isVisible;
    }
}