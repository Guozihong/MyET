using System;
using MongoDB.Driver;

namespace ETModel
{

	[ObjectSystem]
	public class DBDeleteTaskAwakeSystem : AwakeSystem<DBDeleteTask, string, ETTaskCompletionSource<long>>
	{
		public override void Awake(DBDeleteTask self, string collectionName, ETTaskCompletionSource<long> tcs)
		{
			self.CollectionName = collectionName;
			self.Tcs = tcs;
		}
	}

	public sealed class DBDeleteTask : DBTask
	{
		public string CollectionName { get; set; }

		public ETTaskCompletionSource<long> Tcs;

		public override async ETTask Run()
		{
			DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();

			try
			{
				// 执行保存数据库任务
				DeleteResult deleteResult = await dbComponent.GetCollection(this.CollectionName).DeleteManyAsync(s => s.Id == this.Id);
				this.Tcs.SetResult(deleteResult.DeletedCount);
			}
			catch (Exception e)
			{
				this.Tcs.SetException(new Exception($"删除数据失败!  {CollectionName} {Id}", e));
			}
		}
	}
}