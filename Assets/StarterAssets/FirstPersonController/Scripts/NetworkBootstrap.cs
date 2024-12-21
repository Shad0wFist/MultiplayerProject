using Unity.Netcode;
using UnityEngine;

public class NetworkBootstrap : MonoBehaviour
{
    void Start()
    {
        #if UNITY_SERVER
        // Серверная сборка: запускаем сервер автоматически
        Debug.Log("Сервер запущен.");
        NetworkManager.Singleton.StartServer();
        #else
        // Клиентская сборка: ждем пользовательского ввода
        Debug.Log("Клиентская сборка запущена.");
        #endif
    }

    public void StartHost()
    {
        Debug.Log("Хост запущен.");
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        Debug.Log("Клиент подключён.");
        NetworkManager.Singleton.StartClient();
    }
}
