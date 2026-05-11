using GorillaNetworking;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShibaGTGenesis
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Boards : MonoBehaviour
    {
        public Boards(IntPtr ptr) : base(ptr) { }

        private GorillaLevelScreen[] cachedScreens;

        private Text cocText;
        private Text coc;
        private Text motdText;
        private Text motd;

        private Material boardMat;

        private Renderer motdRenderer;
        private Renderer cocRenderer;

        private bool initialized;

        public void Start()
        {
            cachedScreens = GorillaComputer.instance.levelScreens;
            cocText = GameObject.Find("COC Text").GetComponent<Text>();
            coc = GameObject.Find("CodeOfConduct").GetComponent<Text>();
            motdText = GameObject.Find("motdtext").GetComponent<Text>();
            motd = GameObject.Find("motd").GetComponent<Text>();
            motdRenderer = GameObject.Find("wallmonitorlong").GetComponent<Renderer>();
            cocRenderer = GameObject.Find("code of conduct/board").GetComponent<Renderer>();
            boardMat = new Material(Shader.Find("Unlit/Color"));
            boardMat.color = Color.black;
            Material[] motdMats = motdRenderer.materials;
            if (motdMats.Length > 1)
            {
                motdMats[1] = boardMat;
                motdRenderer.materials = motdMats;
            }
            Material[] cocMats = cocRenderer.materials;
            if (cocMats.Length > 1)
            {
                cocMats[1] = boardMat;
                cocRenderer.materials = cocMats;
            }
            foreach (var screen in cachedScreens)
            {
                screen.goodMaterial = boardMat;
                screen.badMaterial = boardMat;
            }
            initialized = true;
        }
        public void Update()
        {
            if (!initialized)
                return;
            if (motd != null && motd.gameObject.activeSelf)
            {
                coc.supportRichText = true;
                motdText.supportRichText = true;
                motd.supportRichText = true;
                cocText.supportRichText = true;
                motd.text = "<color=blue>GENESIS MOTD</color>";
                motdText.text =
                    "WELCOME TO THE FIRST VERSION OF GENESIS! I HOPE YOU ENJOY!\n" +
                    "THIS IS THE PREDECESSOR TO IIMENUCOPYS, SO IT IS A FULL REWRITE AND REBRAND.\n\n" +
                    "ENJOY :D\n\n- <3 NOVA";
                coc.text = "<color=blue>GENESIS NEWS</color>";
                cocText.text =
                    "SHIBAGT <color=blue>GENESIS</color> | BEST MENU ON THE MARKET!\n\n" +
                    "<color=yellow>[ MENU STATUS : RELEASED - MOSTLY TESTED ]\n" +
                    "ANY UPDATES WILL BE ON THE MOTD BOARD, AS THAT BOARD LIVE UPDATES.\n" +
                    "THE MENU IS NOT A RAT!! ANYONE WHO SAYS THAT, HAS NO PROOF, DO NOT BELIEVE THEM.\n\n" +
                    "MENU BY NOVA (@novaissilly)";
                foreach (var screen in cachedScreens)
                {
                    screen.goodMaterial = boardMat;
                    screen.badMaterial = boardMat;
                }
            }
        }
    }
}