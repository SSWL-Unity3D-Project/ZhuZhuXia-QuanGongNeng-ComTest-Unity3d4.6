﻿using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public class HardWareTest : MonoBehaviour 
{
	public UILabel[] m_Label;
	public UILabel m_HitTimmerSet;
	public UISlider m_HitTimmerValue;
	public UILabel m_FallTimmerSet;
	public UISlider m_FallTimmerValue;

	public UILabel TouBiLabel;
	public UILabel ShaCheLabel;
	public UILabel AnJianLabel;
	public UILabel YouMenLabel;
	public UILabel FangXiangLabel;
	public static bool IsTestHardWare;
	public static HardWareTest Instance;
	void Start ()
	{
		Instance = this;
		JiaMiJiaoYanCtrlObj.SetActive(IsJiaMiTest);
		IsTestHardWare = true;
		AnJianLabel.text = "";
		//InputEventCtrl.GetInstance().ClickSetEnterBtEvent += ClickSetEnterBtEvent;
		//InputEventCtrl.GetInstance().ClickSetMoveBtEvent += ClickSetMoveBtEvent;
		//InputEventCtrl.GetInstance().ClickStartBtOneEvent += ClickStartBtOneEvent;
		//InputEventCtrl.GetInstance().ClickCloseDongGanBtEvent += ClickCloseDongGanBtEvent;
		//InputEventCtrl.GetInstance().ClickLaBaBtEvent += ClickLaBaBtEvent;
		pcvr.GetInstance();
		pcvr.CloseFangXiangPanPower();
	}
	public UILabel BeiYongYouMenLabel;
	void Update () 
	{
        //TouBiLabel.text = GlobalData.CoinCur.ToString();
        //ShaCheLabel.text = pcvr.ShaCheCurPcvr.ToString();
        //YouMenLabel.text = pcvr.BikePowerCurPcvr.ToString();
        //BeiYongYouMenLabel.text = pcvr.BikeBeiYongPowerCurPcvr.ToString();
        //FangXiangLabel.text = pcvr.SteerValCur.ToString();

        //m_HitTimmerSet.text = ((float)(Convert.ToDouble(m_HitTimmerValue.value)*5.0f)).ToString();
        //m_FallTimmerSet.text = ((float)(Convert.ToDouble(m_FallTimmerValue.value)*5.0f)).ToString();
        //m_HitshakeTimmerSet = (float)(Convert.ToDouble(m_HitTimmerSet.text));
        //OnShakeHit();
        GetMessage();
    }

    void GetMessage()
    {
        if (!MyCOMDevice.ComThreadClass.IsReadComMsg)
        {
            return;
        }

        if (MyCOMDevice.ComThreadClass.IsReadMsgComTimeOut)
        {
            return;
        }

        if (MyCOMDevice.ComThreadClass.ReadByteMsg.Length < (MyCOMDevice.ComThreadClass.BufLenRead - MyCOMDevice.ComThreadClass.BufLenReadEnd))
        {
            //Debug.Log("ReadBufLen was wrong! len is "+MyCOMDevice.ComThreadClass.ReadByteMsg.Length);
            return;
        }
        UpdateDianWeiQiDt(MyCOMDevice.ComThreadClass.ReadByteMsg);
        UpdateBiZhiDt(MyCOMDevice.ComThreadClass.ReadByteMsg);
    }

    /// <summary>
    /// DianWeiQiLb[x]: 0 1px, 1 1py.
    /// </summary>
    public UILabel[] DianWeiQiLb;
    void UpdateDianWeiQiDt(byte[] buffer)
    {
        DianWeiQiLb[0].text = ((((uint)buffer[2] & 0x0f) << 8) + buffer[3]).ToString();
        DianWeiQiLb[1].text = ((((uint)buffer[4] & 0x0f) << 8) + buffer[5]).ToString();
        DianWeiQiLb[2].text = ((((uint)buffer[6] & 0x0f) << 8) + buffer[7]).ToString();
        DianWeiQiLb[3].text = ((((uint)buffer[8] & 0x0f) << 8) + buffer[9]).ToString();
        DianWeiQiLb[4].text = ((((uint)buffer[10] & 0x0f) << 8) + buffer[11]).ToString();
        DianWeiQiLb[5].text = ((((uint)buffer[12] & 0x0f) << 8) + buffer[13]).ToString();
        DianWeiQiLb[6].text = ((((uint)buffer[14] & 0x0f) << 8) + buffer[15]).ToString();
        DianWeiQiLb[7].text = ((((uint)buffer[16] & 0x0f) << 8) + buffer[17]).ToString();
    }

    /// <summary>
    /// BiZhiLb[x]: 0 币值1, 1 币值2.
    /// </summary>
    public UILabel[] BiZhiLb;
    void UpdateBiZhiDt(byte[] buffer)
    {
        BiZhiLb[0].text = buffer[18].ToString("X2");
        BiZhiLb[1].text = buffer[19].ToString("X2");
    }

    /// <summary>
    /// BianMaQiLb[x]: 0 编码器1, 1 编码器2.
    /// </summary>
    public UILabel[] BianMaQiLb;
    void UpdateBianMaQiLbDt(byte[] buffer)
    {
        BianMaQiLb[0].text = buffer[30].ToString("X2");
        BianMaQiLb[1].text = buffer[31].ToString("X2");
    }


    /// <summary>
    /// AnJianLb[x]: 0 按键1, 1 按键2.
    /// </summary>
    public UILabel[] AnJianLb;
    enum AnJianIndex
    {
        Null = 0,
        bt01 = 1, //按键1
        bt02 = 2,
        bt03 = 3,
        bt04 = 4,
        bt05 = 5,
        bt06 = 6,
        bt07 = 7,
        bt08 = 8,
        bt09 = 9,
        bt10 = 10,
        bt11 = 11,
        bt12 = 12,
        bt13 = 13,
        bt14 = 14,
        bt15 = 15,
    }

    class AnJianDt
    {
        /// <summary>
        /// 按键索引
        /// </summary>
        public AnJianIndex IndexAnJian = AnJianIndex.Null; //按键索引.
        /// <summary>
        /// 有效数据
        /// </summary>
        public byte YouXiaoDt = 21; //有效数据.
        /// <summary>
        /// 按键数据
        /// </summary>
        public byte AnJianVal = 20; //按键数据.
        /// <summary>
        /// 有效数据检测01
        /// </summary>
        public byte YouXiao_01 = 0x10; //有效按键数据检测01
        /// <summary>
        /// 有效数据检测02
        /// </summary>
        public byte YouXiao_02 = 0x40; //有效按键数据检测02
        /// <summary>
        /// 按键检测数据
        /// </summary>
        public byte AnJianKey_01 = 0x00; //按键检测数据.
        /// <summary>
        /// 按键检测数据
        /// </summary>
        public byte AnJianKey_02 = 0x00; //按键检测数据.
        /// <summary>
        /// 按键数据文本索引
        /// </summary>
        public byte IndexAnJianTx = 0; //按键数据文本索引.
        public AnJianDt(AnJianIndex indexAnJian, byte youXiaoDt, byte anJianVal, byte youXiao_01, byte youXiao_02, byte anJianKey_01, byte anJianKey_02)
        {
            IndexAnJian = indexAnJian;
            YouXiaoDt = youXiaoDt;
            AnJianVal = anJianVal;
            YouXiao_01 = youXiao_01;
            YouXiao_02 = youXiao_02;
            AnJianKey_01 = anJianKey_01;
            AnJianKey_02 = anJianKey_02;
        }
    }
    /// <summary>
    /// 按键状态.
    /// </summary>
    byte[] AnJianState = new byte[15];
    void CheckAnJianDt(AnJianDt anJianDtVal)
    {
        byte indexVal = (byte)anJianDtVal.IndexAnJian;
        indexVal -= 1;
        if ((anJianDtVal.YouXiaoDt & anJianDtVal.YouXiao_01) == anJianDtVal.YouXiao_01 && (anJianDtVal.YouXiaoDt & anJianDtVal.YouXiao_02) != anJianDtVal.YouXiao_02)
        {
            //按键有效位01.
            if ((anJianDtVal.AnJianVal & anJianDtVal.AnJianKey_01) == anJianDtVal.AnJianKey_01 && AnJianState[indexVal] == 0)
            {
                AnJianState[indexVal] = 1;
                UpdateAnJianLbInfo(anJianDtVal.IndexAnJian, ButtonState.DOWN);
            }
            else if ((anJianDtVal.AnJianVal & anJianDtVal.AnJianKey_01) == 0x00 && AnJianState[indexVal] == 1)
            {
                AnJianState[indexVal] = 0;
                UpdateAnJianLbInfo(anJianDtVal.IndexAnJian, ButtonState.UP);
            }
        }

        if ((anJianDtVal.YouXiaoDt & anJianDtVal.YouXiao_01) != anJianDtVal.YouXiao_01 && (anJianDtVal.YouXiaoDt & anJianDtVal.YouXiao_02) == anJianDtVal.YouXiao_02)
        {
            //按键有效位02.
            if ((anJianDtVal.AnJianVal & anJianDtVal.AnJianKey_02) == anJianDtVal.AnJianKey_02 && AnJianState[indexVal] == 0)
            {
                AnJianState[indexVal] = 1;
                UpdateAnJianLbInfo(anJianDtVal.IndexAnJian, ButtonState.DOWN);
            }
            else if ((anJianDtVal.AnJianVal & anJianDtVal.AnJianKey_02) == 0x00 && AnJianState[indexVal] == 1)
            {
                AnJianState[indexVal] = 0;
                UpdateAnJianLbInfo(anJianDtVal.IndexAnJian, ButtonState.UP);
            }
        }
    }

    void UpdateAnJianLbInfo(AnJianIndex indexAnJian, ButtonState btState)
    {
        byte indexVal = (byte)indexAnJian;
        indexVal -= 1;
        switch (btState)
        {
            case ButtonState.DOWN:
                {
                    AnJianLb[indexVal].text = "1";
                    break;
                }
            case ButtonState.UP:
                {
                    AnJianLb[indexVal].text = "0";
                    break;
                }
        }
    }

    void UpdateAnJianLbDt(byte[] buffer)
    {
        //byte youXiao_01 = 0x10; //有效数据01
        //byte youXiao_02 = 0x40; //有效数据02
        //byte anJianKey = 0x00; //按键数据.
        //byte indexYouXiao = 21; //有效数据索引.
        //byte indexAnJian = 20; //按键数据索引.
        //byte indexAnJianTx = 0; //按键数据文本索引.

        //按键1（投币3）
        AnJianDt anJianDtVal = new AnJianDt(AnJianIndex.bt01, buffer[21], buffer[20], 0x10, 0x40, 0x40, 0x10);
        CheckAnJianDt(anJianDtVal);

        //按键2（投币4）
        anJianDtVal = new AnJianDt(AnJianIndex.bt02, buffer[22], buffer[24], 0x10, 0x40, 0x20, 0x80);
        CheckAnJianDt(anJianDtVal);

        //按键3（开始1）
        //anJianDtVal = new AnJianDt(AnJianIndex.bt03, buffer[22], buffer[35], 0x10, 0x40, 0x20, 0x80);
        //CheckAnJianDt(anJianDtVal);

        //按键4（开始2）
        anJianDtVal = new AnJianDt(AnJianIndex.bt04, buffer[51], buffer[38], 0x04, 0x10, 0x04, 0x10);
        CheckAnJianDt(anJianDtVal);

        //按键5（开始3）
        anJianDtVal = new AnJianDt(AnJianIndex.bt05, buffer[37], buffer[42], 0x02, 0x20, 0x08, 0x04);
        CheckAnJianDt(anJianDtVal);

        //按键6（开始4）
        anJianDtVal = new AnJianDt(AnJianIndex.bt06, buffer[39], buffer[43], 0x02, 0x80, 0x01, 0x02);
        CheckAnJianDt(anJianDtVal);

        //按键7（设置）
        anJianDtVal = new AnJianDt(AnJianIndex.bt07, buffer[36], buffer[40], 0x04, 0x10, 0x04, 0x10);
        CheckAnJianDt(anJianDtVal);

        //按键8（移动）
        anJianDtVal = new AnJianDt(AnJianIndex.bt08, buffer[25], buffer[27], 0x10, 0x40, 0x02, 0x10);
        CheckAnJianDt(anJianDtVal);

        //按键9（彩票1）
        anJianDtVal = new AnJianDt(AnJianIndex.bt09, buffer[28], buffer[32], 0x01, 0x80, 0x04, 0x20);
        CheckAnJianDt(anJianDtVal);

        //按键10（彩票2）
        anJianDtVal = new AnJianDt(AnJianIndex.bt10, buffer[34], buffer[29], 0x01, 0x80, 0x01, 0x08);
        CheckAnJianDt(anJianDtVal);

        //按键1（投币3）
        //if ((buffer[indexYouXiao] & youXiao_01) == youXiao_01 && (buffer[indexYouXiao] & youXiao_02) != youXiao_02)
        //{
        //    anJianKey = 0x40;
        //    if ((buffer[indexAnJian] & anJianKey) == anJianKey && AnJianLb[indexAnJianTx].text == "0")
        //    {
        //        AnJianLb[indexAnJianTx].text = "1";
        //    }
        //    else if ((buffer[indexAnJian] & anJianKey) == 0x00 && AnJianLb[indexAnJianTx].text == "1")
        //    {
        //        AnJianLb[indexAnJianTx].text = "0";
        //    }
        //}

        //if ((buffer[indexYouXiao] & youXiao_01) != youXiao_01 && (buffer[indexYouXiao] & youXiao_02) == youXiao_02)
        //{
        //    anJianKey = 0x10;
        //    if ((buffer[indexAnJian] & anJianKey) == anJianKey && AnJianLb[indexAnJianTx].text == "0")
        //    {
        //        AnJianLb[indexAnJianTx].text = "1";
        //    }
        //    else if ((buffer[indexAnJian] & anJianKey) == 0x00 && AnJianLb[indexAnJianTx].text == "1")
        //    {
        //        AnJianLb[indexAnJianTx].text = "0";
        //    }
        //}

        //按键2（投币4）
        //if ((buffer[22] & 0x10) == 0x10 && (buffer[22] & 0x40) != 0x40)
        //{
        //    if ((buffer[24] & 0x20) == 0x20 && AnJianLb[1].text == "0")
        //    {
        //        AnJianLb[1].text = "1";
        //    }
        //    else if ((buffer[24] & 0x20) == 0x00 && AnJianLb[1].text == "1")
        //    {
        //        AnJianLb[1].text = "0";
        //    }
        //}

        //if ((buffer[22] & 0x10) != 0x10 && (buffer[22] & 0x40) == 0x40)
        //{
        //    if ((buffer[24] & 0x80) == 0x80 && AnJianLb[1].text == "0")
        //    {
        //        AnJianLb[1].text = "1";
        //    }
        //    else if ((buffer[24] & 0x80) == 0x00 && AnJianLb[1].text == "1")
        //    {
        //        AnJianLb[1].text = "0";
        //    }
        //}

        //按键3（开始1）
        //if ((buffer[22] & 0x10) == 0x10 && (buffer[22] & 0x40) != 0x40)
        //{
        //    if ((buffer[24] & 0x20) == 0x20 && AnJianLb[1].text == "0")
        //    {
        //        AnJianLb[1].text = "1";
        //    }
        //    else if ((buffer[24] & 0x20) == 0x00 && AnJianLb[1].text == "1")
        //    {
        //        AnJianLb[1].text = "0";
        //    }
        //}

        //if ((buffer[22] & 0x10) != 0x10 && (buffer[22] & 0x40) == 0x40)
        //{
        //    if ((buffer[24] & 0x80) == 0x80 && AnJianLb[1].text == "0")
        //    {
        //        AnJianLb[1].text = "1";
        //    }
        //    else if ((buffer[24] & 0x80) == 0x00 && AnJianLb[1].text == "1")
        //    {
        //        AnJianLb[1].text = "0";
        //    }
        //}


        //按键4（开始2）
        //if ((buffer[22] & 0x10) == 0x10 && (buffer[22] & 0x40) != 0x40)
        //{
        //    if ((buffer[24] & 0x20) == 0x20 && AnJianLb[1].text == "0")
        //    {
        //        AnJianLb[1].text = "1";
        //    }
        //    else if ((buffer[24] & 0x20) == 0x00 && AnJianLb[1].text == "1")
        //    {
        //        AnJianLb[1].text = "0";
        //    }
        //}

        //if ((buffer[22] & 0x10) != 0x10 && (buffer[22] & 0x40) == 0x40)
        //{
        //    if ((buffer[24] & 0x80) == 0x80 && AnJianLb[1].text == "0")
        //    {
        //        AnJianLb[1].text = "1";
        //    }
        //    else if ((buffer[24] & 0x80) == 0x00 && AnJianLb[1].text == "1")
        //    {
        //        AnJianLb[1].text = "0";
        //    }
        //}
    }

    void ClickSetEnterBtEvent(ButtonState val)
	{
		if (val == ButtonState.DOWN) {
			AnJianLabel.text = "SetEnter Down";
		}
		else {
			AnJianLabel.text = "SetEnter Up";
		}
	}
	void ClickLaBaBtEvent(ButtonState val)
	{
		if (val == ButtonState.DOWN) {
			AnJianLabel.text = "SpeakerBtDown";
		}
		else {
			AnJianLabel.text = "SpeakerBtUp";
		}
	}
	void ClickSetMoveBtEvent(ButtonState val)
	{
		if (val == ButtonState.DOWN) {
			AnJianLabel.text = "SetMove Down";
		}
		else {
			AnJianLabel.text = "SetMove Up";
		}
	}
	//void ClickStartBtOneEvent(ButtonState val)
	//{
	//	if (val == ButtonState.DOWN) {
	//		AnJianLabel.text = "StartBt Down";
	//	}
	//	else {
	//		AnJianLabel.text = "StartBt Up";
	//	}
	//}
	void ClickCloseDongGanBtEvent(ButtonState val)
	{
		if (val == ButtonState.DOWN) {
			AnJianLabel.text = "DongGanBt Down";
		}
		else {
			AnJianLabel.text = "DongGanBt Up";
		}
	}
	//public void OnClickForwardBt()
	//{
		//if(pcvr.m_IsOpneQinang3)
		//{
		//	pcvr.m_IsOpneQinang3 = false;
		//	m_Label[0].text = "OffQN3";
		//}
		//else
		//{
		//	pcvr.m_IsOpneQinang3 = true;
		//	m_Label[0].text = "OpenQN3";
		//}
	//}
	//public void OnClickBehindBt()
	//{
		//if(pcvr.m_IsOpneQinang4)
		//{
		//	pcvr.m_IsOpneQinang4 = false;
		//	m_Label[1].text = "OffQN4";
		//}
		//else
		//{
		//	pcvr.m_IsOpneQinang4 = true;
		//	m_Label[1].text = "OpenQN4";
		//}
	//}
	//public void OnClickLeftBt()
	//{
		//if(pcvr.m_IsOpneQinang1)
		//{
		//	pcvr.m_IsOpneQinang1 = false;
		//	m_Label[2].text = "OffQN1";
		//}
		//else
		//{
		//	pcvr.m_IsOpneQinang1 = true;
		//	m_Label[2].text = "OpenQN1";
		//}
	//}
	//public void OnClickRightBt()
	//{
		//if(pcvr.m_IsOpneQinang2)
		//{
		//	pcvr.m_IsOpneQinang2 = false;
		//	m_Label[3].text = "OffQN2";
		//}
		//else
		//{
		//	//pcvr.m_IsOpneQinang2 = true;
		//	m_Label[3].text = "OpenQN2";
		//}
	//}
	public void OnClickSubCoinBt()
	{
		pcvr.GetInstance().SubPlayerCoin(1);
	}
	//public UILabel ShaCheDengLabel;
	//int ShaCheCount;
	//public void OnClickShaCheLightBt()
	//{
	//	ShaCheCount++;
	//	switch (ShaCheCount) {
	//	case 0:
	//		ShaCheDengLabel.text = "刹车灯灭";
	//		//pcvr.ShaCheBtLight = StartLightState.Mie;
	//		break;

	//	case 1:
	//		ShaCheDengLabel.text = "刹车灯半亮";
	//		//pcvr.ShaCheBtLight = StartLightState.Shan;
	//		break;
			
	//	case 2:
	//		ShaCheDengLabel.text = "刹车灯全亮";
	//		//pcvr.ShaCheBtLight = StartLightState.Liang;
	//		ShaCheCount = -1;
	//		break;
	//	}
	//}
	public void OnClickCloseAppBt()
	{
		Application.Quit();
	}
	
	public bool IsJiaMiTest = false;
	public GameObject JiaMiJiaoYanCtrlObj;
	public void OnClickRestartAppBt()
	{
		Application.Quit();
		RunCmd("start ComTest.exe");
	}
	void RunCmd(string command)
	{
		//實例一個Process類，啟動一個獨立進程    
		Process p = new Process();    //Process類有一個StartInfo屬性，這個是ProcessStartInfo類，    
		//包括了一些屬性和方法，下面我們用到了他的幾個屬性：   
		p.StartInfo.FileName = "cmd.exe";           //設定程序名   
		p.StartInfo.Arguments = "/c " + command;    //設定程式執行參數   
		p.StartInfo.UseShellExecute = false;        //關閉Shell的使用    p.StartInfo.RedirectStandardInput = true;   //重定向標準輸入    p.StartInfo.RedirectStandardOutput = true;  //重定向標準輸出   
		p.StartInfo.RedirectStandardError = true;   //重定向錯誤輸出    
		p.StartInfo.CreateNoWindow = true;          //設置不顯示窗口    
		p.Start();   //啟動
		
		//p.WaitForInputIdle();
		//MoveWindow(p.MainWindowHandle, 1000, 10, 300, 200, true);
		
		//p.StandardInput.WriteLine(command); //也可以用這種方式輸入要執行的命令    
		//p.StandardInput.WriteLine("exit");        //不過要記得加上Exit要不然下一行程式執行的時候會當機    return p.StandardOutput.ReadToEnd();        //從輸出流取得命令執行結果
	}
	public float m_HitshakeTimmerSet = 1.0f;
	private float m_HitshakeTimmer = 0.0f;
	public static bool m_IsHitshake = false;
	public void OnHitShake()
	{
		pcvr.GetInstance().OpenFangXiangPanZhenDong();
		if (m_IsHitshake) {
			return;
		}
		//UnityEngine.Debug.Log("OnHitShake...");
		m_IsHitshake = true;
	}
	void TestLoopOpenFangXiangPanZhenDong()
	{
//		UnityEngine.Debug.Log("TestLoopOpenFangXiangPanZhenDong...");
		pcvr.GetInstance().OpenFangXiangPanZhenDong();
	}
	void OnShakeHit()
	{
		if(m_IsHitshake)
		{
			if(m_HitshakeTimmer<m_HitshakeTimmerSet)
			{
				m_HitshakeTimmer+=Time.deltaTime;
				if(m_HitshakeTimmer<m_HitshakeTimmerSet*0.25f || (m_HitshakeTimmer>=m_HitshakeTimmerSet*0.5f && m_HitshakeTimmer<m_HitshakeTimmerSet*0.75f))
				{
					//pcvr.m_IsOpneForwardQinang = false;
					//pcvr.m_IsOpneBehindQinang = false;
					//pcvr.m_IsOpneLeftQinang = false;
					//pcvr.m_IsOpneRightQinang = true;
				}
				else if((m_HitshakeTimmer>=m_HitshakeTimmerSet*0.25f &&m_HitshakeTimmer<m_HitshakeTimmerSet*0.5f) || m_HitshakeTimmer>=m_HitshakeTimmerSet*0.75f)
				{
					//pcvr.m_IsOpneForwardQinang = false;
					//pcvr.m_IsOpneBehindQinang = false;
					//pcvr.m_IsOpneLeftQinang = true;
					//pcvr.m_IsOpneRightQinang = false;
				}
			}
			else
			{
				m_HitshakeTimmer = 0.0f;
                m_IsHitshake = false;
                //pcvr.m_IsOpneForwardQinang = false;
                //pcvr.m_IsOpneBehindQinang = false;
                //pcvr.m_IsOpneLeftQinang = false;
                //pcvr.m_IsOpneRightQinang = false;
            }
		}
	}

	public UILabel StartLightLabel;
	int LightStart = 1;
	public void OnClickStartLightBt()
	{
		LightStart++;
		//Debug.Log("**************LightStart "+LightStart);
		switch (LightStart) {
		case 1:
			StartLightLabel.text = "开始灯亮";
			//pcvr.StartBtLight = StartLightState.Liang;
			break;

		case 2:
			StartLightLabel.text = "开始灯闪";
			//pcvr.StartBtLight = StartLightState.Shan;
			break;

		case 3:
			StartLightLabel.text = "开始灯灭";
			//pcvr.StartBtLight = StartLightState.Mie;
			LightStart = 1;
			break;
		}
	}

	public UILabel FangXiangPanPowerLabel;
	public void OnClickFangXiangPanPowerBt()
	{
		switch (FangXiangPanPowerLabel.text) {
		case "方向盘力关闭":
			FangXiangPanPowerLabel.text = "方向盘力打开";
			pcvr.OpenFangXiangPanPower();
			CancelInvoke("TestLoopOpenFangXiangPanZhenDong");
			InvokeRepeating("TestLoopOpenFangXiangPanZhenDong", 0f, 0.2f);
			break;

		case "方向盘力打开":
			FangXiangPanPowerLabel.text = "方向盘力关闭";
			pcvr.CloseFangXiangPanPower();
			CancelInvoke("TestLoopOpenFangXiangPanZhenDong");
			break;
		}
	}

	public UILabel DongGanLightLabel;
	int LightDongGan = 1;
	public void OnClickDongGanLightBt()
	{
		LightDongGan++;
		//Debug.Log("**************LightDongGan "+LightDongGan);
		switch (LightDongGan) {
		case 1:
			DongGanLightLabel.text = "动感灯亮";
			//pcvr.DongGanBtLight = StartLightState.Liang;
			break;
			
		case 2:
			DongGanLightLabel.text = "动感灯闪";
			//pcvr.DongGanBtLight = StartLightState.Shan;
			break;
			
		case 3:
			DongGanLightLabel.text = "动感灯灭";
			//pcvr.DongGanBtLight = StartLightState.Mie;
			LightDongGan = 1;
			break;
		}
	}
	
	public UILabel JiaMiJYLabel;
	public UILabel JiaMiJYMsg;
	public static bool IsOpenJiaMiJiaoYan;
	void CloseJiaMiJiaoYanFailed()
	{
		if (!IsInvoking("JiaMiJiaoYanFailed")) {
			return;
		}
		CancelInvoke("JiaMiJiaoYanFailed");
	}

	public void OnClickJiaMiJiaoYanBt()
	{
		if (JiaMiJYLabel.text != "开启校验" && !pcvr.IsJiaoYanHid) {
			UnityEngine.Debug.Log("OnClickJiaMiJiaoYanBt...");
			OpenJiaMiJiaoYan();
			JiaMiJYLabel.text = "开启校验";
			SetJiaMiJYMsg("校验中...", JiaMiJiaoYanEnum.Null);
		}
	}
	
	public static void OpenJiaMiJiaoYan()
	{
		if (IsOpenJiaMiJiaoYan) {
			return;
		}
		IsOpenJiaMiJiaoYan = true;
		//Instance.DelayCloseJiaMiJiaoYan();

		pcvr.GetInstance().StartJiaoYanIO();
	}
	
	public void DelayCloseJiaMiJiaoYan()
	{
		CloseJiaMiJiaoYanFailed();
		Invoke("JiaMiJiaoYanFailed", 5f);
	}
	
	public void JiaMiJiaoYanFailed()
	{
		SetJiaMiJYMsg("", JiaMiJiaoYanEnum.Failed);
	}

	public void JiaMiJiaoYanSucceed()
	{
		SetJiaMiJYMsg("", JiaMiJiaoYanEnum.Succeed);
	}
	
	public static void CloseJiaMiJiaoYan()
	{
		if (!IsOpenJiaMiJiaoYan) {
			return;
		}
		IsOpenJiaMiJiaoYan = false;
	}
	
	void ResetJiaMiJYLabelInfo()
	{
		CloseJiaMiJiaoYan();
		JiaMiJYLabel.text = "加密校验";
	}
	
	public void SetJiaMiJYMsg(string msg, JiaMiJiaoYanEnum key)
	{
		switch (key) {
		case JiaMiJiaoYanEnum.Succeed:
			CloseJiaMiJiaoYanFailed();
			JiaMiJYMsg.text = "校验成功";
			ResetJiaMiJYLabelInfo();
			//ScreenLog.Log("校验成功");
			break;
			
		case JiaMiJiaoYanEnum.Failed:
			CloseJiaMiJiaoYanFailed();
			JiaMiJYMsg.text = "校验失败";
			ResetJiaMiJYLabelInfo();
			//ScreenLog.Log("校验失败");
			break;
			
		default:
			JiaMiJYMsg.text = msg;
			//ScreenLog.Log(msg);
			break;
		}
	}

}

public enum JiaMiJiaoYanEnum
{
	Null,
	Succeed,
	Failed,
}