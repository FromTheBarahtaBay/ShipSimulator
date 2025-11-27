using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field: SerializeField] public SceneSettings SceneSettings { get; private set; }

    private CameraViewChanger _cameraViewChanger;
    private WaveSpawner _waveSpawner;
    private InputSystem _inputSystem;
    private WindSystemController _windSystemController;

    private void Awake()
    {
        _inputSystem = new InputSystem();

        GameObject wavespawner = GameObject.Instantiate(SceneSettings.WaveSpawnerPrefab.gameObject);
        _waveSpawner = new WaveSpawner(wavespawner.transform, SceneSettings.WavePrefab,
            SceneSettings.WavesFrequency, SceneSettings.MinWaveHeight, SceneSettings.MaxWaveHeight, SceneSettings.MinWaveHeight, SceneSettings.MaxWaveWidth, SceneSettings.WaveSpeed, SceneSettings.TimeToDestroyWave);

        Floater floater = FloaterSpawn();
        Transform sail = SailCreate(floater);
        Transform flag = FlagCreate(floater);

        SetBehaviours(floater, sail, flag);

        _windSystemController = new WindSystemController(flag.transform,
            SceneSettings.SecondsToChangeWindDirection,
            SceneSettings.SpeedForChangeWindDirection);

        _cameraViewChanger = new CameraViewChanger(SceneSettings.Cameras, floater.transform);
    }

    private void Update()
    {
        _waveSpawner.Spawn();
        _windSystemController.Blow();

        if (_inputSystem.IsFKeyDown())
            _cameraViewChanger.ChangeView();
    }

    private Floater FloaterSpawn()
    {
        Floater floater = GameObject.Instantiate(SceneSettings.ShipPrefab);

        return floater;
    }

    private Transform SailCreate(Floater floater)
    {
        var sail = GameObject.Instantiate(SceneSettings.Sail, floater.transform);
        sail.localScale = SceneSettings.SailScale;

        return sail;
    }

    private Transform FlagCreate(Floater floater)
    {
        var flag = GameObject.Instantiate(SceneSettings.Flag, floater.transform);
        flag.localPosition = SceneSettings.FlagPosition;
        flag.localRotation = Quaternion.Euler(SceneSettings.FlagRotation);

        return flag;
    }

    private void SetBehaviours(Floater floater, Transform sail, Transform flag)
    {
        if (floater.TryGetComponent<Rigidbody>(out Rigidbody rigidbody)) {
            PhysicRotator physicRotator = new PhysicRotator(rigidbody);

            var behaviours = CreateBehaviours(rigidbody, physicRotator, floater, sail, flag);

            floater.Initialize(_inputSystem, SceneSettings, behaviours.ToArray());
        }
    }

    private List<IBehaviour> CreateBehaviours(Rigidbody rigidbody, PhysicRotator physicRotator, Floater floater, Transform sail, Transform flag)
    {
        List<IBehaviour> behaviours = new List<IBehaviour>();

        var sailingShipMovingBehaviour = new SailingShipMovingBehaviour(rigidbody, SceneSettings.SpeedForce,
               floater.transform, sail, flag);
        behaviours.Add(sailingShipMovingBehaviour);

        var floatingBehaviour = new FloatingBehaviour(rigidbody, SceneSettings.FloatForce, SceneSettings.OffsetAboveWater, SceneSettings.Long, SceneSettings.OffsetX, SceneSettings.Amplitude, SceneSettings.OffsetY);
        behaviours.Add(floatingBehaviour);

        var sailBehaviour = new SailBehaviour(SceneSettings.SailRotationSpeed, floater.transform, sail);
        behaviours.Add(sailBehaviour);

        var alignWithNormalBehaviour = new AlignWithNormalBehaviour(rigidbody, physicRotator, SceneSettings.RollSpeed);
        behaviours.Add(alignWithNormalBehaviour);

        var shipRotationBehaviour = new RotationBehaviour(rigidbody, physicRotator, SceneSettings.ShipRotationSpeed);
        behaviours.Add(shipRotationBehaviour);

        var tiltingBehaviour = new TiltingBehaviour(rigidbody, physicRotator, SceneSettings.MaxBodyTilt);
        behaviours.Add(tiltingBehaviour);

        return behaviours;
    }
}