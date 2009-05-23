﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Cosmos.Builder.Common {

	public enum TargetHost
	{
	  [Description("VMWare Workstation")]
	  VMWareWorkstation,
	  [Description("VMWare Server")]
	  VMWareServer,
	  QEMU,
	  [Description("Virtual PC")]
	  VPC,
	  PXE,
	  ISO
	}

	public enum Architecture
	{
	  x86
	  //x64
	}

	public enum Framework
	{
		[Description("Microsoft .NET")]
		MicrosoftNET,
		Mono
	}

	public enum VMQemuNetworkCard
	{
		None,
		[Description("Realtek RTL8139")]
		RealtekRTL8139,
	}

	public enum VMQemuAudioCard
	{
		None,
		[Description("PC Speaker")]
		PCSpeaker
	}

	public enum DebugQemuCommunication
	{
		None,
		[Description("TCP: QEMU as server, Cosmos as client")] 
		TCPListener,
		[Description("TCP: QEMU as client, Cosmos as server")] 
		TCPClient,
		[Description("Named Pipe: QEMU as server, Cosmos as client")]
		NamedPipeListener,
		[Description("Named Pipe: QEMU as client, Cosmos as server")]
		NamedPipeClient
	}

	public class DescriptionAttribute : Attribute
	{
		public static String GetDescription(object value)
		{
			Type valueType = value.GetType();
			MemberInfo[] valueMemberInfo;
			Object[] valueMemberAttribute;

			if (valueType.IsEnum == true)
				{
					valueMemberInfo = valueType.GetMember(value.ToString());

					if ((valueMemberInfo != null) && (valueMemberInfo.Length > 0))
					{
						valueMemberAttribute = valueMemberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute),false);
						if ((valueMemberAttribute != null) && (valueMemberAttribute.Length > 0))
						{
							return ((DescriptionAttribute)valueMemberAttribute[0]).Description;
						}
					}
				}

				valueMemberAttribute = valueType.GetCustomAttributes(typeof(DescriptionAttribute),false);
				if ((valueMemberAttribute != null) && (valueMemberAttribute.Length > 0))
				{
					return ((DescriptionAttribute)valueMemberAttribute[0]).Description;
				}

				return value.ToString();
		}

		private string emDescription;

		public DescriptionAttribute(String description)
		{
			this.emDescription = description;
		}

		public String Description
		{ get { return this.emDescription; } }
	}
}
