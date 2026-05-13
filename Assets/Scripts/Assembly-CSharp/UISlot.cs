// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__*.c — 20 methods ported 1-1.
// Source: dump.cs TypeDefIndex 289 — UISlot : MonoBehaviour
// Field offsets verified against dump.cs; vtable@0x168 on string = String.ToString() (identity for strings).

using Cpp2IlInjected;
using LuaInterface;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
	private SlotData SlotInfo;                       // 0x20

	[NoToLua]
	public AdvImage _Icon;                            // 0x28

	[NoToLua]
	public Image _CDFrame;                            // 0x30

	[NoToLua]
	public Text _itemCount;                           // 0x38

	[NoToLua]
	public Text _RightTopText;                        // 0x40

	[NoToLua]
	public UIImagePicker ColorFrame;                  // 0x48

	[NoToLua]
	public GameObject _lock;                          // 0x50

	[NoToLua]
	public UIImagePicker _LeftBottomPic;              // 0x58

	[NoToLua]
	public UIImagePicker _LeftTopPic;                 // 0x60

	[NoToLua]
	public UIImagePicker _RightTopPic;                // 0x68

	[NoToLua]
	public UIImageColorPicker _RestrictColor;         // 0x70

	[NoToLua]
	public GameObject _Effect1;                       // 0x78

	[NoToLua]
	public GameObject _Effect2;                       // 0x80

	[NoToLua]
	public GameObject _Effect3;                       // 0x88

	private float TotalCDTime;                        // 0x90

	private float remainCDTime;                       // 0x94

	private AssetBundleRequest _IconRequest;          // 0x98

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot___ctor.c RVA 0x01A04324
	public UISlot()
	{
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__Start.c RVA 0x01A02AB4
	private void Start()
	{
		if ((UnityEngine.Object)_CDFrame != null)
		{
			if (_CDFrame == null) throw new System.NullReferenceException();
			_CDFrame.type = (Image.Type)3;
			return;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__Update.c RVA 0x01A02B3C
	private void Update()
	{
		if (_IconRequest == null || !_IconRequest.isDone)
		{
			float fVar6 = remainCDTime;
			if (0.0f < fVar6)
			{
				float fVar5 = Time.deltaTime;
				fVar6 = fVar6 - fVar5;
				if (fVar6 <= 0.0f)
				{
					fVar6 = 0.0f;
				}
				remainCDTime = fVar6;
				UpdateCoolDown();
				return;
			}
			return;
		}
		if (_IconRequest != null)
		{
			AdvImage lVar4 = _Icon;
			UnityEngine.Object plVar2 = _IconRequest.asset;
			if (lVar4 != null)
			{
				Sprite asSprite = plVar2 as Sprite;
				_Icon.sprite = asSprite;
				if (_Icon != null)
				{
					GameObject lVar = _Icon.gameObject;
					if (lVar != null)
					{
						lVar.SetActive(true);
						_IconRequest = null;
						// fallthrough to LAB_01b02bfc — re-check CD tick.
						float fVar6 = remainCDTime;
						if (0.0f < fVar6)
						{
							float fVar5 = Time.deltaTime;
							fVar6 = fVar6 - fVar5;
							if (fVar6 <= 0.0f)
							{
								fVar6 = 0.0f;
							}
							remainCDTime = fVar6;
							UpdateCoolDown();
						}
						return;
					}
				}
			}
		}
		throw new System.NullReferenceException();
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__GetSlotData.c RVA 0x01A02CEC
	public SlotData GetSlotData()
	{
		return SlotInfo;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__ClearSlot.c RVA 0x01A02CF4
	public void ClearSlot()
	{
		SlotInfo = null;
		_IconRequest = null;
		TotalCDTime = 0.0f;

		if ((UnityEngine.Object)_Icon != null)
		{
			if (_Icon == null) throw new System.NullReferenceException();
			GameObject lVar3 = _Icon.gameObject;
			if (lVar3 == null) throw new System.NullReferenceException();
			lVar3.SetActive(false);
			AdvImage plVar4 = _Icon;
			if (plVar4 == null) throw new System.NullReferenceException();
			plVar4.color = Color.white;
		}

		if ((UnityEngine.Object)_itemCount != null)
		{
			if (_itemCount == null) throw new System.NullReferenceException();
			_itemCount.text = "";
		}

		if ((UnityEngine.Object)_RightTopText != null)
		{
			if (_RightTopText == null) throw new System.NullReferenceException();
			_RightTopText.text = "";
		}

		SetDark(false);

		if ((UnityEngine.Object)ColorFrame != null)
		{
			if (ColorFrame == null) throw new System.NullReferenceException();
			GameObject lVar3 = ColorFrame.gameObject;
			if (lVar3 == null) throw new System.NullReferenceException();
			lVar3.SetActive(false);
		}

		if ((UnityEngine.Object)_CDFrame != null)
		{
			if (_CDFrame == null) throw new System.NullReferenceException();
			_CDFrame.fillAmount = 0.0f;
		}

		if ((UnityEngine.Object)_lock != null)
		{
			if (_lock == null) throw new System.NullReferenceException();
			_lock.SetActive(false);
		}

		if ((UnityEngine.Object)_LeftBottomPic != null)
		{
			if (_LeftBottomPic == null) throw new System.NullReferenceException();
			GameObject lVar3 = _LeftBottomPic.gameObject;
			if (lVar3 == null) throw new System.NullReferenceException();
			lVar3.SetActive(false);
		}

		if ((UnityEngine.Object)_LeftTopPic != null)
		{
			if (_LeftTopPic == null) throw new System.NullReferenceException();
			GameObject lVar3 = _LeftTopPic.gameObject;
			if (lVar3 == null) throw new System.NullReferenceException();
			lVar3.SetActive(false);
		}

		if ((UnityEngine.Object)_RightTopPic != null)
		{
			if (_RightTopPic == null) throw new System.NullReferenceException();
			GameObject lVar3 = _RightTopPic.gameObject;
			if (lVar3 == null) throw new System.NullReferenceException();
			lVar3.SetActive(false);
		}

		if ((UnityEngine.Object)_RestrictColor != null)
		{
			if (_RestrictColor == null) throw new System.NullReferenceException();
			GameObject lVar3 = _RestrictColor.gameObject;
			if (lVar3 == null) throw new System.NullReferenceException();
			lVar3.SetActive(false);
		}

		if ((UnityEngine.Object)_Effect1 != null)
		{
			if (_Effect1 == null) throw new System.NullReferenceException();
			_Effect1.SetActive(false);
		}

		if ((UnityEngine.Object)_Effect2 != null)
		{
			if (_Effect2 == null) throw new System.NullReferenceException();
			_Effect2.SetActive(false);
		}

		if ((UnityEngine.Object)_Effect3 != null)
		{
			if (_Effect3 == null) return;
			_Effect3.SetActive(false);
			return;
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__UpdateCoolDown.c RVA 0x01A02C48
	private void UpdateCoolDown()
	{
		if ((UnityEngine.Object)_CDFrame == null)
		{
			return;
		}
		float fVar5 = TotalCDTime;
		Image lVar3 = _CDFrame;
		if (fVar5 <= 0.0f)
		{
			fVar5 = 0.0f;
			if (lVar3 == null) throw new System.NullReferenceException();
		}
		else
		{
			if (lVar3 == null) throw new System.NullReferenceException();
			fVar5 = remainCDTime / fVar5;
		}
		lVar3.fillAmount = fVar5;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetCoolDown.c RVA 0x01A031E4
	public void SetCoolDown(float remainTime, float totalTime)
	{
		TotalCDTime = totalTime;
		remainCDTime = remainTime;
		if ((remainTime == 0.0f) || (totalTime == 0.0f))
		{
			if ((UnityEngine.Object)_CDFrame != null)
			{
				if (_CDFrame == null) throw new System.NullReferenceException();
				_CDFrame.fillAmount = 0.0f;
				return;
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetSlotDataLua.c RVA 0x01A03294
	public void SetSlotDataLua(SlotData tempData, int colorTag, int nMode)
	{
		int local_44 = colorTag;
		bool uVar3;
		if (SlotInfo == null)
		{
			uVar3 = true;
		}
		else
		{
			if (tempData == null) throw new System.NullReferenceException();
			uVar3 = !(SlotInfo.strIcon == tempData.strIcon);
		}
		SlotInfo = tempData;

		// ColorFrame @ 0x48
		if ((UnityEngine.Object)ColorFrame != null)
		{
			UIImagePicker lVar10 = ColorFrame;
			if (colorTag < 1)
			{
				if (lVar10 == null) throw new System.NullReferenceException();
				GameObject go = lVar10.gameObject;
				if (go == null) throw new System.NullReferenceException();
				go.SetActive(false);
			}
			else
			{
				string uVar9 = local_44.ToString();
				if (lVar10 == null) throw new System.NullReferenceException();
				lVar10.SetCurTag(uVar9);
				if (ColorFrame == null) throw new System.NullReferenceException();
				GameObject go = ColorFrame.gameObject;
				if (go == null) throw new System.NullReferenceException();
				go.SetActive(true);
			}
		}

		// _Icon @ 0x28
		if ((UnityEngine.Object)_Icon != null)
		{
			if (SlotInfo == null) throw new System.NullReferenceException();
			if (!(SlotInfo.strIcon != ""))
			{
				_IconRequest = null;
				if (_Icon == null) throw new System.NullReferenceException();
				GameObject go = _Icon.gameObject;
				if (go == null) throw new System.NullReferenceException();
				go.SetActive(false);
			}
			else if (uVar3)
			{
				IconTextureMgr mgr = IconTextureMgr.Instance;
				if (SlotInfo == null || mgr == null) throw new System.NullReferenceException();
				AssetBundleRequest req = mgr.LoadIconSpriteAsync(SlotInfo.strIcon, nMode);
				_IconRequest = req;
				if (_IconRequest == null)
				{
					AdvImage lVar8 = _Icon;
					IconTextureMgr mgr2 = IconTextureMgr.Instance;
					if (SlotInfo == null || mgr2 == null) throw new System.NullReferenceException();
					Sprite spr = mgr2.GetIconSprite(SlotInfo.strIcon, nMode);
					if (lVar8 == null) throw new System.NullReferenceException();
					lVar8.sprite = spr;
					if (_Icon == null) throw new System.NullReferenceException();
					GameObject go = _Icon.gameObject;
					if (go == null) throw new System.NullReferenceException();
					go.SetActive(true);
				}
			}
		}

		// _itemCount @ 0x38
		if ((UnityEngine.Object)_itemCount != null)
		{
			if (SlotInfo == null) throw new System.NullReferenceException();
			if (SlotInfo.CountText == "")
			{
				if (SlotInfo == null) throw new System.NullReferenceException();
				int nCount = SlotInfo.nCount;
				Text plVar7 = _itemCount;
				string uVar9;
				if (nCount == 0)
				{
					if (plVar7 == null) throw new System.NullReferenceException();
					uVar9 = "";
				}
				else
				{
					uVar9 = SlotInfo.nCount.ToString();
					if (plVar7 == null) throw new System.NullReferenceException();
				}
				plVar7.text = uVar9;
			}
			else
			{
				if (SlotInfo == null) throw new System.NullReferenceException();
				Text plVar7 = _itemCount;
				if (plVar7 == null) throw new System.NullReferenceException();
				plVar7.text = SlotInfo.CountText;
			}
		}

		// _RightTopText @ 0x40
		if ((UnityEngine.Object)_RightTopText != null)
		{
			if (SlotInfo == null) throw new System.NullReferenceException();
			if (SlotInfo.RightTopText != "")
			{
				if (SlotInfo == null) throw new System.NullReferenceException();
				Text plVar7 = _RightTopText;
				if (plVar7 == null) throw new System.NullReferenceException();
				plVar7.text = SlotInfo.RightTopText;
			}
		}

		// _lock @ 0x50
		if ((UnityEngine.Object)_lock != null)
		{
			if (SlotInfo == null || _lock == null) throw new System.NullReferenceException();
			_lock.SetActive(SlotInfo._bLock);
		}

		// _LeftBottomPic @ 0x58
		if ((UnityEngine.Object)_LeftBottomPic != null)
		{
			if (SlotInfo == null) throw new System.NullReferenceException();
			UIImagePicker lVar10 = _LeftBottomPic;
			if (!(SlotInfo.LeftBottomTag != ""))
			{
				if (lVar10 == null) throw new System.NullReferenceException();
				GameObject go = lVar10.gameObject;
				if (go == null) throw new System.NullReferenceException();
				go.SetActive(false);
			}
			else
			{
				if (SlotInfo == null) throw new System.NullReferenceException();
				string tag = SlotInfo.LeftBottomTag;
				if (tag == null) throw new System.NullReferenceException();
				if (lVar10 == null) throw new System.NullReferenceException();
				lVar10.SetCurTag(tag);
				if (_LeftBottomPic == null) throw new System.NullReferenceException();
				GameObject go = _LeftBottomPic.gameObject;
				if (go == null) throw new System.NullReferenceException();
				go.SetActive(true);
			}
		}

		// _LeftTopPic @ 0x60
		if ((UnityEngine.Object)_LeftTopPic != null)
		{
			if (SlotInfo == null) throw new System.NullReferenceException();
			UIImagePicker lVar10 = _LeftTopPic;
			if (!(SlotInfo.LeftTopTag != ""))
			{
				if (lVar10 == null) throw new System.NullReferenceException();
				GameObject go = lVar10.gameObject;
				if (go == null) throw new System.NullReferenceException();
				go.SetActive(false);
			}
			else
			{
				if (SlotInfo == null) throw new System.NullReferenceException();
				string tag = SlotInfo.LeftTopTag;
				if (tag == null) throw new System.NullReferenceException();
				if (lVar10 == null) throw new System.NullReferenceException();
				lVar10.SetCurTag(tag);
				if (_LeftTopPic == null) throw new System.NullReferenceException();
				GameObject go = _LeftTopPic.gameObject;
				if (go == null) throw new System.NullReferenceException();
				go.SetActive(true);
			}
		}

		// _RightTopPic @ 0x68
		if ((UnityEngine.Object)_RightTopPic != null)
		{
			if (SlotInfo == null) throw new System.NullReferenceException();
			UIImagePicker lVar10 = _RightTopPic;
			if (!(SlotInfo.RightTopTag != ""))
			{
				if (lVar10 == null) throw new System.NullReferenceException();
				GameObject go = lVar10.gameObject;
				if (go == null) throw new System.NullReferenceException();
				go.SetActive(false);
			}
			else
			{
				if (SlotInfo == null) throw new System.NullReferenceException();
				string tag = SlotInfo.RightTopTag;
				if (tag == null) throw new System.NullReferenceException();
				if (lVar10 == null) throw new System.NullReferenceException();
				lVar10.SetCurTag(tag);
				if (_RightTopPic == null) throw new System.NullReferenceException();
				GameObject go = _RightTopPic.gameObject;
				if (go == null) throw new System.NullReferenceException();
				go.SetActive(true);
			}
		}

		// _RestrictColor @ 0x70
		if ((UnityEngine.Object)_RestrictColor != null)
		{
			if (SlotInfo == null) throw new System.NullReferenceException();
			UIImageColorPicker lVar10 = _RestrictColor;
			if (!(SlotInfo.RestrictColorTag != ""))
			{
				if (lVar10 == null) throw new System.NullReferenceException();
				GameObject go = lVar10.gameObject;
				if (go == null) throw new System.NullReferenceException();
				go.SetActive(false);
			}
			else
			{
				if (SlotInfo == null) throw new System.NullReferenceException();
				string tag = SlotInfo.RestrictColorTag;
				if (tag == null) throw new System.NullReferenceException();
				if (lVar10 == null) throw new System.NullReferenceException();
				lVar10.SetCurTag(tag);
				if (_RestrictColor == null) throw new System.NullReferenceException();
				GameObject go = _RestrictColor.gameObject;
				if (go == null) throw new System.NullReferenceException();
				go.SetActive(true);
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetDark.c RVA 0x01A0311C
	public void SetDark(bool bDark)
	{
		if ((UnityEngine.Object)_Icon == null)
		{
			return;
		}
		AdvImage plVar3 = _Icon;
		Color col;
		if (!bDark)
		{
			if (plVar3 == null) throw new System.NullReferenceException();
			col = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
		else
		{
			if (plVar3 == null) throw new System.NullReferenceException();
			col = new Color(0.5f, 0.5f, 0.5f, 1.0f);
		}
		plVar3.color = col;
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetGray.c RVA 0x01A03940
	public void SetGray(bool bgray)
	{
		if ((UnityEngine.Object)_Icon == null)
		{
			return;
		}
		if (_Icon != null)
		{
			float uVar4 = 1.0f;
			if (!bgray)
			{
				uVar4 = 0.0f;
			}
			_Icon.GrayScaleAmount = uVar4;
			return;
		}
		throw new System.NullReferenceException();
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetLock.c RVA 0x01A039E4
	public void SetLock(bool bLock)
	{
		if (SlotInfo != null)
		{
			SlotInfo._bLock = bLock;
			if ((UnityEngine.Object)_lock != null)
			{
				if ((SlotInfo != null) && (_lock != null))
				{
					_lock.SetActive(SlotInfo._bLock);
					return;
				}
				throw new System.NullReferenceException();
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetCount.c RVA 0x01A03A98
	public void SetCount(string strCountText, int nCountNum)
	{
		if (SlotInfo == null)
		{
			return;
		}
		SlotInfo.CountText = strCountText;
		if (SlotInfo != null)
		{
			SlotInfo.nCount = nCountNum;
			if ((UnityEngine.Object)_itemCount == null)
			{
				return;
			}
			if (SlotInfo != null)
			{
				if (SlotInfo.CountText == "")
				{
					if (SlotInfo != null)
					{
						int nCount = SlotInfo.nCount;
						Text plVar7 = _itemCount;
						string uVar8;
						if (nCount == 0)
						{
							if (plVar7 == null) throw new System.NullReferenceException();
							uVar8 = "";
						}
						else
						{
							uVar8 = SlotInfo.nCount.ToString();
							if (plVar7 == null) throw new System.NullReferenceException();
						}
						plVar7.text = uVar8;
						return;
					}
				}
				else
				{
					if ((SlotInfo != null) && (_itemCount != null))
					{
						_itemCount.text = SlotInfo.CountText;
						return;
					}
				}
			}
		}
		throw new System.NullReferenceException();
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetRightTopText.c RVA 0x01A03BDC
	public void SetRightTopText(string strText)
	{
		if (SlotInfo != null)
		{
			SlotInfo.RightTopText = strText;
			if ((UnityEngine.Object)_RightTopText != null)
			{
				if (SlotInfo != null)
				{
					if (!(SlotInfo.RightTopText != ""))
					{
						return;
					}
					if ((SlotInfo != null) && (_RightTopText != null))
					{
						_RightTopText.text = SlotInfo.RightTopText;
						return;
					}
				}
				throw new System.NullReferenceException();
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetLeftBottomPic.c RVA 0x01A03CBC
	public void SetLeftBottomPic(string tagName)
	{
		if (SlotInfo != null)
		{
			SlotInfo.LeftBottomTag = tagName;
			if ((UnityEngine.Object)_LeftBottomPic != null)
			{
				if (SlotInfo != null)
				{
					UIImagePicker lVar6 = _LeftBottomPic;
					if (!(SlotInfo.LeftBottomTag != ""))
					{
						if ((lVar6 != null) && (lVar6.gameObject != null))
						{
							lVar6.gameObject.SetActive(false);
							return;
						}
					}
					else if ((SlotInfo != null) && (SlotInfo.LeftBottomTag != null))
					{
						string uVar5 = SlotInfo.LeftBottomTag;
						if (lVar6 != null)
						{
							lVar6.SetCurTag(uVar5);
							if ((_LeftBottomPic != null) && (_LeftBottomPic.gameObject != null))
							{
								_LeftBottomPic.gameObject.SetActive(true);
								return;
							}
						}
					}
				}
				throw new System.NullReferenceException();
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetLeftTopPic.c RVA 0x01A03DE4
	public void SetLeftTopPic(string tagName)
	{
		if (SlotInfo != null)
		{
			SlotInfo.LeftTopTag = tagName;
			if ((UnityEngine.Object)_LeftTopPic != null)
			{
				if (SlotInfo != null)
				{
					UIImagePicker lVar6 = _LeftTopPic;
					if (!(SlotInfo.LeftTopTag != ""))
					{
						if ((lVar6 != null) && (lVar6.gameObject != null))
						{
							lVar6.gameObject.SetActive(false);
							return;
						}
					}
					else if ((SlotInfo != null) && (SlotInfo.LeftTopTag != null))
					{
						string uVar5 = SlotInfo.LeftTopTag;
						if (lVar6 != null)
						{
							lVar6.SetCurTag(uVar5);
							if ((_LeftTopPic != null) && (_LeftTopPic.gameObject != null))
							{
								_LeftTopPic.gameObject.SetActive(true);
								return;
							}
						}
					}
				}
				throw new System.NullReferenceException();
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetRightTopPic.c RVA 0x01A03F0C
	public void SetRightTopPic(string tagName)
	{
		if (SlotInfo != null)
		{
			SlotInfo.RightTopTag = tagName;
			if ((UnityEngine.Object)_RightTopPic != null)
			{
				if (SlotInfo != null)
				{
					UIImagePicker lVar6 = _RightTopPic;
					if (!(SlotInfo.RightTopTag != ""))
					{
						if ((lVar6 != null) && (lVar6.gameObject != null))
						{
							lVar6.gameObject.SetActive(false);
							return;
						}
					}
					else if ((SlotInfo != null) && (SlotInfo.RightTopTag != null))
					{
						string uVar5 = SlotInfo.RightTopTag;
						if (lVar6 != null)
						{
							lVar6.SetCurTag(uVar5);
							if ((_RightTopPic != null) && (_RightTopPic.gameObject != null))
							{
								_RightTopPic.gameObject.SetActive(true);
								return;
							}
						}
					}
				}
				throw new System.NullReferenceException();
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetRestrictColorTag.c RVA 0x01A04034
	public void SetRestrictColorTag(string tagName)
	{
		if (SlotInfo != null)
		{
			SlotInfo.RestrictColorTag = tagName;
			if ((UnityEngine.Object)_RestrictColor != null)
			{
				if (SlotInfo != null)
				{
					UIImageColorPicker lVar6 = _RestrictColor;
					if (!(SlotInfo.RestrictColorTag != ""))
					{
						if ((lVar6 != null) && (lVar6.gameObject != null))
						{
							lVar6.gameObject.SetActive(false);
							return;
						}
					}
					else if ((SlotInfo != null) && (SlotInfo.RestrictColorTag != null))
					{
						string uVar5 = SlotInfo.RestrictColorTag;
						if (lVar6 != null)
						{
							lVar6.SetCurTag(uVar5);
							if ((_RestrictColor != null) && (_RestrictColor.gameObject != null))
							{
								_RestrictColor.gameObject.SetActive(true);
								return;
							}
						}
					}
				}
				throw new System.NullReferenceException();
			}
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetEffect1.c RVA 0x01A0415C
	public void SetEffect1(bool bShow)
	{
		if ((UnityEngine.Object)_Effect1 != null)
		{
			if (_Effect1 != null)
			{
				_Effect1.SetActive(bShow);
				return;
			}
			throw new System.NullReferenceException();
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetEffect2.c RVA 0x01A041F4
	public void SetEffect2(bool bShow)
	{
		if ((UnityEngine.Object)_Effect2 != null)
		{
			if (_Effect2 != null)
			{
				_Effect2.SetActive(bShow);
				return;
			}
			throw new System.NullReferenceException();
		}
	}

	// Source: Ghidra work/06_ghidra/decompiled_rva/UISlot__SetEffect3.c RVA 0x01A0428C
	public void SetEffect3(bool bShow)
	{
		if ((UnityEngine.Object)_Effect3 != null)
		{
			if (_Effect3 != null)
			{
				_Effect3.SetActive(bShow);
				return;
			}
			throw new System.NullReferenceException();
		}
	}
}
