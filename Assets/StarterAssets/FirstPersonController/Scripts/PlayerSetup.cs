using UnityEngine;
using Unity.Netcode;
using Unity.Cinemachine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] private Transform headTransform; // Ссылка на объект Head внутри PlayerCapsule

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner) // Проверяем, что это локальный игрок
        {
            SetupCamera();
        }
    }

    private void SetupCamera()
    {
        // Ищем PlayerFollowCamera
        CinemachineCamera virtualCamera = Object.FindFirstObjectByType<CinemachineCamera>();
        if (virtualCamera != null && headTransform != null)
        {
            // Устанавливаем Target для камеры
            virtualCamera.Follow = headTransform;
            Debug.Log("Камера настроена на локального игрока.");
        }
        else
        {
            Debug.LogWarning("Камера или Head не найдены!");
        }
    }
}
