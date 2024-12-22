using UnityEngine;
using UnityEngine.UI;
using TMPro; // Добавьте эту строку
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Net.Sockets;

public class ConnectButton : MonoBehaviour
{
    [SerializeField] private Button connectButton; // Ссылка на кнопку
    [SerializeField] private NetworkManager networkManager; // Ссылка на NetworkManager
    [SerializeField] private StateController stateController; // Ссылка на StateController
    [SerializeField] private TextMeshProUGUI statusText;

    private void Start()
    {
        if (connectButton == null || networkManager == null || stateController == null)
        {
            Debug.LogError("ConnectButton: Не все поля настроены в инспекторе.");
            return;
        }

        connectButton.onClick.AddListener(TryConnectToServer);
    }

    private void TryConnectToServer()
    {
        var transport = networkManager.GetComponent<UnityTransport>();
        if (transport == null)
        {
            Debug.LogError("ConnectButton: UnityTransport не найден на NetworkManager.");
            return;
        }

        string serverAddress = transport.ConnectionData.Address;
        ushort serverPort = transport.ConnectionData.Port;

        // Проверяем доступность сервера
        if (IsServerAvailable(serverAddress, serverPort))
        {
            Debug.Log($"Подключение к серверу {serverAddress}:{serverPort}...");
            networkManager.StartClient();

            // Переключение на состояние геймплея
            stateController.SwitchState(stateController.gamePlayState);
            UpdateStatus("Подключено. Переключение на геймплей...");
        }
        else
        {
            Debug.Log($"Сервер {serverAddress}:{serverPort} недоступен.");
            UpdateStatus("Сервер недоступен.");
        }
    }

    private bool IsServerAvailable(string address, ushort port)
    {
        try
        {
            using (var client = new TcpClient())
            {
                var result = client.BeginConnect(address, port, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(1000); // Таймаут 1 секунда
                if (success)
                {
                    client.EndConnect(result);
                    return true; // Сервер доступен
                }
            }
        }
        catch
        {
            // Игнорируем исключения, связанные с невозможностью соединения
        }

        return false; // Сервер недоступен
    }

    private void UpdateStatus(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
    }
}
