using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace CDR.VFXSystem
{
    public static class VFXUtilities
    {
        public static IEnumerator LinearEaseIn(Action<float> easeEvent, float time)
        {
            float currentTime = 0;

            while(currentTime < time)
            {
                easeEvent?.Invoke(currentTime / time);

                currentTime += Time.deltaTime;

                yield return null;
            }

            easeEvent?.Invoke(1);
        }
        public static IEnumerator LinearEaseOut(Action<float> easeEvent, float time)
        {
            float currentTime = time;

            while(currentTime > 0)
            {
                easeEvent?.Invoke(currentTime / time);

                currentTime -= Time.deltaTime;

                yield return null;
            }

            easeEvent?.Invoke(1);
        }

        public static IEnumerator LinearEaseIn(Action<float> easeEvent, float time, Action onAfterEase)
        {
            yield return LinearEaseIn(easeEvent, time);

            onAfterEase?.Invoke();
        }

        public static IEnumerator LinearEaseOut(Action<float> easeEvent, float time, Action onAfterEase)
        {
            yield return LinearEaseOut(easeEvent, time);

            onAfterEase?.Invoke();
        }
    }
}