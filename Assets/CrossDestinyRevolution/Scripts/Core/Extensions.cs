using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR
{
    public static class Extensions
    {
        // Source: https://blog.devgenius.io/calculating-the-area-under-an-animationcurve-in-unity-c43132a3abf8
        private static float IntegralOnStep(float x0, float y0, float x1, float y1)
        {
            float a = (y1 - y0) / (x1 - x0);
            float b = y0 - a * x0;
            return (a/2* x1*x1 + b*x1) - (a/2 * x0*x0 + b*x0);
        }

        // Source: https://blog.devgenius.io/calculating-the-area-under-an-animationcurve-in-unity-c43132a3abf8
        public static float GetArea(this AnimationCurve curve, float stepSize)
        {    
            float sum = 0;

            for (int i = 0; i < 1 / stepSize; i++)
            {
                sum += IntegralOnStep(
                    stepSize * i, 
                    curve.Evaluate(stepSize * i), 
                    stepSize * (i + 1), 
                    curve.Evaluate(stepSize * (i + 1))
                    );
            }

            return sum;
        }
    }
}