using UnityEngine;

public class MainMenuState : MonoBehaviour
{
    public GameObject mainMenuUI; // UI главного меню

    private void OnEnable()
    {
        // Включаем UI главного меню
        mainMenuUI.SetActive(true);
        // Другие действия, например, разблокировка курсора
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        // Отключаем UI главного меню
        mainMenuUI.SetActive(false);
    }
}
