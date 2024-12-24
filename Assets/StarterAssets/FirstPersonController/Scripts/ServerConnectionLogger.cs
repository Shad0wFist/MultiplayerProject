using Unity.Netcode;
using UnityEngine;

public class ServerConnectionLogger : MonoBehaviour
{
    private void Start()
    {
        // Подписываемся на событие подключения клиента
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    private void OnDestroy()
    {
        // Отписываемся от событий, чтобы избежать утечек памяти
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log($"Клиент с ID {clientId} подключился к серверу.");
        }
    }

    private void OnClientDisconnected(ulong clientId)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log($"Клиент с ID {clientId} отключился от сервера.");
        }
    }
}
