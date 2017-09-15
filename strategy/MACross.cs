﻿using System;

namespace HaiFeng
{

	public class MACross : Strategy //继承 Strategy 类,并实现 Initialize 和 OnBarUpdate 函数
	{
		public MACross()
		{
		}

		[Parameter("均线1")]
		public int MA1 = 5;
		[Parameter("均线2")]
		public int MA2 = 10;
		[Parameter("手数")]
		public int Lots = 1;

		SMA ma1, ma2;

		public override void Initialize()
		{
			ma1 = SMA(C, MA1);
			ma2 = SMA(C, MA2);
		}


		public override void OnBarUpdate()
		{
			if (CurrentBar <= Math.Max(MA1, MA2))
				return;
			if (PositionLong == 0 && ma1[2].Less(ma2[2]) && ma1[1].GreaterEqual(ma2[1]))
			{
				if (PositionShort > 0)
					BuyToCover(PositionShort, O[0], "开多前先平空");
				Buy(Lots, O[0], "上穿开多,平开的时间差可能因资金不足而导致而失败!");
			}
			else if (PositionShort == 0 && ma1[2].Greater(ma2[2]) && ma1[1].LessEqual(ma2[1]))
			{
				if (PositionLong > 0)
					Sell(PositionLong, O[0], "开空前先平多");

				SellShort(Lots, O[0], "下穿开空,平开的时间差可能因资金不足而导致而失败!");
			}
		}
	}
}
