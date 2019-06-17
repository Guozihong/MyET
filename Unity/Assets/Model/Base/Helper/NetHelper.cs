﻿using System.Collections.Generic;
using System.Net;

namespace ETModel
{
	public static class NetHelper
	{
		public static string[] GetAddressIPs()
		{
			//获取本地的IP地址
			List<string> addressIPs = new List<string>();
			Log.Info("hostName: " + Dns.GetHostName());
			foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
			{
				addressIPs.Add(address.ToString());
			}
			return addressIPs.ToArray();
		}
		public static string GetHostName()
		{
			return Dns.GetHostName();
		}
	}
}
