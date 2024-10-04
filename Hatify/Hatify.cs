using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace Hatify
{
  [BepInPlugin("com.Nuxlar.Hatify", "Hatify", "1.0.3")]

  public class Hatify : BaseUnityPlugin
  {
    private Material hatMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/Commando/matCommandoDualies.mat").WaitForCompletion();
    private GameObject hat = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Bandit2/mdlBandit2.fbx").WaitForCompletion().transform.GetChild(4).GetChild(2).GetChild(0).GetChild(6).GetChild(0).GetChild(2).GetChild(0).gameObject;
    private Material banditHatMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/Bandit2/matBandit2AltColossus.mat").WaitForCompletion();

    public static ConfigEntry<float> commandoSize;
    public static ConfigEntry<float> banditSize;
    public static ConfigEntry<float> huntressSize;
    public static ConfigEntry<float> engiSize;
    public static ConfigEntry<float> engiTurretSize;
    public static ConfigEntry<float> engiWalkerTurretSize;
    public static ConfigEntry<float> artiSize;
    public static ConfigEntry<float> mercSize;
    public static ConfigEntry<float> loaderSize;
    public static ConfigEntry<float> acridSize;
    public static ConfigEntry<float> captainSize;
    public static ConfigEntry<float> railgunnerSize;
    public static ConfigEntry<float> fiendSize;
    private static ConfigFile HatifyConfig { get; set; }

    public void Awake()
    {
      HatifyConfig = new ConfigFile(Paths.ConfigPath + "\\com.Nuxlar.Hatify.cfg", true);
      commandoSize = HatifyConfig.Bind<float>("General", "Commando Hat Size", 1.3f, "The scale of the hat.");
      banditSize = HatifyConfig.Bind<float>("General", "Bandit Hat Size", 1f, "The scale of the hat.");
      huntressSize = HatifyConfig.Bind<float>("General", "Huntress Hat Size", 1.3f, "The scale of the hat.");
      engiSize = HatifyConfig.Bind<float>("General", "Engi Hat Size", 1.6f, "The scale of the hat.");
      engiTurretSize = HatifyConfig.Bind<float>("General", "Engi Turret Hat Size", 10f, "The scale of the hat.");
      engiWalkerTurretSize = HatifyConfig.Bind<float>("General", "Engi Walker Turret Hat Size", 8f, "The scale of the hat.");
      artiSize = HatifyConfig.Bind<float>("General", "Artificer Hat Size", 1f, "The scale of the hat.");
      mercSize = HatifyConfig.Bind<float>("General", "Merc Hat Size", 1.4f, "The scale of the hat.");
      loaderSize = HatifyConfig.Bind<float>("General", "Loader Hat Size", 1.5f, "The scale of the hat.");
      acridSize = HatifyConfig.Bind<float>("General", "Acrid Hat Size", 15f, "The scale of the hat.");
      captainSize = HatifyConfig.Bind<float>("General", "Captain Hat Size", 1.3f, "The scale of the hat.");
      railgunnerSize = HatifyConfig.Bind<float>("General", "Railgunner Hat Size", 1f, "The scale of the hat.");
      fiendSize = HatifyConfig.Bind<float>("General", "Fiend Hat Size", 1.4f, "The scale of the hat.");

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
            gameObject.transform.localScale = new Vector3(banditSize.Value, banditSize.Value, banditSize.Value);
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
            gameObject.transform.localScale = new Vector3(captainSize.Value, captainSize.Value, captainSize.Value);
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
          gameObject.transform.localScale = new Vector3(commandoSize.Value, commandoSize.Value, commandoSize.Value);
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
          gameObject.transform.localScale = new Vector3(railgunnerSize.Value, railgunnerSize.Value, railgunnerSize.Value);
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
          gameObject.transform.localScale = new Vector3(artiSize.Value, artiSize.Value, artiSize.Value);
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
          gameObject.transform.localScale = new Vector3(huntressSize.Value, huntressSize.Value, huntressSize.Value);
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
          gameObject.transform.localScale = new Vector3(acridSize.Value, acridSize.Value, acridSize.Value);
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
          gameObject.transform.localScale = new Vector3(engiSize.Value, engiSize.Value, engiSize.Value);
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
          gameObject.transform.localScale = new Vector3(engiWalkerTurretSize.Value, engiWalkerTurretSize.Value, engiWalkerTurretSize.Value);
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
          gameObject.transform.localScale = new Vector3(engiTurretSize.Value, engiTurretSize.Value, engiTurretSize.Value);
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
          gameObject.transform.localScale = new Vector3(mercSize.Value, mercSize.Value, mercSize.Value);
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
          gameObject.transform.localScale = new Vector3(fiendSize.Value, fiendSize.Value, fiendSize.Value);
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
          gameObject.transform.localScale = new Vector3(loaderSize.Value, loaderSize.Value, loaderSize.Value);
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