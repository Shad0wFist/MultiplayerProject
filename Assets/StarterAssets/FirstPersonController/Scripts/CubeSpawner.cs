using Unity.Netcode;
using UnityEngine;

public class CubeSpawner : NetworkBehaviour
{
    // Место для спавна кубов
    private Transform spawnPoint;

    // Префаб куба
    public GameObject cubePrefab;

    // Время между спавнами
    public float spawnInterval = 2f;

    // Массив материалов для случайного выбора
    public Material[] materials;

    private float spawnTimer;

    private void Start()
    {
        spawnPoint = transform;
    }

    private void Update()
    {
        // Если это сервер, обновляем таймер спавна
        if (IsServer)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;
                SpawnCube();
            }
        }
    }

    // Спавнит куб и синхронизирует его с клиентами
    private void SpawnCube()
    {
        // Создание куба на сервере
        GameObject cube = Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity);

        // Выбор случайного материала
        Material randomMaterial = materials[Random.Range(0, materials.Length)];
        cube.GetComponent<Renderer>().material = randomMaterial;

        // Сетевой объект для синхронизации на клиентах
        NetworkObject networkObject = cube.GetComponent<NetworkObject>();

        // Регистрация и спавн объекта на всех клиентах
        networkObject.Spawn();
    }
}
