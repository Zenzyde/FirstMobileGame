using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fixed Settings", fileName = "New Fixed Settings")]
public class FixedSettingsCreator : ScriptableObject
{
    [SerializeField] private float sphereParentRotationSpeed, sphereRotationSpeed, sphereOscillationSpeed, sphereOscillationMagnitude, needleTravelSpeed;
    [SerializeField] private GameObject needle, sphere;
    [SerializeField] private Vector3 minSphereSize, maxSphereSize;
    [SerializeField] private Color[] sphereChangeColours;
    [SerializeField] private int growStartPoint, oscillateStartPoint, shrinkStartPoint, colourStartPoint;

    #region floats
    public float SphereParentRotationSpeed
    {
        get
        {
            return sphereParentRotationSpeed;
        }
    }
    public float NeedleTravelSpeed
    {
        get
        {
            return needleTravelSpeed;
        }
    }
    public float SphereRotationSpeed
    {
        get
        {
            return sphereRotationSpeed;
        }
    }
    public float SphereOscillationSpeed
    {
        get
        {
            return sphereOscillationSpeed;
        }
    }
    public float SphereOscillationMagnitude
    {
        get
        {
            return sphereOscillationMagnitude;
        }
    }
    #endregion

    public Color[] SphereChangeColours
    {
        get
        {
            return sphereChangeColours;
        }
    }

    public GameObject Needle
    {
        get
        {
            return needle;
        }
    }
    public GameObject Sphere
    {
        get
        {
            return sphere;
        }
    }

    public Vector3 MinSphereSize
    {
        get
        {
            return minSphereSize;
        }
    }
    public Vector3 MaxSphereSize
    {
        get
        {
            return maxSphereSize;
        }
    }

    public int GrowStartPoint { get { return growStartPoint; } }
    public int ShrinkStartPoint { get { return shrinkStartPoint; } }
    public int OscillateStartPoint { get { return oscillateStartPoint; } }
    public int ColourStartPoint { get { return colourStartPoint; } }
}