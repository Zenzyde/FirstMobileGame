using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTargetRotator : MonoBehaviour
{
    public static float speed;
    [SerializeField] private GameObject sphereTarget;
    public static SphereTargetRotator Instance
    {
        get
        {
            return instance;
        }
    }

    private static SphereTargetRotator instance;
    private int totalPointsThisRound;
    public int TotalPointsThisRound
    {
        get
        {
            return totalPointsThisRound;
        }
        set
        {
            if (value < 0) return;
            totalPointsThisRound = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        speed = GameSettingsManager.Instance.FixedSettings.SphereParentRotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuManager.Instance.GetIsPaused()) return;

        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    public void SpawnNewSphereTarget(Vector3 position)
    {
        Instantiate(sphereTarget, position, Quaternion.identity, transform);
    }
}
