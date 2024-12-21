using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    public StateController stateController;
    // Метод для активации состояния
    public virtual void OnEnableState()
    {
        gameObject.SetActive(true);  // Активируем объект состояния
    }

    // Метод для деактивации состояния
    public virtual void OnDisableState()
    {
        gameObject.SetActive(false);  // Деактивируем объект состояния
    }
}
