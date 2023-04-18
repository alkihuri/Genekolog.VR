using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace TVP
{
    public class UziDisplayController : DisplayController
    {
        [SerializeField] RawImage _image;
        [SerializeField] RawImage _printImage;
        [SerializeField] Texture2D _texture;

         

        public void PrintPhoto() => StartCoroutine(GetImage());

        IEnumerator GetImage()
        {
             
             
            Texture2D tex = new Texture2D(_image.mainTexture.width, _image.mainTexture.height);
            var pixels = toTexture2D((RenderTexture)_image.mainTexture).GetPixels();
            yield return new WaitUntil(()=>pixels.Length > 0);
            tex.SetPixels(pixels);
            tex.Apply(); 
            yield return new WaitUntil(()=>tex.isReadable);
            _printImage.texture = tex;

            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.PrintResultOfSurgery_08_02_02);
            yield return new WaitForSeconds(1); 
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsDone_08_02);

        }
        Texture2D toTexture2D(RenderTexture rTex)
        {
            Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
            RenderTexture.active = rTex;
            tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            tex.Apply();
            return tex;
        }

         
    }

}