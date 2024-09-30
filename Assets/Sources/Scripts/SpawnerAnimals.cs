using System.Collections;
using UnityEngine;
using Zenject;

public class SpawnerAnimals : MonoBehaviour
{
    [Inject] private readonly DiContainer _container;
    
    [SerializeField] private AnimalsData _animalsData;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnInterval = 5f;

    private void Awake()
    {
        StartCoroutine(SpawnAnimals());
    }

    private IEnumerator SpawnAnimals()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            SpawnAnimal();
        }
    }

    private void SpawnAnimal()
    {
        AnimalClick randomAnimal = _animalsData.GetRandomAnimal();
        Transform randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        _container.InstantiatePrefabForComponent<AnimalClick>(randomAnimal, randomSpawnPoint.position, Quaternion.Euler(0f, 90f, 0f), transform);
    }

}