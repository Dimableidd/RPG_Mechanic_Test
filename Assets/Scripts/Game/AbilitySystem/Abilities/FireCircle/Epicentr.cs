using UnityEngine;

public class Epicentr : MonoBehaviour
{

    public GameObject meteorPrefab;

    private float p_damage, p_duration, p_radius, p_interval, p_curDuration, p_curInterval;
    private int p_countPerWave;
    private Vector3 p_spawnPoint;

    public void Init(float _damage, float _duration, float _radius, float _interval, int _countPerWave, Vector3 _spawnPoint)
    {
        p_damage = _damage;
        p_duration = _duration;
        p_radius = _radius;
        p_interval = _interval;
        p_countPerWave = _countPerWave;
        p_spawnPoint = _spawnPoint;
    }

    private void Start()
    {
        transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
    }

    private void Update()
    {
        p_curInterval += Time.deltaTime;
        if(p_curInterval >= p_interval)
        {
            for(int i = 0; i < p_countPerWave; i++)
            {
                var _spawnPosition = new Vector3(Random.Range(p_spawnPoint.x - p_radius, p_spawnPoint.x + p_radius), 10.0f, Random.Range(p_spawnPoint.z - p_radius, p_spawnPoint.z + p_radius));
                var temp = Instantiate(meteorPrefab, _spawnPosition, Quaternion.identity).GetComponent<Meteor>();
                temp.Init(p_damage);
            }
            p_curInterval = 0.0f;
        }
        if(p_curDuration >= p_duration)
        {
            Destroy(gameObject);
        }
        p_curDuration += Time.deltaTime;
    }
}
