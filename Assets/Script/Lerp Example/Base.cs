using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WhalePark18.LerpExample
{
    public abstract class Base : MonoBehaviour
    {
        public InputField inputA;
        public InputField inputB;
        public Text reultText;
        public Slider slider;

        protected abstract void Result();
    }
}
