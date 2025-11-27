using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[System.Serializable]
public class SceneSettings
{
    [field: SerializeField] public Floater ShipPrefab { get; private set; }

    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField] public float RollSpeed { get; private set; }
    [field: SerializeField] public float ShipRotationSpeed { get; private set; }
    [field: SerializeField] public float SpeedForce { get; private set; }
    [field: SerializeField] public float FloatForce { get; private set; }
    [field: SerializeField] public float MaxSpeed { get; private set; }

    [field: SerializeField] public Transform Ship { get; private set; }
    [field: SerializeField] public Transform Parent { get; private set; }
    [field: SerializeField] public Transform Sail { get; private set; }
    [field: SerializeField] public Vector3 SailScale { get; private set; }
    [field: SerializeField] public Transform Wind { get; private set; }
    [field: SerializeField] public Vector3 FlagPosition { get; private set; }
    [field: SerializeField] public Vector3 FlagRotation { get; private set; }

    [field: SerializeField] public float MaxBodyTilt { get; private set; }

    [field: SerializeField] public float OffsetAboveWater { get; private set; }
    [field: SerializeField] public float Long { get; private set; }
    [field: SerializeField] public float OffsetX { get; private set; }
    [field: SerializeField] public float OffsetY { get; private set; }
    [field: SerializeField] public float Amplitude { get; private set; }

    [field: SerializeField] public float SailRotationSpeed { get; private set; }

    [field: SerializeField] public List<CinemachineVirtualCamera> Cameras { get; private set; }


    [field: SerializeField] public Transform WaveSpawnerPrefab { get; private set; }
    [field: SerializeField] public GameObject WavePrefab { get; private set; }

    [field: SerializeField] public float WavesFrequency { get; private set; }

    [field: SerializeField] public float MinWaveHeight { get; private set; }
    [field: SerializeField] public float MaxWaveHeight { get; private set; }

    [field: SerializeField] public float MinWaveWidth { get; private set; }
    [field: SerializeField] public float MaxWaveWidth { get; private set; }

    [field: SerializeField] public float WaveSpeed { get; private set; }
    [field: SerializeField] public float TimeToDestroyWave { get; private set; }


    [field: SerializeField] public Transform Flag { get; private set; }
    [field: SerializeField] public float SecondsToChangeWindDirection { get; private set; }
    [field: SerializeField] public float SpeedForChangeWindDirection { get; private set; }
}