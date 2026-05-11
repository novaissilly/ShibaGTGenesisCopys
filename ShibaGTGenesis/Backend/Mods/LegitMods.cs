using easyInputs;
using System.Reflection;
using UnityEngine;

namespace ShibaGTGenesis
{
    public class LegitMods
    {
        public static void SteamArms()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
        }

        public static void DisableSteamArms()
        {
            if (Menu.Menu.Instance.GetButton("Really Long Arms").enabled == false)
                GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public static void HZ()
        {
            foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType<GameObject>())
            { }
        }

        public static void SlideControl()
        {
            GorillaLocomotion.Player.Instance.slideControl = 1f;
        }

        public static void OFFSlideControl()
        {
            GorillaLocomotion.Player.Instance.slideControl = 0.00425f;
        }

        public static float LagFloat;
        static bool LagBool;
        public static void FakeLag()
        {
            if (LagFloat < Time.time)
            {
                LagFloat = Time.time + 0.2f;
                int randomint = UnityEngine.Random.Range(1, 3);
                if (randomint == 1)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
                LagBool = true;
            }
        }


        public static void FakeOculus()
        {
            if (EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand))
            {
                GameObject.Find("LeftHand Controller").SetActive(false);
                GameObject.Find("RightHand Controller").SetActive(false);
            }
            else
            {
                GameObject.Find("LeftHand Controller").SetActive(true);
                GameObject.Find("RightHand Controller").SetActive(true);
            }
        }


        private static Vector3 walkPos;
        private static Vector3 walkNormal;
        public static void WallWalk()
        {
            if ((GorillaLocomotion.Player.Instance.IsHandTouching(true) || GorillaLocomotion.Player.Instance.IsHandTouching(false)) && EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
            {
                FieldInfo fieldInfo = typeof(GorillaLocomotion.Player).GetField("lastHitInfoHand", BindingFlags.NonPublic | BindingFlags.Instance);
                RaycastHit ray = (RaycastHit)fieldInfo.GetValue(GorillaLocomotion.Player.Instance);
                walkPos = ray.point;
                walkNormal = ray.normal;
            }

            if (!EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
            {
                walkPos = Vector3.zero;
            }

            if (walkPos != Vector3.zero)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(walkNormal * -9.81f, ForceMode.Acceleration);
            }
        }
    }
}