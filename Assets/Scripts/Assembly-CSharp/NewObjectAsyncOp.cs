// Port 1-1 from Ghidra (decompiled_rva/NewObjectAsyncOp__*.c).
// All 15 methods (RVAs 0x17BBAF4 … 0x17BC7E4) ported with explicit Ghidra source comments.

using System.Collections.Generic;
using UnityEngine;

public class NewObjectAsyncOp : NewObjectAsyncOpBase
{
	// Static fields (per dump.cs TypeDefIndex 129 + Ghidra .cctor)
	private static List<NewObjectAsyncOp> _waitRequest;  // offset 0x0
	private static uint _totalProcessed;                 // offset 0x8
	private static int _requestNumber;                   // offset 0xC
	private const int MAX_REQUESTNUMBER = 10;

	// Instance fields (offsets per dump.cs + verified by Ghidra .ctor.c)
	private string[] _names;                             // 0x18
	private CBNewObjectLoader _callback;                 // 0x20
	private bool _bFinish;                               // 0x28
	private RequestFile _request;                        // 0x30
	private AssetBundleOP _bundleOP;                     // 0x38
	private long _bundleBytes;                           // 0x40
	private new string _error;                           // 0x48 (shadow of base _error at 0x10)
	private ResourcesLoader.AssetType _asType;           // 0x50
	private string _requestPath;                         // 0x58

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp__get_requestNumber.c RVA 0x17BBAF4
	// 1-1: return *(static + 0xC);  // = _requestNumber
	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp__set_requestNumber.c RVA 0x17BBB4C
	// 1-1: *(static + 0xC) = value;
	private static int requestNumber
	{
		get
		{
			return _requestNumber;
		}
		set
		{
			_requestNumber = value;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp__get_isDone.c RVA 0x17BBBA8
	// 1-1: return *(this + 0x28);   // = _bFinish
	public override bool isDone
	{
		get
		{
			return _bFinish;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp__get_progress.c RVA 0x17BBBB0
	// 1-1: if (_bFinish) return 1.0f;
	//      if (_request != null) return RequestFile.get_progress(_request);
	//      return 0.0f;
	public override float progress
	{
		get
		{
			if (_bFinish)
			{
				return 1.0f;
			}
			if (_request != null)
			{
				return _request.progress;
			}
			return 0.0f;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp__get_error.c RVA 0x17BBBD8
	// 1-1: return *(this + 0x48);   // = _error
	public override string error
	{
		get
		{
			return _error;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp__get_bundleBytes.c RVA 0x17BBBE0
	// 1-1: return *(this + 0x40);   // = _bundleBytes
	public override long bundleBytes
	{
		get
		{
			return _bundleBytes;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp__get_requestPath.c RVA 0x17BBBE8
	// 1-1: return *(this + 0x58);   // = _requestPath
	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp__set_requestPath.c RVA 0x17BBBF0
	// 1-1 logic:
	//   _requestPath = value;
	//   if (string.IsNullOrEmpty(value)) return;
	//   _error = null;
	//   _bFinish = false;
	//   if (_requestNumber > 9) {
	//       _waitRequest.Add(this);   // queue for later
	//   } else {
	//       RunRequest();              // execute now
	//   }
	public string requestPath
	{
		get
		{
			return _requestPath;
		}
		set
		{
			_requestPath = value;
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			_error = null;
			_bFinish = false;
			if (_requestNumber > 9)
			{
				if (_waitRequest == null) throw new System.NullReferenceException();
				_waitRequest.Add(this);
			}
			else
			{
				RunRequest();
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp__get_values.c RVA 0x17BC1F8
	// 1-1 logic (collects loaded assets from _bundleOP for each name):
	//   if (_bundleOP == null || _bundleOP.isStreamedSceneAssetBundle) return null;
	//   if (_names == null) NRE;
	//   var result = new Object[_names.Length];
	//   for (int i = 0; i < _names.Length; i++) {
	//       Object obj;
	//       if (_asType == 0x20 || _asType == 1) obj = _bundleOP.Load<Sprite>(_names[i]);
	//       else                                  obj = _bundleOP.Load(_names[i]);
	//       result[i] = obj;
	//       AssetCacheManager (static cacheMgr).Add(_asType, _names[i], obj);
	//   }
	//   return result;
	// Note: 0x20 = SCENE_TEXTURE / specific asset type that uses Load<Sprite>; 1 = UIATLASES_SHARED.
	// Per Ghidra: `AssetBundleOP__Load<object>` for asType==0x20/1, plain `AssetBundleOP__Load` otherwise.
	public override Object[] values
	{
		get
		{
			if (_bundleOP == null)
			{
				return null;
			}
			if (_bundleOP.isStreamedSceneAssetBundle)
			{
				return null;
			}
			if (_names == null) throw new System.NullReferenceException();
			Object[] result = new Object[_names.Length];
			for (int i = 0; i < _names.Length; i++)
			{
				Object obj;
				// Per Ghidra: _asType comparison vs 0x20 (SCENE_TEXTURE? high enum) and 1 (UIATLASES_SHARED)
				// chooses Load<Sprite> path vs generic Load.
				if ((int)_asType == 0x20 || (int)_asType == 1)
				{
					obj = _bundleOP.Load<Sprite>(_names[i]);
				}
				else
				{
					obj = _bundleOP.Load(_names[i]);
				}
				result[i] = obj;
				if (ResourcesLoader.cacheMgr == null) throw new System.NullReferenceException();
				ResourcesLoader.cacheMgr.Add(_asType, _names[i], obj);
			}
			return result;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp__RunRequest.c RVA 0x17BBD6C
	// 1-1 logic:
	//   var bm = AssetBundleManager.Instance (static at PTR_DAT_034480f8 +0xb8);
	//   var cb = new AssetBundleManager.CBAssetBundle(this, this.Callback);  // delegate to instance.Callback
	//   if (bm != null) {
	//       _request = bm.LoadAssetBundle(_requestPath, cb, 0, 0);
	//       if (_request != null) {
	//           _bundleBytes = _request.bundleSize;
	//           _requestNumber++;
	//           return;
	//       }
	//       // _request null path: failure
	//       _bFinish = true;
	//       if (_callback != null) _callback.Invoke(null);
	//       _error = "Failed to load AssetBundle " + _requestPath + "...";  // (StringLit_3120 prefix + path + StringLit_266 suffix)
	//       if (_names != null) {
	//           string msg = string.Format("Get assetType : {0},name : {1},state : {2}", _names[0], _asType, GET_FROM.ASSETBUNDLE_NOT_FOUND=0xB);
	//           ResourcesLoader.printResourcesLoaderMsg(GET_FROM.ASSETBUNDLE_NOT_FOUND, msg, this);
	//           // Pop next from _waitRequest if any, then RunRequest on it
	//           if (_waitRequest.Count >= 1) {
	//               var next = _waitRequest[0]; _waitRequest.RemoveAt(0);
	//               if (next != null) next.RunRequest();
	//           }
	//           return;
	//       }
	//   }
	//   throw NRE;
	private void RunRequest()
	{
		var bm = AssetBundleManager.Instance;
		var cb = new AssetBundleManager.CBAssetBundle(this.Callback);
		if (bm != null)
		{
			// Ghidra: bm.LoadAssetBundle(_requestPath, cb, 0, 0) — last 2 zero args are
			// implicit MethodInfo* + maybe bForceUseBundle. C# overload: (name, cb, bForceUseBundle=false).
			_request = bm.LoadAssetBundle(_requestPath, cb, false);
			if (_request != null)
			{
				_bundleBytes = _request.bundleSize;
				_requestNumber++;
				return;
			}
			// failure path
			_bFinish = true;
			if (_callback != null)
			{
				_callback(null);
			}
			_error = "Failed to load AssetBundle " + _requestPath + "...";  // 1-1 with StringLit_3120 + _requestPath + StringLit_266
			if (_names != null)
			{
				string assetTypeName = _asType.ToString();
				string msg = string.Format("Get assetType : {0},name : {1},state : {2}", assetTypeName, _names[0], "ASSETBUNDLE_NOT_FOUND");
				ResourcesLoader.printResourcesLoaderMsg(ResourcesLoader.GET_FROM.ASSETBUNDLE_NOT_FOUND, msg, this);
				if (_waitRequest != null && _waitRequest.Count >= 1)
				{
					var next = _waitRequest[0];
					_waitRequest.RemoveAt(0);
					if (next != null)
					{
						next.RunRequest();
					}
				}
				return;
			}
		}
		throw new System.NullReferenceException();
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp___ctor.c RVA 0x17BC13C
	// 1-1 field-init order:
	//   _bFinish = true;                              // *(this + 0x28) = 1
	//   _error = StringLiteral_18867;                 // *(this + 0x48) = "" (assumed empty string literal)
	//   System_Object___ctor(this, 0);                // base.ctor()
	//   _names = names;                               // *(this + 0x18)
	//   _callback = cb;                               // *(this + 0x20)
	//   _error = errorMsg;                            // *(this + 0x48) overwrites with param
	//   _asType = asType;                             // *(this + 0x50)
	// NOTE: _error is set twice (literal then param). Net effect = _error = errorMsg.
	public NewObjectAsyncOp(string[] names, CBNewObjectLoader cb, string errorMsg = "", ResourcesLoader.AssetType asType = ResourcesLoader.AssetType.DEFAULT)
		: base()
	{
		_bFinish = true;
		_names = names;
		_callback = cb;
		_error = errorMsg;
		_asType = asType;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp__Callback.c RVA 0x17BC3D4
	// 1-1 logic:
	//   _bundleOP = bundleOP;
	//   Object[] vals = null;
	//   if (_bundleOP != null && !_bundleOP.isStreamedSceneAssetBundle) {
	//       vals = this.values;   // virtual call (slot at *(this + 0x218))
	//       if (vals != null && vals.Length > 0 && (UnityEngine.Object)vals[0] == null) {
	//           _error = "Failed to load AssetBundle " + _requestPath + "...";
	//           vals = null;
	//       }
	//   }
	//   _bFinish = true;
	//   if (_callback != null) _callback.Invoke(vals);
	//   _callback = null;
	//   _request = null;
	//   if (_names != null) _requestNumber--;
	//   _totalProcessed++;
	//   if (_waitRequest != null && _waitRequest.Count >= 1) {
	//       var next = _waitRequest[0]; _waitRequest.RemoveAt(0);
	//       if (next != null) next.RunRequest();
	//   }
	public void Callback(AssetBundleOP bundleOP)
	{
		_bundleOP = bundleOP;
		Object[] vals = null;
		if (_bundleOP != null && !_bundleOP.isStreamedSceneAssetBundle)
		{
			vals = this.values;
			if (vals != null && vals.Length > 0 && (UnityEngine.Object)vals[0] == null)
			{
				_error = "Failed to load AssetBundle " + _requestPath + "...";
				vals = null;
			}
		}
		_bFinish = true;
		if (_callback != null)
		{
			_callback(vals);
		}
		_callback = null;
		_request = null;
		if (_names != null)
		{
			_requestNumber--;
		}
		_totalProcessed++;
		if (_waitRequest != null && _waitRequest.Count >= 1)
		{
			var next = _waitRequest[0];
			_waitRequest.RemoveAt(0);
			if (next != null)
			{
				next.RunRequest();
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp__RequestFirst.c RVA 0x17BC6AC
	// 1-1 logic:
	//   if (_waitRequest != null && _waitRequest.Remove(this)) {
	//       _waitRequest.Add(this);   // move to back of queue
	//   }
	public void RequestFirst()
	{
		if (_waitRequest == null) throw new System.NullReferenceException();
		if (_waitRequest.Remove(this))
		{
			_waitRequest.Add(this);
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp__ImmDestroy.c RVA 0x17BC7D0
	// 1-1: if (_request != null) _request.ImmDestroy();
	public override void ImmDestroy()
	{
		if (_request != null)
		{
			_request.ImmDestroy();
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/NewObjectAsyncOp___cctor.c RVA 0x17BC7E4
	// 1-1: _waitRequest = new List<NewObjectAsyncOp>(); _totalProcessed = 0;
	static NewObjectAsyncOp()
	{
		_waitRequest = new List<NewObjectAsyncOp>();
		_totalProcessed = 0u;
	}
}
