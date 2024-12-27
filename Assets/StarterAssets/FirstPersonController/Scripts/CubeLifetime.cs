using Unity.Netcode;
using UnityEngine;

public class CubeLifetime : NetworkBehaviour
{
    // Время жизни куба
    public float lifetime = 5f;

    private float timer;

    private void OnEnable()
    {
        // Сбрасываем таймер при активации куба
        timer = 0f;
    }

    private void Update()
    {
        // Если это сервер, то отслеживаем время жизни
        if (IsServer)
        {
            timer += Time.deltaTime;
            if (timer >= lifetime)
            {
                // Если время жизни истекло, уничтожаем куб
                DestroyCube();
            }
        }
    }

    // Уничтожение куба
    private void DestroyCube()
    {
        // Деструктируем объект на сервере
        if (IsServer)
        {
            NetworkObject networkObject = GetComponent<NetworkObject>();
            if (networkObject != null)
            {
                // Уничтожаем объект и удаляем его с всех клиентов
                networkObject.Despawn();
                Destroy(gameObject);
            }
        }
    }
}
