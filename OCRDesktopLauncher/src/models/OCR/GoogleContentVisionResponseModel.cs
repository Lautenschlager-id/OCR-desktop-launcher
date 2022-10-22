using System.Collections.Generic;
using System.Runtime.Serialization;

class GoogleContentVisionResponseModel
{
	public Model<Response> Model { get; private set; }

	[DataContract]
	public struct GroupInfo
	{
		[DataMember]
		public string description { get; set; }
	}

	[DataContract]
	public struct ResultGroup
	{
		[DataMember]
		public List<GroupInfo> textAnnotations { get; set; }
	}

	[DataContract]
	public struct Response
	{
		[DataMember]
		public List<ResultGroup> responses { get; set; }
	}

	public GoogleContentVisionResponseModel()
	{
		Model = new Model<Response>()
		{
			Data = new Response()
		};
	}
}