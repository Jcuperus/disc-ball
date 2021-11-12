using System;
using System.Collections;
using UnityEngine;

namespace Helpers
{
    public static class CoroutineExtensions
    {
        public static Coroutine DelayedAction(this MonoBehaviour monoBehaviour, Action action, float duration)
        {
            return monoBehaviour.StartCoroutine(DelayedActionCoroutine(action, duration));
        }

        private static IEnumerator DelayedActionCoroutine(Action action, float duration)
        {
            yield return new WaitForSeconds(duration);

            action();
        }
    }
}