using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
	[ObjectSystem]
	public class UserComponentSystem : AwakeSystem<UserComponent>
	{
		public override void Awake(UserComponent self)
		{
			self.Awake();
		}
	}
	
	public class UserComponent : Component
	{
		public static UserComponent Instance { get; private set; }

		public User MyUser;
		
		private readonly Dictionary<long, User> idUsers = new Dictionary<long, User>();

		public void Awake()
		{
			Instance = this;
		}
		
		public void Add(User User)
		{
			this.idUsers.Add(User.UserId, User);
		}

		public User Get(long userId)
		{
			this.idUsers.TryGetValue(userId, out User gamer);
			return gamer;
		}

		public void Remove(long userId)
		{
			this.idUsers.Remove(userId);
		}

		public int Count
		{
			get
			{
				return this.idUsers.Count;
			}
		}

		public User[] GetAll()
		{
			return this.idUsers.Values.ToArray();
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			
			base.Dispose();

			foreach (User User in this.idUsers.Values)
			{
				User.Dispose();
			}

			Instance = null;
		}
	}
}