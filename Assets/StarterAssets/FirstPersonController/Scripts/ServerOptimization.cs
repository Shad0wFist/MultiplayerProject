using UnityEngine;

public class ServerOptimization : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_SERVER || UNITY_EDITOR
        Debug.Log("Testing optimization in the Editor.");

        // Отключаем все рендеры
        foreach (var renderer in Object.FindObjectsByType<Renderer>(FindObjectsSortMode.None))
        {
            renderer.enabled = false;
        }

        // Отключаем все эффекты
        foreach (var particleSystem in Object.FindObjectsByType<ParticleSystem>(FindObjectsSortMode.None))
        {
            particleSystem.Stop();
        }

        Debug.Log("Server optimization applied: Renderers and effects disabled.");
#endif
    }
}
