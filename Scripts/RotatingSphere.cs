using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotatingSphere : MonoBehaviour
{

    private SphereEffect sphereEffect;
    private float speed, oscillatingSpeed, oscillatingMagnitude;
    [SerializeField] private TMP_Text text;
    private int displayedPoint, totalPoint;
    private bool canBePinned = true;
    private Color[] desiredColourForChange = new Color[] { };
    private int colorIndex = 0;

    void Awake()
    {
        text.text = displayedPoint.ToString();
        int i = Random.Range(0, 6);
        switch (i)
        {
            case 0:
                sphereEffect = SphereEffect.ColourChange;
                break;
            case 1:
                sphereEffect = SphereEffect.constantColourChange;
                break;
            case 2:
                sphereEffect = SphereEffect.grow;
                break;
            case 3:
                sphereEffect = SphereEffect.oscillateSize;
                break;
            case 4:
                sphereEffect = SphereEffect.shrink;
                break;
            case 5:
                sphereEffect = SphereEffect.none;
                break;
        }
    }

    void Start()
    {
        speed = GameSettingsManager.Instance.FixedSettings.SphereRotationSpeed;
        oscillatingSpeed = GameSettingsManager.Instance.FixedSettings.SphereOscillationSpeed;
        oscillatingMagnitude = GameSettingsManager.Instance.FixedSettings.SphereOscillationMagnitude;
        desiredColourForChange = GameSettingsManager.Instance.FixedSettings.SphereChangeColours;
        switch (sphereEffect)
        {
            case SphereEffect.ColourChange:
                totalPoint = displayedPoint = GameSettingsManager.Instance.FixedSettings.ColourStartPoint;
                break;
            case SphereEffect.grow:
                totalPoint = displayedPoint = GameSettingsManager.Instance.FixedSettings.GrowStartPoint;
                break;
            case SphereEffect.oscillateSize:
                totalPoint = displayedPoint = GameSettingsManager.Instance.FixedSettings.OscillateStartPoint;
                StartCoroutine(OscillateSize());
                break;
            case SphereEffect.shrink:
                totalPoint = displayedPoint = GameSettingsManager.Instance.FixedSettings.ShrinkStartPoint;
                break;
        }
    }

    void Update()
    {
        if (!MenuManager.Instance.GetIsPaused() && displayedPoint <= 0)
        {
            DestroyAndSpawnNewObject();
        }

        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
        text.text = displayedPoint.ToString();
        text.gameObject.transform.rotation = Quaternion.identity;
    }

    IEnumerator OscillateSize()
    {
        float i = 0f;
        float rate = .05f;
        Vector3 currentSize = (Vector2)transform.localScale;
        while (true)
        {
            while (i < 1f)
            {
                i += rate * Time.deltaTime;
                currentSize.x = Mathf.Abs(Mathf.Sin(Time.time * oscillatingSpeed)) + oscillatingMagnitude;
                currentSize.y = Mathf.Abs(Mathf.Sin(Time.time * oscillatingSpeed)) + oscillatingMagnitude;
                currentSize.z = Mathf.Abs(Mathf.Sin(Time.time * oscillatingSpeed)) + oscillatingMagnitude;
                transform.localScale = currentSize;
                yield return null;
            }
            i = 0f;
            yield return null;
        }
    }

    private void DestroyAndSpawnNewObject()
    {
        /*text.gameObject.SetActive(false);
        for (int i = 0; i < GetComponentsInChildren<SpriteRenderer>().Length; i++)
        {
            GetComponentsInChildren<SpriteRenderer>()[i].enabled = false;
            GetComponentsInChildren<Collider2D>()[i].enabled = false;
        }*/
        // GetComponent<SpriteRenderer>().enabled = false;
        // GetComponent<CircleCollider2D>().enabled = false;
        //StartCoroutine(pinSpawner.Instance.SpawnCircleOnDestroyed());
        //PinSpawner.speed++;
        //SphereTargetRotator.speed++;
        SphereTargetRotator.Instance.SpawnNewSphereTarget(transform.position);
        SphereTargetRotator.Instance.TotalPointsThisRound += totalPoint;
        PinSpawner.Instance.UpdateScore(totalPoint);
        Destroy(gameObject);
    }

    private IEnumerator Grow()
    {
        Vector3 currentSize = (Vector2)transform.localScale;
        currentSize += new Vector3(currentSize.x * 2f, currentSize.y * 2f, 0) * Time.deltaTime;
        currentSize.z = 1;
        transform.localScale = currentSize;
        // if (currentSize.magnitude >= GameSettingsManager.Instance.FixedSettings.MaxSphereSize.magnitude)
        // {
        //     StartCoroutine(DestroyAndSpawnNewObject());
        // }
        yield return null;
    }

    private IEnumerator Shrink()
    {
        Vector3 currentSize = (Vector2)transform.localScale;
        currentSize -= new Vector3(currentSize.x * 2f, currentSize.y * 2f, 0) * Time.deltaTime;
        currentSize.z = 1;
        transform.localScale = currentSize;
        // if (currentSize.magnitude <= GameSettingsManager.Instance.FixedSettings.MinSphereSize.magnitude)
        // {
        //     StartCoroutine(DestroyAndSpawnNewObject());
        // }
        yield return null;
    }

    private IEnumerator ColourChange()
    {
        Color c = GetComponentInChildren<MeshRenderer>().material.color;
        c = desiredColourForChange[colorIndex];
        c.a = 255;
        GetComponentInChildren<MeshRenderer>().material.color = c;
        yield return null;
    }

    public void CollisionUpdate()
    {
        displayedPoint--;
        if (Random.value > .7f)
        {
            speed *= -1;
        }
        switch (sphereEffect)
        {
            case SphereEffect.shrink:
                StartCoroutine(Shrink());
                return;
            case SphereEffect.grow:
                StartCoroutine(Grow());
                return;
            case SphereEffect.ColourChange:
                colorIndex = (colorIndex + 1) % GameSettingsManager.Instance.FixedSettings.SphereChangeColours.Length;
                StartCoroutine(ColourChange());
                return;
        }
    }
}

public enum SphereEffect
{
    shrink, grow, oscillateSize, constantColourChange, ColourChange, none
}