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
    [SerializeField] private StateController stateController; // Ссылка на контроллер состояний

    private void Start()
    {
        if (connectButton == null || networkManager == null || stateController == null)
        {
            Debug.LogError("ConnectButton: Не все поля настроены в инспекторе.");
            return;
        }

        connectButton.onClick.AddListener(ConnectToServer);

        // Подписываемся на событие отключения клиента
        networkManager.OnClientDisconnectCallback += OnClientDisconnected;

        // Подписываемся на событие подключения клиента
        networkManager.OnClientConnectedCallback += OnClientConnected;
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

        Debug.Log("Попытка подключения...");
        UpdateStatus("Попытка подключения...");
        networkManager.StartClient();
    }

    private void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.IsClient && clientId == NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("Подключение успешно.");
            UpdateStatus("Подключение успешно!");
            stateController.SwitchState(stateController.gamePlayState); // Переход в геймплейное состояние
        }
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
        if (networkManager != null)
        {
            networkManager.OnClientDisconnectCallback -= OnClientDisconnected;
            networkManager.OnClientConnectedCallback -= OnClientConnected;
        }
    }
}
