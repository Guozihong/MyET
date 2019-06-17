using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_LandlordsRetistHandler : AMRpcHandler<C2R_LandlordsRegist, R2C_LandlordsLogin>
    {
        protected override void Run(Session session, C2R_LandlordsRegist message, Action<R2C_LandlordsLogin> reply)
        {
            RunAsync(session, message, reply).Coroutine();
        }

        private async ETVoid RunAsync(Session session, C2R_LandlordsRegist message, Action<R2C_LandlordsLogin> reply) {
            R2C_LandlordsLogin r2C_LandlordsLogin = new R2C_LandlordsLogin();
            try
            {
                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                List<ComponentWithId> result = await dBProxyComponent.Query<AccountInfo>(_account => _account.Account == message.Account);
                if (result.Count > 0) {
                    r2C_LandlordsLogin.Error = ErrorCode.ERR_MyErrorCode;
                    r2C_LandlordsLogin.Message = "无法重复注册！";
                    reply(r2C_LandlordsLogin);
                    return;
                }

                AccountInfo account = ComponentFactory.CreateWithId<AccountInfo>(IdGenerater.GenerateId());
                account.Account = message.Account;
                account.Password = message.Password;
                Log.Info($"账号注册成功！{MongoHelper.ToJson(account)}");

                dBProxyComponent.Save(account);

                // 随机一个网关服务器
                var gateConfig = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
                var gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(gateConfig.GetComponent<InnerConfig>().IPEndPoint);

                // 请求登陆key
                var g2RGetLoginKey = (G2R_LandlordsGetLoginKey) await gateSession.Call(new R2G_LandlordsGetLoginKey() { UserId = account.Id });

                r2C_LandlordsLogin.Key = g2RGetLoginKey.Key;
                r2C_LandlordsLogin.Address = gateConfig.GetComponent<OuterConfig>().Address;

                reply(r2C_LandlordsLogin);
            }
            catch (Exception e) {
                ReplyError(r2C_LandlordsLogin, e, reply);
            }
        }
    }
}
