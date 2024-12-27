using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;
using Unity.Cinemachine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] private Transform headTransform; // Ссылка на объект Head внутри PlayerCapsule
    public string eyeTag = "Eyes"; // Тег для глаз игрока
    private GameObject player; // Объект игрока

    void Start()
    {
        // Получаем ссылку на текущего игрока
        player = gameObject;

        // Находим все объекты внутри игрока, которые имеют тег "PlayerEyes" и отключаем их MeshRenderer
        foreach (Transform child in player.transform)
        {
            if (child.CompareTag(eyeTag))
            {
                MeshRenderer renderer = child.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.enabled = false; // Отключаем отображение глаз
                }
            }
        }
    }

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
