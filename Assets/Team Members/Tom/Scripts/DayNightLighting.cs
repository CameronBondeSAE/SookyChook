using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class DayNightLighting : MonoBehaviour
    {
        public DayNightManager dayNight;

        void Update()
        {
            transform.eulerAngles = new Vector3(dayNight.currentTime * 15f - 90f,0,0);
        }
    }
}