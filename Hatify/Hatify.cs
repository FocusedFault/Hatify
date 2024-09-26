using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace Hatify
{
  [BepInPlugin("com.Nuxlar.Hatify", "Hatify", "1.0.2")]

  public class Hatify : BaseUnityPlugin
  {
    private Material hatMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/Commando/matCommandoDualies.mat").WaitForCompletion();
    private GameObject hat = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Bandit2/mdlBandit2.fbx").WaitForCompletion().transform.GetChild(4).GetChild(2).GetChild(0).GetChild(6).GetChild(0).GetChild(2).GetChild(0).gameObject;
    private Material banditHatMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/Bandit2/matBandit2AltColossus.mat").WaitForCompletion();

    public void Awake()
    {
      On.RoR2.CharacterModel.Start += CharacterModel_Start;
    }

    private void CharacterModel_Start(On.RoR2.CharacterModel.orig_Start orig, CharacterModel self)
    {
      orig(self);
      self.StartCoroutine(this.HatifyThese(self));
    }

    private IEnumerator HatifyThese(CharacterModel model)
    {
      yield return new WaitForFixedUpdate();
      if ((bool)model.body)
      {
        if (model.body.name == "Bandit2Body(Clone)")
        {
          if (model.GetComponent<ModelSkinController>().currentSkinIndex == 2)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.hat, model.transform.GetChild(4).GetChild(2).GetChild(0).GetChild(6).GetChild(0).GetChild(2));
            gameObject.AddComponent<NetworkIdentity>();
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            gameObject.transform.localPosition = new Vector3(0f, 0.15f, 0f);
            gameObject.transform.Rotate(new Vector3(10f, 0.0f, 0.0f));
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.banditHatMat;
            List<CharacterModel.RendererInfo> rendererInfos = ((IEnumerable<CharacterModel.RendererInfo>)model.baseRendererInfos).ToList<CharacterModel.RendererInfo>();
            Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
            for (int index = 0; index < rendererArray.Length; ++index)
            {
              Renderer renderer = rendererArray[index];
              rendererInfos.Add(new CharacterModel.RendererInfo()
              {
                renderer = renderer,
                defaultMaterial = renderer.sharedMaterial,
                defaultShadowCastingMode = renderer.shadowCastingMode,
                hideOnDeath = false,
                ignoreOverlays = false
              });
              renderer = (Renderer)null;
            }
            rendererArray = (Renderer[])null;
            model.baseRendererInfos = rendererInfos.ToArray();
            gameObject = (GameObject)null;
            rendererInfos = (List<CharacterModel.RendererInfo>)null;
          }
        }
        else if (model.body.name == "CaptainBody(Clone)")
        {
          {
            Transform captainHead = model.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetChild(0);
            Transform captainHat = captainHead.Find("CaptainHat");
            if (captainHat)
            {
              captainHat.gameObject.SetActive(false);
            }
            GameObject gameObject = Object.Instantiate<GameObject>(this.hat, captainHead);
            gameObject.AddComponent<NetworkIdentity>();
            gameObject.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            gameObject.transform.localPosition = new Vector3(0f, 0.15f, 0);
            gameObject.transform.Rotate(new Vector3(15f, 0.0f, 0.0f));
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.hatMat;
            List<CharacterModel.RendererInfo> rendererInfos = ((IEnumerable<CharacterModel.RendererInfo>)model.baseRendererInfos).ToList<CharacterModel.RendererInfo>();
            Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
            for (int index = 0; index < rendererArray.Length; ++index)
            {
              Renderer renderer = rendererArray[index];
              rendererInfos.Add(new CharacterModel.RendererInfo()
              {
                renderer = renderer,
                defaultMaterial = renderer.sharedMaterial,
                defaultShadowCastingMode = renderer.shadowCastingMode,
                hideOnDeath = false,
                ignoreOverlays = false
              });
              renderer = (Renderer)null;
            }
            rendererArray = (Renderer[])null;
            model.baseRendererInfos = rendererInfos.ToArray();
            gameObject = (GameObject)null;
            rendererInfos = (List<CharacterModel.RendererInfo>)null;
          }
        }
        else if (model.body.name == "CommandoBody(Clone)")
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.hat, model.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0));
          gameObject.AddComponent<NetworkIdentity>();
          gameObject.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
          gameObject.transform.localPosition = new Vector3(0.0f, 0.3f, 0.0f);
          gameObject.transform.Rotate(new Vector3(15f, 0.0f, 0.0f));
          gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.hatMat;
          List<CharacterModel.RendererInfo> rendererInfos = ((IEnumerable<CharacterModel.RendererInfo>)model.baseRendererInfos).ToList<CharacterModel.RendererInfo>();
          Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
          for (int index = 0; index < rendererArray.Length; ++index)
          {
            Renderer renderer = rendererArray[index];
            rendererInfos.Add(new CharacterModel.RendererInfo()
            {
              renderer = renderer,
              defaultMaterial = renderer.sharedMaterial,
              defaultShadowCastingMode = renderer.shadowCastingMode,
              hideOnDeath = false,
              ignoreOverlays = false
            });
            renderer = (Renderer)null;
          }
          rendererArray = (Renderer[])null;
          model.baseRendererInfos = rendererInfos.ToArray();
          gameObject = (GameObject)null;
          rendererInfos = (List<CharacterModel.RendererInfo>)null;
        }
        else if (model.body.name == "RailgunnerBody(Clone)")
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.hat, model.transform.GetChild(5).GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(2).GetChild(0));
          gameObject.AddComponent<NetworkIdentity>();
          gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
          gameObject.transform.localPosition = new Vector3(0.0f, 0.175f, -0.025f);
          gameObject.transform.Rotate(new Vector3(30f, 0.0f, 0.0f));
          gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.hatMat;
          List<CharacterModel.RendererInfo> rendererInfos = ((IEnumerable<CharacterModel.RendererInfo>)model.baseRendererInfos).ToList<CharacterModel.RendererInfo>();
          Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
          for (int index = 0; index < rendererArray.Length; ++index)
          {
            Renderer renderer = rendererArray[index];
            rendererInfos.Add(new CharacterModel.RendererInfo()
            {
              renderer = renderer,
              defaultMaterial = renderer.sharedMaterial,
              defaultShadowCastingMode = renderer.shadowCastingMode,
              hideOnDeath = false,
              ignoreOverlays = false
            });
            renderer = (Renderer)null;
          }
          rendererArray = (Renderer[])null;
          model.baseRendererInfos = rendererInfos.ToArray();
          gameObject = (GameObject)null;
          rendererInfos = (List<CharacterModel.RendererInfo>)null;
        }
        else if (model.body.name == "MageBody(Clone)")
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.hat, model.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(3).GetChild(0).GetChild(2).GetChild(0));
          gameObject.AddComponent<NetworkIdentity>();
          gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
          gameObject.transform.localPosition = new Vector3(0.0f, 0.15f, -0.1f);
          gameObject.transform.Rotate(new Vector3(15f, 0.0f, 0.0f));
          gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.hatMat;
          List<CharacterModel.RendererInfo> rendererInfos = ((IEnumerable<CharacterModel.RendererInfo>)model.baseRendererInfos).ToList<CharacterModel.RendererInfo>();
          Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
          for (int index = 0; index < rendererArray.Length; ++index)
          {
            Renderer renderer = rendererArray[index];
            rendererInfos.Add(new CharacterModel.RendererInfo()
            {
              renderer = renderer,
              defaultMaterial = renderer.sharedMaterial,
              defaultShadowCastingMode = renderer.shadowCastingMode,
              hideOnDeath = false,
              ignoreOverlays = false
            });
            renderer = (Renderer)null;
          }
          rendererArray = (Renderer[])null;
          model.baseRendererInfos = rendererInfos.ToArray();
          gameObject = (GameObject)null;
          rendererInfos = (List<CharacterModel.RendererInfo>)null;
        }
        else if (model.body.name == "HuntressBody(Clone)")
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.hat, model.transform.GetChild(2).GetChild(3).GetChild(0).GetChild(2).GetChild(0).GetChild(1));
          gameObject.AddComponent<NetworkIdentity>();
          gameObject.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
          gameObject.transform.localPosition = new Vector3(0.0f, 0.3f, -0.05f);
          gameObject.transform.Rotate(new Vector3(15f, 0.0f, 0.0f));
          gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.hatMat;
          List<CharacterModel.RendererInfo> rendererInfos = ((IEnumerable<CharacterModel.RendererInfo>)model.baseRendererInfos).ToList<CharacterModel.RendererInfo>();
          Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
          for (int index = 0; index < rendererArray.Length; ++index)
          {
            Renderer renderer = rendererArray[index];
            rendererInfos.Add(new CharacterModel.RendererInfo()
            {
              renderer = renderer,
              defaultMaterial = renderer.sharedMaterial,
              defaultShadowCastingMode = renderer.shadowCastingMode,
              hideOnDeath = false,
              ignoreOverlays = false
            });
            renderer = (Renderer)null;
          }
          rendererArray = (Renderer[])null;
          model.baseRendererInfos = rendererInfos.ToArray();
          gameObject = (GameObject)null;
          rendererInfos = (List<CharacterModel.RendererInfo>)null;
        }
        else if (model.body.name == "CrocoBody(Clone)")
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.hat, model.transform.GetChild(5).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0));
          gameObject.AddComponent<NetworkIdentity>();
          gameObject.transform.localScale = new Vector3(15f, 15f, 15f);
          gameObject.transform.localPosition = new Vector3(0.0f, 0f, 1.6f);
          gameObject.transform.Rotate(new Vector3(55f, 180f, 180f));
          gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.hatMat;
          List<CharacterModel.RendererInfo> rendererInfos = ((IEnumerable<CharacterModel.RendererInfo>)model.baseRendererInfos).ToList<CharacterModel.RendererInfo>();
          Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
          for (int index = 0; index < rendererArray.Length; ++index)
          {
            Renderer renderer = rendererArray[index];
            rendererInfos.Add(new CharacterModel.RendererInfo()
            {
              renderer = renderer,
              defaultMaterial = renderer.sharedMaterial,
              defaultShadowCastingMode = renderer.shadowCastingMode,
              hideOnDeath = false,
              ignoreOverlays = false
            });
            renderer = (Renderer)null;
          }
          rendererArray = (Renderer[])null;
          model.baseRendererInfos = rendererInfos.ToArray();
          gameObject = (GameObject)null;
          rendererInfos = (List<CharacterModel.RendererInfo>)null;
        }
        else if (model.body.name == "EngiBody(Clone)")
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.hat, model.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(3).GetChild(0));
          gameObject.AddComponent<NetworkIdentity>();
          gameObject.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
          gameObject.transform.localPosition = new Vector3(0.0f, 0.65f, 0.0f);
          gameObject.transform.Rotate(new Vector3(15f, 0.0f, 0.0f));
          gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.hatMat;
          List<CharacterModel.RendererInfo> rendererInfos = ((IEnumerable<CharacterModel.RendererInfo>)model.baseRendererInfos).ToList<CharacterModel.RendererInfo>();
          Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
          for (int index = 0; index < rendererArray.Length; ++index)
          {
            Renderer renderer = rendererArray[index];
            rendererInfos.Add(new CharacterModel.RendererInfo()
            {
              renderer = renderer,
              defaultMaterial = renderer.sharedMaterial,
              defaultShadowCastingMode = renderer.shadowCastingMode,
              hideOnDeath = false,
              ignoreOverlays = false
            });
            renderer = (Renderer)null;
          }
          rendererArray = (Renderer[])null;
          model.baseRendererInfos = rendererInfos.ToArray();
          gameObject = (GameObject)null;
          rendererInfos = (List<CharacterModel.RendererInfo>)null;
        }
        else if (model.body.name == "EngiWalkerTurretBody(Clone)")
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.hat, model.transform.GetChild(1).GetChild(0).GetChild(4).GetChild(0));
          gameObject.AddComponent<NetworkIdentity>();
          gameObject.transform.localScale = new Vector3(8f, 8f, 8f);
          gameObject.transform.localPosition = new Vector3(0.0f, 1f, 0.0f);
          gameObject.transform.Rotate(new Vector3(15f, 0.0f, 0.0f));
          gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.hatMat;
          List<CharacterModel.RendererInfo> rendererInfos = ((IEnumerable<CharacterModel.RendererInfo>)model.baseRendererInfos).ToList<CharacterModel.RendererInfo>();
          Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
          for (int index = 0; index < rendererArray.Length; ++index)
          {
            Renderer renderer = rendererArray[index];
            rendererInfos.Add(new CharacterModel.RendererInfo()
            {
              renderer = renderer,
              defaultMaterial = renderer.sharedMaterial,
              defaultShadowCastingMode = renderer.shadowCastingMode,
              hideOnDeath = false,
              ignoreOverlays = false
            });
            renderer = (Renderer)null;
          }
          rendererArray = (Renderer[])null;
          model.baseRendererInfos = rendererInfos.ToArray();
          gameObject = (GameObject)null;
          rendererInfos = (List<CharacterModel.RendererInfo>)null;
        }
        else if (model.body.name == "EngiTurretBody(Clone)")
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.hat, model.transform.GetChild(1).GetChild(0).GetChild(4).GetChild(0));
          gameObject.AddComponent<NetworkIdentity>();
          gameObject.transform.localScale = new Vector3(10f, 10f, 10f);
          gameObject.transform.localPosition = new Vector3(0.0f, 0.3f, 0.0f);
          gameObject.transform.Rotate(new Vector3(15f, 0.0f, 0.0f));
          gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.hatMat;
          List<CharacterModel.RendererInfo> rendererInfos = ((IEnumerable<CharacterModel.RendererInfo>)model.baseRendererInfos).ToList<CharacterModel.RendererInfo>();
          Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
          for (int index = 0; index < rendererArray.Length; ++index)
          {
            Renderer renderer = rendererArray[index];
            rendererInfos.Add(new CharacterModel.RendererInfo()
            {
              renderer = renderer,
              defaultMaterial = renderer.sharedMaterial,
              defaultShadowCastingMode = renderer.shadowCastingMode,
              hideOnDeath = false,
              ignoreOverlays = false
            });
            renderer = (Renderer)null;
          }
          rendererArray = (Renderer[])null;
          model.baseRendererInfos = rendererInfos.ToArray();
          gameObject = (GameObject)null;
          rendererInfos = (List<CharacterModel.RendererInfo>)null;
        }
        else if (model.body.name == "MercBody(Clone)")
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.hat, model.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(3).GetChild(0).GetChild(3).GetChild(1));
          gameObject.AddComponent<NetworkIdentity>();
          gameObject.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
          gameObject.transform.localPosition = new Vector3(0.0f, 0.2f, 0.0f);
          gameObject.transform.Rotate(new Vector3(15f, 0.0f, 0.0f));
          gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.hatMat;
          List<CharacterModel.RendererInfo> rendererInfos = ((IEnumerable<CharacterModel.RendererInfo>)model.baseRendererInfos).ToList<CharacterModel.RendererInfo>();
          Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
          for (int index = 0; index < rendererArray.Length; ++index)
          {
            Renderer renderer = rendererArray[index];
            rendererInfos.Add(new CharacterModel.RendererInfo()
            {
              renderer = renderer,
              defaultMaterial = renderer.sharedMaterial,
              defaultShadowCastingMode = renderer.shadowCastingMode,
              hideOnDeath = false,
              ignoreOverlays = false
            });
            renderer = (Renderer)null;
          }
          rendererArray = (Renderer[])null;
          model.baseRendererInfos = rendererInfos.ToArray();
          gameObject = (GameObject)null;
          rendererInfos = (List<CharacterModel.RendererInfo>)null;
        }
        else if (model.body.name == "VoidSurvivorBody(Clone)")
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.hat, model.transform.GetChild(7).GetChild(0).GetChild(0).GetChild(1).GetChild(2).GetChild(2).GetChild(0));
          gameObject.AddComponent<NetworkIdentity>();
          gameObject.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
          gameObject.transform.localPosition = new Vector3(0.0f, 0.1f, 0.0f);
          gameObject.transform.Rotate(new Vector3(15f, 0.0f, 0.0f));
          gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.hatMat;
          List<CharacterModel.RendererInfo> rendererInfos = ((IEnumerable<CharacterModel.RendererInfo>)model.baseRendererInfos).ToList<CharacterModel.RendererInfo>();
          Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
          for (int index = 0; index < rendererArray.Length; ++index)
          {
            Renderer renderer = rendererArray[index];
            rendererInfos.Add(new CharacterModel.RendererInfo()
            {
              renderer = renderer,
              defaultMaterial = renderer.sharedMaterial,
              defaultShadowCastingMode = renderer.shadowCastingMode,
              hideOnDeath = false,
              ignoreOverlays = false
            });
            renderer = (Renderer)null;
          }
          rendererArray = (Renderer[])null;
          model.baseRendererInfos = rendererInfos.ToArray();
          gameObject = (GameObject)null;
          rendererInfos = (List<CharacterModel.RendererInfo>)null;
        }
        else if (model.body.name == "LoaderBody(Clone)")
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.hat, model.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(5).GetChild(0).GetChild(3).GetChild(0));
          gameObject.AddComponent<NetworkIdentity>();
          gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
          gameObject.transform.localPosition = new Vector3(0.0f, 0.2f, 0.0f);
          gameObject.transform.Rotate(new Vector3(15f, 0.0f, 0.0f));
          gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.hatMat;
          List<CharacterModel.RendererInfo> rendererInfos = ((IEnumerable<CharacterModel.RendererInfo>)model.baseRendererInfos).ToList<CharacterModel.RendererInfo>();
          Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
          for (int index = 0; index < rendererArray.Length; ++index)
          {
            Renderer renderer = rendererArray[index];
            rendererInfos.Add(new CharacterModel.RendererInfo()
            {
              renderer = renderer,
              defaultMaterial = renderer.sharedMaterial,
              defaultShadowCastingMode = renderer.shadowCastingMode,
              hideOnDeath = false,
              ignoreOverlays = false
            });
            renderer = (Renderer)null;
          }
          rendererArray = (Renderer[])null;
          model.baseRendererInfos = rendererInfos.ToArray();
          gameObject = (GameObject)null;
          rendererInfos = (List<CharacterModel.RendererInfo>)null;
        }
      }
    }
  }
}