using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_LandlordsLoginGateHandler : AMRpcHandler<C2G_LandlordsLoginGate, G2C_LandlordsLoginGate>
    {
        protected override void Run(Session session, C2G_LandlordsLoginGate message, Action<G2C_LandlordsLoginGate> reply)
        {
            RunAsync(session, message, reply).Coroutine();
        }

        private async ETVoid RunAsync(Session session, C2G_LandlordsLoginGate message, Action<G2C_LandlordsLoginGate> reply) {
            var respone = new G2C_LandlordsLoginGate();
            try
            {
                var sessionKeyComponent = Game.Scene.GetComponent<LandlordsSessionKeyComponent>();
                var userId = sessionKeyComponent.Get(message.Key);
                // 验证key是否有效
                if (userId == 0) {
                    respone.Error = ErrorCode.ERR_ConnectGateKeyError;
                    reply(respone);
                    return;
                }
                // 移除认证token
                sessionKeyComponent.Remove(message.Key);

                var user = ComponentFactory.Create<User, long>(userId);
                user.GateAppId = session.InstanceId;
                // 给用户增加MailBoxComponent，成为一个actor
                await user.AddComponent<MailBoxComponent>().AddLocation();
                
                // 加入本gate服务器用户管理组件
                Game.Scene.GetComponent<UserComponent>().Add(user);
                
                // 绑定session，在session销毁时会触发SessionUserComponent destroy事件
                session.AddComponent<SessionUserComponent>().User = user;

                // 在线通知 
                var realmInnerIpEndPoint = StartConfigComponent.Instance.RealmConfig.GetComponent<InnerConfig>().IPEndPoint;
                var realmSession = Game.Scene.GetComponent<NetInnerComponent>().Get(realmInnerIpEndPoint);
                await realmSession.Call(new G2R_LandlordsOnline() { UserId = user.Id, GateAppId = user.GateAppId });

                respone.PlayerId = user.InstanceId;
                reply(respone);
            }
            catch (Exception e) {
                ReplyError(respone, e, reply);
            }
        }
    }
}
