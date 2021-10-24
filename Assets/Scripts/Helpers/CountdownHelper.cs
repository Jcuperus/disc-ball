using System;
using System.Collections;
using UnityEngine;

namespace Helpers
{
    public static class CountdownHelper
    {
        public static Action<int> OnCountChanged;
    
        public static IEnumerator CountDownCoroutine(int countdownAmount)
        {
            for (int i = countdownAmount; i > 0; i--)
            {
                OnCountChanged.Invoke(i);
                yield return new WaitForSeconds(1);
            }
            
            OnCountChanged.Invoke(0);
        }
    }
}