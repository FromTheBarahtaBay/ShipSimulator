using UnityEngine;

public class WaveSpawner
{
    private Transform _spawner;

    private GameObject _wavePrefab;

    private float _wavesFrequency;

    private float _minWaveHeight;
    private float _maxWaveHeight;

    private float _minWaveWidth;
    private float _maxWaveWidth;

    private float _waveSpeed;
    private float _TimeToDestroyWave;

    private float _timer;

    public WaveSpawner(Transform spawner, GameObject wavePrefab, float wavesFrequency, float minWaveHeight, float maxWaveHeight,
        float minWaveWidth, float maxWaveWidth, float waveSpeed, float timeToDestroyWave) {
        _spawner = spawner;
        _wavePrefab = wavePrefab;
        _wavesFrequency = wavesFrequency;
        _minWaveHeight = minWaveHeight;
        _maxWaveHeight = maxWaveHeight;
        _minWaveWidth = minWaveWidth;
        _maxWaveWidth = maxWaveWidth;
        _waveSpeed = waveSpeed;
        _TimeToDestroyWave = timeToDestroyWave;
    }

    public void Spawn()
    {
        if (_timer > _wavesFrequency)
        {
            if (_wavePrefab == null)
                return;

            var wave = GameObject.Instantiate(_wavePrefab, _spawner.position, Quaternion.identity, _spawner);

            Vector3 waveSize = new Vector3(
                wave.transform.localScale.x,
                Random.Range(_minWaveHeight, _maxWaveHeight),
                Random.Range(_minWaveWidth, _maxWaveWidth));

            wave.transform.localScale = waveSize;
            wave.transform.rotation = _spawner.rotation;

            Wave waveComponent = wave.AddComponent<Wave>();
            waveComponent.Initialize(_waveSpeed, _TimeToDestroyWave);

            _timer = 0;
        }

        _timer += Time.deltaTime;
    }
}