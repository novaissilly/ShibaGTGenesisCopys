using easyInputs;
using Il2CppSystem;
using Photon.Pun;
using ShibaGTGenesis.Classes;
using UnityEngine;

namespace ShibaGTGenesis
{
    public class RigMods
    {
        public static void GhostMonkey()
        {
            if (EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand))
            {
                if (PhotonNetwork.InRoom)
                    GorillaTagger.Instance.myVRRig.enabled = false;
                else
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
            }
            else
            {
                if (PhotonNetwork.InRoom)
                    GorillaTagger.Instance.myVRRig.enabled = true;
                else
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static void ResetRig()
        {
            if (PhotonNetwork.InRoom)
                GorillaTagger.Instance.myVRRig.enabled = true;
            else
                GorillaTagger.Instance.offlineVRRig.enabled = true;
        }

        public static Photon.Realtime.Player lucyTarget;

        public static float lucyspeed = 0.1f;

        private static bool Spin;
        private static bool Roll;

        public static void HeadSpin()
        {
            Vector3 rot = GorillaTagger.Instance.myVRRig.head.trackingRotationOffset;
            rot.y += 15f;
            GorillaTagger.Instance.myVRRig.head.trackingRotationOffset = rot;

            Spin = true;
        }

        public static void OffHeadSpin()
        {
            if (Spin)
            {
                Spin = false;

                Vector3 rot = GorillaTagger.Instance.myVRRig.head.trackingRotationOffset;
                rot.y = 0f;
                GorillaTagger.Instance.myVRRig.head.trackingRotationOffset = rot;
            }
        }

        public static void HeadRoll()
        {
            Vector3 rot = GorillaTagger.Instance.myVRRig.head.trackingRotationOffset;
            rot.x += 15f;
            GorillaTagger.Instance.myVRRig.head.trackingRotationOffset = rot;

            Roll = true;
        }

        public static void SpazMonk()
        {
            System.Random random = new System.Random();
            if (PhotonNetwork.InRoom)
            {
                GorillaTagger.Instance.myVRRig.head.rigTarget.eulerAngles = new Vector3(random.Next(0, 360), random.Next(0, 360), random.Next(0, 360));
                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.eulerAngles = new Vector3(random.Next(0, 360), random.Next(0, 360), random.Next(0, 360));
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.eulerAngles = new Vector3(random.Next(0, 360), random.Next(0, 360), random.Next(0, 360));
            }
        }

        static bool upside;
        static bool back;

        public static void OFFHeadBackwards()
        {
            if (back)
            {
                back = false;

                Vector3 rot = GorillaTagger.Instance.myVRRig.head.trackingRotationOffset;
                rot.y = 0f;
                GorillaTagger.Instance.myVRRig.head.trackingRotationOffset = rot;
            }
        }

        public static void HeadBackwards()
        {
            if (!back)
            {
                Vector3 rot = GorillaTagger.Instance.myVRRig.head.trackingRotationOffset;
                rot.y = 180f;
                GorillaTagger.Instance.myVRRig.head.trackingRotationOffset = rot;

                back = true;
            }
        }

        public static void HeadUpsidedown()
        {
            if (!upside)
            {
                Vector3 rot = GorillaTagger.Instance.myVRRig.head.trackingRotationOffset;
                rot.x = 180f;
                rot.y = 180f;
                GorillaTagger.Instance.myVRRig.head.trackingRotationOffset = rot;

                upside = true;
            }
        }

        public static void OFFHeadUpsidedown()
        {
            if (upside)
            {
                upside = false;

                Vector3 rot = GorillaTagger.Instance.myVRRig.head.trackingRotationOffset;
                rot.x = 0f;
                rot.y = 0f;
                GorillaTagger.Instance.myVRRig.head.trackingRotationOffset = rot;
            }
        }

        public static void HeadSpaz()
        {
            System.Random random = new System.Random();
            if (PhotonNetwork.InRoom)
            {
                GorillaTagger.Instance.myVRRig.head.rigTarget.eulerAngles = new Vector3(random.Next(0, 360), random.Next(0, 360), random.Next(0, 360));
            }
        }

        public static void HandSpaz()
        {
            System.Random random = new System.Random();
            if (PhotonNetwork.InRoom)
            {
                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.eulerAngles = new Vector3(random.Next(0, 360), random.Next(0, 360), random.Next(0, 360));
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.eulerAngles = new Vector3(random.Next(0, 360), random.Next(0, 360), random.Next(0, 360));
            }
        }

        public static void OffHeadRoll()
        {
            if (Roll)
            {
                Roll = false;
                Vector3 rot = GorillaTagger.Instance.myVRRig.head.trackingRotationOffset;
                rot.x = 0f;
                GorillaTagger.Instance.myVRRig.head.trackingRotationOffset = rot;
            }
        }


        public static void SpinBot()
        {
            if (EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
            {
                GorillaTagger.Instance.myVRRig.enabled = false;
                GorillaTagger.Instance.myVRRig.transform.position = GorillaLocomotion.Player.Instance.bodyCollider.transform.position + new Vector3(0, 0.3f, 0);
                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.myVRRig.transform.position + GorillaTagger.Instance.myVRRig.transform.right * -1f;
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.myVRRig.transform.position + GorillaTagger.Instance.myVRRig.transform.right * 1f;
                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.rotation = GorillaTagger.Instance.myVRRig.transform.rotation;
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.rotation = GorillaTagger.Instance.myVRRig.transform.rotation;
                GorillaTagger.Instance.myVRRig.transform.rotation = Quaternion.Euler(GorillaTagger.Instance.myVRRig.transform.rotation.eulerAngles + new Vector3(4f, 7f, 0f));
            }
            else {
                GorillaTagger.Instance.myVRRig.enabled = true;
            }
        }

        public static void LucyGun()
        {
            if (Menu.Menu.GetGunInput(false))
            {
                var GunData = Menu.Menu.RenderGun();
                GameObject Pointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Menu.Menu.lockTarget && Menu.Menu.gunLocked)
                {
                    VRRig lucyTargetRig = Menu.Menu.lockTarget;
                    GorillaTagger.Instance.myVRRig.enabled = false;
                    GorillaTagger.Instance.myVRRig.transform.position += GorillaTagger.Instance.myVRRig.transform.forward * lucyspeed * Time.deltaTime;
                    GorillaTagger.Instance.myVRRig.transform.LookAt(lucyTargetRig.transform);
                    lucyspeed += 0.005f;
                }

                if (Menu.Menu.GetGunInput(true))
                {
                    VRRig rig = Ray.collider.GetComponentInParent<VRRig>();
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        Menu.Menu.lockTarget = rig;
                        Menu.Menu.gunLocked = true;
                    }
                }
                else
                {
                    GorillaTagger.Instance.myVRRig.enabled = true;
                    lucyspeed = 0.1f;
                }
            }
            else
            {
                Menu.Menu.lockTarget = null;
                Menu.Menu.gunLocked = false;
            }
        }

        public static void LucyRandom()
        {
            if (EasyInputs.GetGripButtonDown(EasyHand.RightHand))
            {
                if (lucyTarget == null)
                {
                    lucyTarget = RigManager.GetRandomPlayer(false);
                }
                VRRig lucyTargetRig = RigManager.GetVRRigFromPlayer(lucyTarget);
                GorillaTagger.Instance.myVRRig.enabled = false;
                GorillaTagger.Instance.myVRRig.transform.position += GorillaTagger.Instance.myVRRig.transform.forward * lucyspeed * Time.deltaTime;
                GorillaTagger.Instance.myVRRig.transform.LookAt(lucyTargetRig.transform);
                lucyspeed += 0.005f;
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                lucyspeed = 0.1f;
            }
        }


        public static void RigGun()
        {
            if (Menu.Menu.GetGunInput(false))
            {
                var GunData = Menu.Menu.RenderGun();
                GameObject Pointer = GunData.Pointer;
                if (Menu.Menu.GetGunInput(true))
                {
                    if (PhotonNetwork.InRoom)
                    {
                        GorillaTagger.Instance.myVRRig.enabled = false;
                        GorillaTagger.Instance.myVRRig.transform.position = Pointer.transform.position;
                    }
                    else
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                        GorillaTagger.Instance.offlineVRRig.transform.position = Pointer.transform.position;
                    }
                }
                else
                {
                    if (PhotonNetwork.InRoom)
                        GorillaTagger.Instance.myVRRig.enabled = true;
                    else
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }

        public static void FollowPlayerGun()
        {
            if (Menu.Menu.GetGunInput(false))
            {
                var GunData = Menu.Menu.RenderGun();
                GameObject Pointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Menu.Menu.lockTarget && Menu.Menu.gunLocked)
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;
                    Vector3 direction = (GorillaTagger.Instance.myVRRig.transform.position - Menu.Menu.lockTarget.headConstraint.transform.position).normalized;
                    Vector3 newPosition = Menu.Menu.lockTarget.headConstraint.transform.position + direction * 2f;
                    GorillaTagger.Instance.myVRRig.transform.LookAt(Menu.Menu.lockTarget.headConstraint.transform.position);
                    GorillaTagger.Instance.myVRRig.transform.position = newPosition;
                    System.Random random = new System.Random();
                    if (PhotonNetwork.InRoom)
                    {
                        GorillaTagger.Instance.myVRRig.head.rigTarget.eulerAngles = new Vector3(random.Next(0, 360), random.Next(0, 360), random.Next(0, 360));
                        GorillaTagger.Instance.myVRRig.leftHand.rigTarget.eulerAngles = new Vector3(random.Next(0, 360), random.Next(0, 360), random.Next(0, 360));
                        GorillaTagger.Instance.myVRRig.rightHand.rigTarget.eulerAngles = new Vector3(random.Next(0, 360), random.Next(0, 360), random.Next(0, 360));
                    }
                }

                if (Menu.Menu.GetGunInput(true))
                {
                    VRRig rig = Ray.collider.GetComponentInParent<VRRig>();
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        Menu.Menu.lockTarget = rig;
                        Menu.Menu.gunLocked = true;
                    }
                }
                else
                {
                    GorillaTagger.Instance.myVRRig.enabled = true;
                }
            }
            else
            {
                Menu.Menu.lockTarget = null;
                Menu.Menu.gunLocked = false;
            }
        }

        public static void CopyGun()
        {
            if (Menu.Menu.GetGunInput(false))
            {
                var GunData = Menu.Menu.RenderGun();
                GameObject Pointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Menu.Menu.lockTarget && Menu.Menu.gunLocked)
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;
                    GorillaTagger.Instance.myVRRig.transform.position = Menu.Menu.lockTarget.transform.position;
                    GorillaTagger.Instance.myVRRig.transform.rotation = Menu.Menu.lockTarget.transform.rotation;
                    GorillaTagger.Instance.myVRRig.rightHandPlayer.transform.position = Menu.Menu.lockTarget.rightHandPlayer.transform.position;
                    GorillaTagger.Instance.myVRRig.rightHandPlayer.transform.rotation = Menu.Menu.lockTarget.rightHandPlayer.transform.rotation;
                    GorillaTagger.Instance.myVRRig.leftHandPlayer.transform.position = Menu.Menu.lockTarget.leftHandPlayer.transform.position;
                    GorillaTagger.Instance.myVRRig.leftHandPlayer.transform.rotation = Menu.Menu.lockTarget.leftHandPlayer.transform.rotation;
                    GorillaTagger.Instance.myVRRig.head.headTransform.transform.rotation = Menu.Menu.lockTarget.head.headTransform.transform.rotation;
                    GorillaTagger.Instance.myVRRig.head.headTransform.transform.position = Menu.Menu.lockTarget.head.headTransform.transform.position;
                    GorillaTagger.Instance.offlineVRRig.headConstraint.rotation = Menu.Menu.lockTarget.headConstraint.rotation;
                }

                if (Menu.Menu.GetGunInput(true))
                {
                    VRRig rig = Ray.collider.GetComponentInParent<VRRig>();
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        Menu.Menu.lockTarget = rig;
                        Menu.Menu.gunLocked = true;
                    }
                }
                else
                {
                    GorillaTagger.Instance.myVRRig.enabled = true;
                    GorillaTagger.Instance.myVRRig.headConstraint.rotation = GorillaLocomotion.Player.Instance.headCollider.transform.rotation;

                }
            }
            else
            {
                Menu.Menu.lockTarget = null;
                Menu.Menu.gunLocked = false;
            }
        }

        public static void SexGun()
        {
            if (Menu.Menu.GetGunInput(false))
            {
                var GunData = Menu.Menu.RenderGun();
                GameObject Pointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;

                if (Menu.Menu.lockTarget && Menu.Menu.gunLocked)
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;
                    GorillaTagger.Instance.myVRRig.transform.position = Menu.Menu.lockTarget.transform.position + (Menu.Menu.lockTarget.transform.forward * -(0.2f + (Mathf.Sin(Time.frameCount / 8f) * 0.1f)));
                    GorillaTagger.Instance.myVRRig.transform.rotation = Menu.Menu.lockTarget.transform.rotation;
                    GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = (Menu.Menu.lockTarget.transform.position + Menu.Menu.lockTarget.transform.right * -0.2f) + Menu.Menu.lockTarget.transform.up * -0.4f;
                    GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = (Menu.Menu.lockTarget.transform.position + Menu.Menu.lockTarget.transform.right * 0.2f) + Menu.Menu.lockTarget.transform.up * -0.4f;
                    GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.rotation = Menu.Menu.lockTarget.transform.rotation;
                    GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.rotation = Menu.Menu.lockTarget.transform.rotation;
                    GorillaTagger.Instance.myVRRig.head.rigTarget.transform.rotation = Menu.Menu.lockTarget.transform.rotation;

                }

                if (Menu.Menu.GetGunInput(true))
                {
                    VRRig rig = Ray.collider.GetComponentInParent<VRRig>();
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        Menu.Menu.lockTarget = rig;
                        Menu.Menu.gunLocked = true;
                    }
                }
                else
                {
                    GorillaTagger.Instance.myVRRig.enabled = true;
                }
            }
            else
            {
                Menu.Menu.lockTarget = null;
                Menu.Menu.gunLocked = false;
            }
        }

        public static void HoldRig()
        {
            if (EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
            {
                GorillaTagger.Instance.myVRRig.enabled = false;
                GorillaTagger.Instance.myVRRig.transform.position = GorillaLocomotion.Player.Instance.leftHandFollower.position;
            }
            if (EasyInputs.GetGripButtonDown(EasyHand.RightHand))
            {
                GorillaTagger.Instance.myVRRig.enabled = false;
                GorillaTagger.Instance.myVRRig.transform.position = GorillaLocomotion.Player.Instance.rightHandFollower.position;
            }
            if (!EasyInputs.GetGripButtonDown(EasyHand.RightHand) && !EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
            }
        }


        public static void InvisMonkey()
        {
            if (EasyInputs.GetTriggerButtonDown(EasyHand.LeftHand))
            {
                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;
                    GorillaTagger.Instance.myVRRig.transform.position = new Vector3(435345f, 3323f, 32432f);
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(435345f, 3323f, 32432f);
                }
            }
            else
            {
                if (PhotonNetwork.InRoom)
                    GorillaTagger.Instance.myVRRig.enabled = true;
                else
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
    }
}