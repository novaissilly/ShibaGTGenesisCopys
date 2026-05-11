using ExitGames.Client.Photon;
using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ShibaGTGenesis
{
    public class RoomMods
    {
        public static void JoinGenesis()
        {
            Menu.Menu.Instance.controller.AttemptToJoinSpecificRoom("GENESIS");
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
            PhotonNetwork.Disconnect();
            Menu.Menu.Instance.controller.AttemptToJoinSpecificRoom(Menu.Menu.Instance.Lastroom);
        }
        public static void Serverhop()
        {
            PhotonNetwork.Disconnect();
            PhotonNetwork.JoinRandomRoom();
        }

        public static void DisableNetworkTriggers()
        {
            GameObject.Find("NetworkTriggers").SetActive(true);
        }
        public static void EnableNetworkTriggers()
        {
            GameObject.Find("NetworkTriggers").SetActive(true);
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

        public static void GenesisName()
        {
            PhotonNetwork.LocalPlayer.NickName = "<color=blue>SHIBAGT GENESIS</color> <color=cyan>BY NOVA</color>\ndiscord.gg/dtQdz59FJG";
        }
    }
}