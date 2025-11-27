using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _wavePrefab;

    [Header("Wave Settings")]
    [SerializeField] private float _wavesFrequency;

    [SerializeField] private float _minWaveHeight;
    [SerializeField] private float _maxWaveHeight;

    [SerializeField] private float _minWaveWidth;
    [SerializeField] private float _maxWaveWidth;

    [SerializeField] private float _waveSpeed;
    [SerializeField] private float _TimeToDestroyWave;

    private float _timer;

    private void Update()
    {
        if (_wavePrefab == null)
            return;

        if (_timer > _wavesFrequency)
        {
            var wave = GameObject.Instantiate(_wavePrefab, transform.position, Quaternion.identity, transform);
            Vector3 waveSize = new Vector3(wave.transform.localScale.x, Random.Range(_minWaveHeight, _maxWaveHeight), Random.Range(_minWaveWidth, _maxWaveWidth));
            wave.transform.localScale = waveSize;
            wave.transform.rotation = transform.rotation;

            Wave waveComponent = wave.AddComponent<Wave>();
            waveComponent.Initialize(_waveSpeed, _TimeToDestroyWave);

            _timer = 0;
        }

        _timer += Time.deltaTime;
    }
}