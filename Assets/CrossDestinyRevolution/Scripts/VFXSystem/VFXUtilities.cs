using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace CDR.VFXSystem
{
    public static class VFXUtilities
    {
        public static IEnumerator LinearEaseIn(Action<float> easeEvent, Func<float> deltaTime, float time)
        {
            float currentTime = 0;

            while(currentTime < time)
            {
                float? dTime = deltaTime?.Invoke();
                
                if(!dTime.HasValue)
                    yield break;

                easeEvent?.Invoke(currentTime / time);

                currentTime += dTime.Value;

                yield return null;
            }

            easeEvent?.Invoke(1);
        }

        public static IEnumerator LinearEaseOut(Action<float> easeEvent, Func<float> deltaTime, float time)
        {
            float currentTime = time;

            while(currentTime > 0)
            {
                float? dTime = deltaTime?.Invoke();

                if(!dTime.HasValue)
                    yield break;

                easeEvent?.Invoke(currentTime / time);

                currentTime -= dTime.Value;

                yield return null;
            }

            easeEvent?.Invoke(0);
        }

        public static IEnumerator LinearEaseIn(Action<float> easeEvent, Func<float> deltaTime, float time, Action onAfterEase)
        {
            yield return LinearEaseIn(easeEvent, deltaTime, time);

            onAfterEase?.Invoke();
        }

        public static IEnumerator LinearEaseOut(Action<float> easeEvent, Func<float> deltaTime, float time, Action onAfterEase)
        {
            yield return LinearEaseOut(easeEvent, deltaTime, time);

            onAfterEase?.Invoke();
        }
    }
}