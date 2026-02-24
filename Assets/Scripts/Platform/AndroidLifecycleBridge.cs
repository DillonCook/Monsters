using UnityEngine;

namespace Monsters.Platform
{
    public sealed class AndroidLifecycleBridge : MonoBehaviour
    {
        private void OnApplicationPause(bool isPaused)
        {
            Debug.Log($"[AndroidLifecycleBridge] Pause changed: {isPaused}");
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Debug.Log($"[AndroidLifecycleBridge] Focus changed: {hasFocus}");
        }
    }
}
