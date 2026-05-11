using easyInputs;
using ExitGames.Client.Photon;
using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ShibaGTGenesis
{
    public class RoomMods
    {
        public static void JoinGenesis()
        {
            Menu.Menu.Instance.Controller().AttemptToJoinSpecificRoom("GENESIS");
        }

        public static void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public static void BDisconnect()
        {
            if (EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand))
            {
                PhotonNetwork.Disconnect();
            }
        }
        public static void CreatePublicRoom()
        {
            Hashtable dictionaryEntries = new Hashtable();
            dictionaryEntries.Add("gameMode", "INFECTION");
            RoomOptions roomOptions = new RoomOptions()
            {
                MaxPlayers = 10,
                SuppressPlayerInfo = true,
                CustomRoomProperties = dictionaryEntries,
                PublishUserId = true,
                IsOpen = true,
                IsVisible = true
            };
            PhotonNetwork.CreateRoom("HANJ", roomOptions);
        }
        public static void RejoinLastRoom()
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.Disconnect();
            }
            Menu.Menu.Instance.Controller().AttemptToJoinSpecificRoom(Menu.Menu.Instance.Lastroom);
        }
        public static void Serverhop()
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.Disconnect();
            }
            PhotonNetwork.JoinRandomRoom();
        }

        public static void MuteGun()
        {
            if (Menu.Menu.GetGunInput(false))
            {
                var GunData = Menu.Menu.RenderGun();
                GameObject Pointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;
                if (Menu.Menu.GetGunInput(true))
                {
                    VRRig rig = Ray.collider.GetComponentInParent<VRRig>();
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        rig.muted = true;
                        GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().Where(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).FirstOrDefault().PressButton(true, GorillaPlayerLineButton.ButtonType.Mute);
                        GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().Where(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).FirstOrDefault().muteButton.enabled = true;
                    }
                }
            }
        }

        public static void MuteAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                {
                    rig.muted = true;
                    GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().Where(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).FirstOrDefault().PressButton(true, GorillaPlayerLineButton.ButtonType.Mute);
                    GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().Where(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).FirstOrDefault().muteButton.enabled = true;
                }
            }
        }

        public static void UnmuteGun()
        {
            if (Menu.Menu.GetGunInput(false))
            {
                var GunData = Menu.Menu.RenderGun();
                GameObject Pointer = GunData.Pointer;
                RaycastHit Ray = GunData.Ray;
                if (Menu.Menu.GetGunInput(true))
                {
                    VRRig rig = Ray.collider.GetComponentInParent<VRRig>();
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        rig.muted = false;
                        GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().Where(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).FirstOrDefault().PressButton(false, GorillaPlayerLineButton.ButtonType.Mute);
                        GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().Where(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).FirstOrDefault().muteButton.enabled = false;
                    }
                }
            }
        }

        public static void UnmuteAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                {
                    rig.muted = false;
                    GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().Where(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).FirstOrDefault().PressButton(false, GorillaPlayerLineButton.ButtonType.Mute);
                    GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>().Where(line => line.linePlayer.UserId == rig.photonView.Owner.UserId).FirstOrDefault().muteButton.enabled = false;
                }
            }
        }

        public static void Rejoin()
        {
            Menu.Menu.Instance.Controller().OnDisconnected(DisconnectCause.DisconnectByClientLogic);
            Menu.Menu.Instance.rejoinCode = PhotonNetwork.CurrentRoom.Name;
            PhotonNetwork.Disconnect();
        }

        public static void CancelRejoin()
        {
            Menu.Menu.Instance.rejoinCode = null;
        }

        private static GameObject cachedTriggers;
        private static GameObject GetNetworkingTriggers()
        {
            if (cachedTriggers == null)
            {
                cachedTriggers = GameObject.Find("NetworkTriggers");
            }
            return cachedTriggers;
        }

        public static void DisableNetworkTriggers()
        {
            GameObject triggers = GetNetworkingTriggers();
            if (triggers != null)
            {
                triggers.SetActive(false);
            }
        }

        public static void EnableNetworkTriggers()
        {
            GameObject triggers = GetNetworkingTriggers();
            if (triggers != null)
            {
                triggers.SetActive(true);
            }
        }

        public static void VisibleTrigs()
        {
            GameObject triggers = GetNetworkingTriggers();
            if (triggers == null)
                return;
            foreach (Renderer g in triggers.GetComponentsInChildren<Renderer>(true))
            {
                g.material.shader = Shader.Find("Standard");
                g.enabled = true;
            }
        }
        public static void NonVisTrigs()
        {
            GameObject triggers = GetNetworkingTriggers();
            if (triggers == null)
                return;
            foreach (Renderer g in triggers.GetComponentsInChildren<Renderer>(true))
            {
                g.enabled = false;
            }
        }


        public static void AntiReport()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                    {
                        foreach (GorillaPlayerScoreboardLine line in GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>())
                        {
                            if (line.linePlayer.UserId == rig.photonView.Owner.UserId)
                            {
                                Transform reportButton = line.reportButton.transform;
                                float distanceR = Vector3.Distance(reportButton.position, rig.rightHandTransform.position);
                                float distanceL = Vector3.Distance(reportButton.position, rig.leftHandTransform.position);
                                if (distanceR > 0.35f || distanceL > 0.35f)
                                {
                                    PhotonNetwork.Disconnect();
                                    NotificationManager.SendNotification($"<color=red>[AntiReport]</color>  The player {rig.photonView.Owner.NickName} almost reported you, but dont worry ShibaGT Genesis's antireport made u leave they could report u!");
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void AntiModerator()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != null && rig != GorillaTagger.Instance.myVRRig)
                {
                    if (rig.concatStringOfCosmeticsAllowed.Contains("LBAAK"))
                    {
                        PhotonNetwork.Disconnect();
                        NotificationManager.SendNotification("<color=red>[ANTI-MODERATOR]</color> Someone with a STICK joined, disconnected.");
                    }
                }
            }
        }

        public static void HideNameOnLeaderboard()
        {
            if (PhotonNetwork.InRoom)
            {
                GorillaComputer.instance.savedName = "GORILLA" + UnityEngine.Random.Range(0000, 9999);
                GorillaComputer.instance.currentName = "GORILLA" + UnityEngine.Random.Range(0000, 9999);
                foreach (GorillaScoreBoard line in GameObject.FindObjectsOfType<GorillaScoreBoard>())
                {
                    line.RedrawPlayerLines();
                }
            }
        }

        static float gradientTime;
        public static void GenesisName()
        {
            gradientTime += Time.deltaTime * 2f;
            string animatedName = GenerateGradientText(
                "SHIBAGT GENESIS",
                new Color(0.23f, 0.51f, 0.96f),
                new Color(0.61f, 0.64f, 0.69f),
                gradientTime
            );
            PhotonNetwork.LocalPlayer.NickName = animatedName + "\n<color=#60A5FA>BY NOVA</color>\n" + "discord.gg/dtQdz59FJG";
        }
        static string GenerateGradientText(string text, Color color1, Color color2, float time)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                float t = Mathf.PingPong(time + i * 0.15f, 1f);
                Color current = Color.Lerp(color1, color2, t);
                string hex = ColorUtility.ToHtmlStringRGB(current);
                sb.Append($"<color=#{hex}>{text[i]}</color>");
            }
            return sb.ToString();
        }
    }
}