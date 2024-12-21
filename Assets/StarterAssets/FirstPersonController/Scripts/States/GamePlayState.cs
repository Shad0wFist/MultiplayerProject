using StarterAssets;
using UnityEngine;

public class GamePlayState : GameState
{
    private GameObject player;
    private FirstPersonController FPController;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.TryGetComponent<FirstPersonController>(out FPController);
        }
    }
    private void OnEnable()
    {
        // Здесь выключаем другие UI элементы (например, меню)
        Cursor.lockState = CursorLockMode.Locked;  // Блокируем курсор
        Cursor.visible = false;  // Скрываем курсор
        // Дополнительные действия для начала игры
        if (FPController != null)
            FPController.enabled = true;
    }

    private void OnDisable()
    {
        // Освобождаем курсор и делаем его видимым, когда игра завершена
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (FPController != null)
            FPController.enabled = false;
    }
}
