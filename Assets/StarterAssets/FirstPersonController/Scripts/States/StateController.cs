using UnityEngine;

public class StateController : MonoBehaviour
{
    public GameObject currentState;  // Текущее состояние игры

    public GameObject mainMenuState; // Объект состояния главного меню
    public GameObject gamePlayState; // Объект состояния игры

    private void Start()
    {
        // Изначально активируем состояние главного меню
        SwitchState(mainMenuState);
    }

    // Метод для переключения состояния игры
    public void SwitchState(GameObject newState)
    {
        // Отключаем текущее состояние
        if (currentState != null)
        {
            currentState.SetActive(false);
        }

        // Активируем новое состояние
        newState.SetActive(true);

        // Обновляем текущее состояние
        currentState = newState;
    }
}
