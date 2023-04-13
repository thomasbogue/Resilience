using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathSup
{
     public static float RandomNormal() {
          // adapted from code at https://stackoverflow.com/questions/5817490/implementing-box-mueller-random-number-generator-in-c-sharp
          float u, v, S;
          do {
              u = 2.0f * Random.value - 1.0f;
              v = 2.0f * Random.value - 1.0f;
              S = u * u + v * v;
          }
          while (S >= 1.0f);
          // v * Math.Sqrt(-2.0f * Math.Log(S) / S is also considered to be uncorrelated random normal variable.  I'm not sure I believe its uncorrelated.  Or if its uncorrelated, I'm not convinced its independent.
          return u * (float)Mathf.Sqrt(-2.0f * (float)Mathf.Log(S, 2.718281828f) / S);
     }
}
