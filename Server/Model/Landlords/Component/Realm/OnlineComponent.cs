using System.Collections.Generic;

namespace ETModel {
    /// <summary>
    /// 记录在线玩家所在gateId
    /// 可用于全体广播或获取指定用户gateId
    /// </summary>
    public class OnlineComponent : Component {
        private readonly Dictionary<long, long> onlineDict = new Dictionary<long, long>();

        public void Add(long userId, long gateSessionId) {
            onlineDict.Add(userId, gateSessionId);
        }

        public long Get(long userId) {
            long gateSessionId;
            onlineDict.TryGetValue(userId, out gateSessionId);

            return gateSessionId;
        }

        public void Remove(long userId) {
            onlineDict.Remove(userId);
        }
    }

}