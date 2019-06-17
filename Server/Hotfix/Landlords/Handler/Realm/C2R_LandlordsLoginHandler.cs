using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_LandlordsLoginHandler : AMRpcHandler<C2R_LandlordsLogin, R2C_LandlordsLogin>
    {
        protected override void Run(Session session, C2R_LandlordsLogin message, Action<R2C_LandlordsLogin> reply)
        {
            RunAsync(session, message, reply).Coroutine();
        }

        private async ETVoid RunAsync(Session session, C2R_LandlordsLogin message, Action<R2C_LandlordsLogin> reply) {
            R2C_LandlordsLogin r2C_LandlordsLogin = new R2C_LandlordsLogin();
            try
            {
                // DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                // long result = await dBProxyComponent.Delete<AccountInfo>(_account => _account.Account == message.Account);
                // long result = await dBProxyComponent.Delete<AccountInfo>(_account => _account.Password == "");
                // if (result > 0) {
                //     r2C_LandlordsLogin.Error = ErrorCode.ERR_MyErrorCode;
                //     r2C_LandlordsLogin.Message = "删除成功";
                //     reply(r2C_LandlordsLogin);
                //     return;
                // }
                // r2C_LandlordsLogin.Error = ErrorCode.ERR_MyErrorCode;
                // r2C_LandlordsLogin.Message = "删除失败";
                // reply(r2C_LandlordsLogin);

                DBProxyComponent dBProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
                List<ComponentWithId> result = await dBProxyComponent.Query<AccountInfo>(_account => _account.Account == message.Account && _account.Password == message.Password);
                if (result.Count == 0) {
                    r2C_LandlordsLogin.Error = ErrorCode.ERR_AccountOrPasswordError;
                    r2C_LandlordsLogin.Message = "账号或密码错误！";
                    reply(r2C_LandlordsLogin);
                    return;
                }

                AccountInfo account = result[0] as AccountInfo;
                Log.Info($"账号登陆成功！{MongoHelper.ToJson(account)}");


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
