using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

class Model<BaseStruct>
{
	public BaseStruct Data;

	private Type selfType = typeof(BaseStruct);

	public string ToJSON()
	{
		string json;

		using (MemoryStream stream = new MemoryStream())
		{
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(selfType);
			serializer.WriteObject(stream, Data);

			stream.Position = 0;
			json = (new StreamReader(stream)).ReadToEnd();

			stream.Close();
		}

		return json;
	}

	public BaseStruct FromJSON(string rcvJSON)
	{
		BaseStruct json;

		using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(rcvJSON)))
		{
			DataContractJsonSerializer deserializer = new DataContractJsonSerializer(selfType);
			json = (BaseStruct)deserializer.ReadObject(stream);
			stream.Close();
		}

		return json;
	}
}