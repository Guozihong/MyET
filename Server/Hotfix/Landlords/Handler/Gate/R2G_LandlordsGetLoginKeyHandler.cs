using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class R2G_LandlordsGetLoginKeyHandler : AMRpcHandler<R2G_LandlordsGetLoginKey, G2R_LandlordsGetLoginKey>
    {
        protected override void Run(Session session, R2G_LandlordsGetLoginKey message, Action<G2R_LandlordsGetLoginKey> reply)
        {
            RunAsync(session, message, reply).Coroutine();
        }

        private async ETVoid RunAsync(Session session, R2G_LandlordsGetLoginKey message, Action<G2R_LandlordsGetLoginKey> reply) {
            var respone = new G2R_LandlordsGetLoginKey();
            try
            {
                long key = RandomHelper.RandInt64();
                Game.Scene.GetComponent<LandlordsSessionKeyComponent>().Add(key, message.UserId);
                respone.Key = key;

                reply(respone);
            }
            catch (Exception e) {
                ReplyError(respone, e, reply);
            }
        }
    }
}
