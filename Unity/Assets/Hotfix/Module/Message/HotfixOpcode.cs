using ETModel;
namespace ETHotfix
{
	[Message(HotfixOpcode.C2R_Login)]
	public partial class C2R_Login : IRequest {}

	[Message(HotfixOpcode.R2C_Login)]
	public partial class R2C_Login : IResponse {}

	[Message(HotfixOpcode.C2G_LoginGate)]
	public partial class C2G_LoginGate : IRequest {}

	[Message(HotfixOpcode.G2C_LoginGate)]
	public partial class G2C_LoginGate : IResponse {}

	[Message(HotfixOpcode.G2C_TestHotfixMessage)]
	public partial class G2C_TestHotfixMessage : IMessage {}

	[Message(HotfixOpcode.C2M_TestActorRequest)]
	public partial class C2M_TestActorRequest : IActorLocationRequest {}

	[Message(HotfixOpcode.M2C_TestActorResponse)]
	public partial class M2C_TestActorResponse : IActorLocationResponse {}

	[Message(HotfixOpcode.PlayerInfo)]
	public partial class PlayerInfo : IMessage {}

	[Message(HotfixOpcode.C2G_PlayerInfo)]
	public partial class C2G_PlayerInfo : IRequest {}

	[Message(HotfixOpcode.G2C_PlayerInfo)]
	public partial class G2C_PlayerInfo : IResponse {}

	[Message(HotfixOpcode.C2R_LandlordsLogin)]
	public partial class C2R_LandlordsLogin : IRequest {}

	[Message(HotfixOpcode.R2C_LandlordsLogin)]
	public partial class R2C_LandlordsLogin : IResponse {}

	[Message(HotfixOpcode.C2R_LandlordsRegist)]
	public partial class C2R_LandlordsRegist : IRequest {}

	[Message(HotfixOpcode.C2G_LandlordsLoginGate)]
	public partial class C2G_LandlordsLoginGate : IRequest {}

	[Message(HotfixOpcode.G2C_LandlordsLoginGate)]
	public partial class G2C_LandlordsLoginGate : IResponse {}

}
namespace ETHotfix
{
	public static partial class HotfixOpcode
	{
		 public const ushort C2R_Login = 10001;
		 public const ushort R2C_Login = 10002;
		 public const ushort C2G_LoginGate = 10003;
		 public const ushort G2C_LoginGate = 10004;
		 public const ushort G2C_TestHotfixMessage = 10005;
		 public const ushort C2M_TestActorRequest = 10006;
		 public const ushort M2C_TestActorResponse = 10007;
		 public const ushort PlayerInfo = 10008;
		 public const ushort C2G_PlayerInfo = 10009;
		 public const ushort G2C_PlayerInfo = 10010;
		 public const ushort C2R_LandlordsLogin = 10011;
		 public const ushort R2C_LandlordsLogin = 10012;
		 public const ushort C2R_LandlordsRegist = 10013;
		 public const ushort C2G_LandlordsLoginGate = 10014;
		 public const ushort G2C_LandlordsLoginGate = 10015;
	}
}
