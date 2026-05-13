// Bootstrap CustomSettings — 99 types from Ghidra LuaBinder.Bind extract.
// Types verified to exist in project (Ported/, Generated/_Skeletons/, AssetRipper, ToLua).
// Missing 139/238 = third-party SDK (MarsSDK platforms, Spine, PostProcessing) — skipped.

using System;
using System.Collections.Generic;
using LuaInterface;
using BindType = ToLuaMenu.BindType;
using UnityEngine;

public static class CustomSettings
{
    public static string saveDir = Application.dataPath + "/Source/Generate/";
    public static string toluaBaseType = Application.dataPath + "/ToLua/BaseType/";
    public static string baseLuaDir = Application.dataPath + "/ToLua/Lua/";
    public static string injectionFilesPath = Application.dataPath + "/ToLua/Injection/";

    // Lists FIRST (referenced by BindType ctor)
    public static List<Type> dynamicList = new List<Type>();
    public static List<Type> sealedList = new List<Type>();
    public static List<Type> staticClassTypes = new List<Type>();
    public static List<Type> outList = new List<Type>();

    public static DelegateType[] customDelegateList = {
        _DT(typeof(TResource2.cbFunction)),
    };

    public static BindType[] customTypeList =
    {
        _GT(typeof(AStarMgr)),
        _GT(typeof(AdvImage)),
        _GT(typeof(ApkValidater)),
        _GT(typeof(AutoSpriteBase)),
        _GT(typeof(BaseEntityLua)),
        _GT(typeof(BaseImage)),
        _GT(typeof(BaseProcLua)),
        _GT(typeof(BinFileMgr)),
        _GT(typeof(CBaseProc)),
        _GT(typeof(CProcManager)),
        _GT(typeof(CircleImage)),
        _GT(typeof(ConfigMgr)),
        _GT(typeof(ConfigVar)),
        _GT(typeof(CreateJobIMG)),
        _GT(typeof(EProcID)),
        _GT(typeof(ESGameManager)),
        _GT(typeof(FhyxSDKManager)),
        _GT(typeof(FxPlayer)),
        _GT(typeof(FxResource)),
        _GT(typeof(FxhySDKManager)),
        _GT(typeof(GMCommandHelper)),
        _GT(typeof(GameProcMgr)),
        _GT(typeof(GradientText)),
        _GT(typeof(IWndComponent)),
        _GT(typeof(IconTextureMgr)),
        _GT(typeof(ImageViewer)),
        _GT(typeof(InfiniteVerticalScroll)),
        _GT(typeof(LuaFramework.Util)),
        _GT(typeof(LuaInterface.InjectType)),
        _GT(typeof(LuaInterface.LuaInjectionStation)),
        _GT(typeof(LuaProfiler)),
        _GT(typeof(MagicFxData)),
        _GT(typeof(MagicFxData.FxData)),
        _GT(typeof(MagicFxData.FxDatas)),
        _GT(typeof(MagicFxLoader)),
        _GT(typeof(MagicFxLoader.FxLoader)),
        _GT(typeof(MagicFxLoader.FxLoaders)),
        _GT(typeof(MagicFxResource)),
        _GT(typeof(MagicLoader)),
        _GT(typeof(MarsSDK.MarsFunction)),
        _GT(typeof(MarsSDK.MarsFunction.DeleteAccountAction)),
        _GT(typeof(MarsSDK.MarsMessageProcess)),
        _GT(typeof(MarsSDK.MarsTools)),
        _GT(typeof(MarsSDK.Platform.BasePlatform)),
        _GT(typeof(MusicProxy)),
        _GT(typeof(NetReceivePackerBase)),
        _GT(typeof(NetReceiverLua)),
        _GT(typeof(NetRequestPackerBase)),
        _GT(typeof(PathNodeRealTime)),
        _GT(typeof(PlayerCamControl)),
        _GT(typeof(ProxyWndForm)),
        _GT(typeof(ResMgr)),
        _GT(typeof(ResourceBase)),
        _GT(typeof(ResourceUnloader)),
        _GT(typeof(SGCLanguage)),
        _GT(typeof(SGCRegion)),
        _GT(typeof(SGCSteamManager)),
        _GT(typeof(SlotData)),
        _GT(typeof(SoundProxy)),
        _GT(typeof(SpriteManagerPool)),
        _GT(typeof(SpriteRoot)),
        _GT(typeof(TextureAnim)),
        _GT(typeof(TweenAlpha)),
        _GT(typeof(TweenPosition)),
        _GT(typeof(UICurrency)),
        _GT(typeof(UIDigital)),
        _GT(typeof(UIFixedProgressbarController)),
        _GT(typeof(UIImageColorPicker)),
        _GT(typeof(UIImagePicker)),
        _GT(typeof(UISizeControl)),
        _GT(typeof(UISlot)),
        _GT(typeof(UITweener)),
        _GT(typeof(UITweenerGroup)),
        _GT(typeof(UITweener.Style)),
        _GT(typeof(UJScrollRectSnap)),
        _GT(typeof(UVAnimation)),
        _GT(typeof(WndClickMethod)),
        _GT(typeof(WndForm)),
        _GT(typeof(WndParticle)),
        _GT(typeof(WndRoot)),
        _GT(typeof(WrdFileMgr)),
        _GT(typeof(tageventDATA)),
        _GT(typeof(tagmapCODEDATA)),
        _GT(typeof(UnityEngine.Animation)),
        _GT(typeof(UnityEngine.AnimationState)),
        _GT(typeof(UnityEngine.Animator)),
        _GT(typeof(UnityEngine.AnimatorStateInfo)),
        _GT(typeof(UnityEngine.Application)),
        _GT(typeof(UnityEngine.BoxCollider)),
        _GT(typeof(UnityEngine.Camera)),
        _GT(typeof(UnityEngine.CameraClearFlags)),
        _GT(typeof(UnityEngine.Canvas)),
        _GT(typeof(UnityEngine.CanvasGroup)),
        _GT(typeof(UnityEngine.Color32)),
        _GT(typeof(UnityEngine.EventSystems.EventSystem)),
        _GT(typeof(UnityEngine.EventSystems.PointerEventData)),
        _GT(typeof(UnityEngine.EventType)),
        _GT(typeof(UnityEngine.GameObject)),
        _GT(typeof(UnityEngine.ImageConversion)),
        _GT(typeof(UnityEngine.Input)),
        _GT(typeof(UnityEngine.KeyCode)),
        _GT(typeof(UnityEngine.LODGroup)),
        _GT(typeof(UnityEngine.Light)),
        _GT(typeof(UnityEngine.Material)),
        _GT(typeof(UnityEngine.MeshRenderer)),
        _GT(typeof(UnityEngine.ParticleSystem)),
        _GT(typeof(UnityEngine.Physics)),
        _GT(typeof(UnityEngine.PlayMode)),
        _GT(typeof(UnityEngine.Projector)),
        _GT(typeof(UnityEngine.Ray)),
        _GT(typeof(UnityEngine.RaycastHit)),
        _GT(typeof(UnityEngine.Rect)),
        _GT(typeof(UnityEngine.RectOffset)),
        _GT(typeof(UnityEngine.RectTransformUtility)),
        _GT(typeof(UnityEngine.RenderTexture)),
        _GT(typeof(UnityEngine.Renderer)),
        _GT(typeof(UnityEngine.Resources)),
        _GT(typeof(UnityEngine.Rigidbody)),
        _GT(typeof(UnityEngine.SceneManagement.LoadSceneMode)),
        _GT(typeof(UnityEngine.SceneManagement.Scene)),
        _GT(typeof(UnityEngine.SceneManagement.SceneManager)),
        _GT(typeof(UnityEngine.Screen)),
        _GT(typeof(UnityEngine.ScreenCapture)),
        _GT(typeof(UnityEngine.SkinnedMeshRenderer)),
        _GT(typeof(UnityEngine.Sprite)),
        _GT(typeof(UnityEngine.SystemInfo)),
        _GT(typeof(UnityEngine.Texture)),
        _GT(typeof(UnityEngine.Texture2D)),
        _GT(typeof(UnityEngine.TrailRenderer)),
        _GT(typeof(UnityEngine.Transform)),
        _GT(typeof(UnityEngine.UI.Button)),
        _GT(typeof(UnityEngine.UI.GridLayoutGroup)),
        _GT(typeof(UnityEngine.UI.HorizontalLayoutGroup)),
        _GT(typeof(UnityEngine.UI.Image)),
        _GT(typeof(UnityEngine.UI.InputField)),
        _GT(typeof(UnityEngine.UI.LayoutElement)),
        _GT(typeof(UnityEngine.UI.LayoutRebuilder)),
        _GT(typeof(UnityEngine.UI.RawImage)),
        _GT(typeof(UnityEngine.UI.ScrollRect)),
        _GT(typeof(UnityEngine.UI.Scrollbar)),
        _GT(typeof(UnityEngine.UI.Slider)),
        _GT(typeof(UnityEngine.UI.Text)),
        _GT(typeof(UnityEngine.UI.Toggle)),
        _GT(typeof(UnityEngine.UI.ToggleGroup)),
        _GT(typeof(UnityEngine.UI.VerticalLayoutGroup)),
        _GT(typeof(UnityEngine.WrapMode)),
        _GT(typeof(TMPro.TextMeshProUGUI)),
        _GT(typeof(Spine.Unity.SkeletonGraphic)),

        _GT(typeof(UnityEngine.LayerMask)),

        _GT(typeof(UnityEngine.Plane)),

        _GT(typeof(UnityEngine.Bounds)),

        _GT(typeof(UnityEngine.Mathf)),

        _GT(typeof(UnityEngine.Vector2)),

        _GT(typeof(UnityEngine.Vector3)),

        _GT(typeof(UnityEngine.Vector4)),

        _GT(typeof(UnityEngine.Quaternion)),

        _GT(typeof(UnityEngine.Color)),

        _GT(typeof(UnityEngine.Touch)),
        _GT(typeof(TResource2)),
    };

    static BindType _GT(Type t) { return new BindType(t); }
    static DelegateType _DT(Type t) { return new DelegateType(t); }
}
