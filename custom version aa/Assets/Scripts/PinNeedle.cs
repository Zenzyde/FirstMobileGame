using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinNeedle : MonoBehaviour
{
    public float speed;
    public Rigidbody rigid;
    bool isPinned = false, spawnedNewPin = false, released = false;
    [SerializeField] private Renderer pin, needle;

    void Start()
    {
        speed = GameSettingsManager.Instance.FixedSettings.NeedleTravelSpeed;
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        // {

        // }
        if (isPinned) return;
        if (!MenuManager.Instance.GetIsPaused())
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                transform.SetParent(null);
                released = true;
                PinSpawner.Instance.RemoveActivePin(this);
                if (spawnedNewPin) return;
                PinSpawner.Instance.SpawnPin();
                spawnedNewPin = true;
            }
        }
        //if (MenuManager.Instance.GetIsPaused()) return;
        if (!isPinned)
        {
            if (released)
            {
                rigid.MovePosition(rigid.position + rigid.transform.up * speed * Time.deltaTime);
            }
        }
        if (!IsVisible())
        {
            if (!spawnedNewPin)
            {
                PinSpawner.Instance.SpawnPin();
            }
            Destroy(gameObject);
        }
    }

    bool IsVisible()
    {
        return pin.isVisible || needle.isVisible;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject parent = other.transform.parent.gameObject;
        // if (parent.GetComponent<RotatingSphere>())
        // {
        //     //Set is pinned & update score
        //     isPinned = true;

        //     //Switch direction
        //     if (Random.value > .7f)
        //     {
        //         //PinSpawner.speed *= -1;
        //         SphereTargetRotator.speed *= -1;
        //     }

        //     //Set rotation & parenting
        //     float z = Mathf.Atan2((other.transform.position.y - transform.position.y), (other.transform.position.x - transform.position.x)) * Mathf.Rad2Deg;
        //     transform.rotation = Quaternion.AngleAxis((z - 90), Vector3.forward);

        //     transform.SetParent(parent.transform);

        //     //Activate additional effects
        //     parent.GetComponent<RotatingSphere>().CollisionUpdate();
        // }

        if (parent.GetComponent<GrowingSphere>())
        {
            for (int i = 0; i < PinSpawner.Instance.GetMultiplier(); i++)
            {
                parent.GetComponent<GrowingSphere>().Shrink();
            }
            PinSpawner.Instance.IncreaseMultiplier();
            Destroy(gameObject);
        }

        if (parent.GetComponent<Bar>())
        {
            PinSpawner.Instance.ResetMultiplier();
            Destroy(gameObject);
        }

        // if (parent.GetComponent<PinNeedle>() && isPinned)
        // {
        //     parent.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        //     MenuManager.Instance.Restart();//SetMenuPausedState(true);
        // }
    }
}