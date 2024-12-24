using TMPro;
using UnityEngine;

public class ClientLogger : MonoBehaviour
{
    [SerializeField]
    private TMP_Text logText; // Ссылка на TextMeshPro для вывода логов

    [SerializeField]
    private int maxLines = 20; // Максимальное количество строк в логе

    private string logCache = ""; // Хранение текста лога

    private void Awake()
    {
        // Подписываемся на события логирования
        Application.logMessageReceived += HandleLog;
    }

    private void OnDestroy()
    {
        // Отписываемся от событий логирования
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        string logEntry;
        switch (type)
        {
            case LogType.Error:
            case LogType.Exception:
                logEntry = $"<color=red>[Error]</color> {logString}\n";
                break;
            case LogType.Warning:
                logEntry = $"<color=yellow>[Warning]</color> {logString}\n";
                break;
            default:
                logEntry = $"<color=white>[Log]</color> {logString}\n";
                break;
        }


        // Добавляем в кэш
        logCache += logEntry;

        // Удаляем старые строки, если их стало слишком много
        string[] lines = logCache.Split('\n');
        if (lines.Length > maxLines)
        {
            logCache = string.Join("\n", lines, lines.Length - maxLines, maxLines);
        }

        // Обновляем текст в TextMeshPro
        if (logText != null)
        {
            logText.text = logCache;
        }
    }
}
