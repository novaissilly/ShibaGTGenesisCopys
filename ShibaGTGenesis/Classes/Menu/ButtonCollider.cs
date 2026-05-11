using System;
using UnityEngine;

namespace ShibaGTGenesis.Classes
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ButtonCollider : MonoBehaviour
    {
        public ButtonCollider(IntPtr ptr) : base(ptr) { }
        public string relatedText;

        public static float buttonCooldown = 0f;

        public void OnTriggerEnter(Collider collider)
        {
            if (Time.time > buttonCooldown && collider == Menu.Menu.Instance.referencecollider && Menu.Menu.Instance.menu != null)
            {
                buttonCooldown = Time.time + 0.2f;
                Menu.Menu.Instance.Toggle(relatedText);
            }
        }
    }
}