using UnityEngine;

public class LogRedirector : MonoBehaviour
{
    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Здесь вы можете отправить логи на сервер
        Debug.Log($"Лог: {logString}\n{stackTrace}");
    }
}
