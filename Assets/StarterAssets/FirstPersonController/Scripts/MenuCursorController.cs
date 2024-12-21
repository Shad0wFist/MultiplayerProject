using UnityEngine;

public class MenuCursorController : MonoBehaviour
{
    private StarterAssets.StarterAssetsInputs playerInput; // Ссылка на StarterAssetsInputs
    private GameObject player; // Ссылка на игрока // Ссылка на StarterAssetsInputs

    private void Start()
    {
        // Находим игрока по тегу и получаем компонент StarterAssetsInputs
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInput = player.GetComponent<StarterAssets.StarterAssetsInputs>(); // Получаем компонент на игроке
        }
    }

    private void OnEnable()
    {
        //if (playerInput != null)
            //playerInput.ShowMenu();
    }

    private void OnDisable()
    {
        //if (playerInput != null)
            //playerInput.HideMenu();
    }
}
