using UnityEngine;
using UnityEngine.UI;
using TMPro; // Для TextMeshPro
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class ConnectButton : MonoBehaviour
{
    [SerializeField] private Button connectButton; // Кнопка для подключения
    [SerializeField] private NetworkManager networkManager; // Ссылка на NetworkManager
    [SerializeField] private TextMeshProUGUI statusText; // Текст для вывода статуса подключения

    private void Start()
    {
        if (connectButton == null || networkManager == null)
        {
            Debug.LogError("ConnectButton: Не все поля настроены в инспекторе.");
            return;
        }

        connectButton.onClick.AddListener(ConnectToServer);

        // Подписываемся на событие отключения клиента
        networkManager.OnClientDisconnectCallback += OnClientDisconnected;
    }

    private void ConnectToServer()
    {
        var transport = networkManager.GetComponent<UnityTransport>();
        if (transport == null)
        {
            Debug.LogError("ConnectButton: UnityTransport не найден на NetworkManager.");
            UpdateStatus("Ошибка: транспорт не настроен.");
            return;
        }

        string serverAddress = transport.ConnectionData.Address;
        ushort serverPort = transport.ConnectionData.Port;

        Debug.Log($"Попытка подключения к серверу {serverAddress}:{serverPort}...");
        UpdateStatus("Попытка подключения...");

        networkManager.StartClient();
    }

    private void OnClientDisconnected(ulong clientId)
    {
        Debug.Log("Подключение не удалось или разорвано.");
        UpdateStatus("Не удалось подключиться к серверу.");
    }

    private void UpdateStatus(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
    }

    private void OnDestroy()
    {
        // Отписываемся от событий, чтобы избежать ошибок
        if (networkManager != null)
        {
            networkManager.OnClientDisconnectCallback -= OnClientDisconnected;
        }
    }
}
