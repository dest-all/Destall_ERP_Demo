using Protocol.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Protocol.Json
{
    public static class JsonSerialization
    {
        //public static T FromJson<T>(this string obj) => JsonConvert.DeserializeObject<T>(obj, Settings);

        //public static readonly System.Text.Json.JsonSerializer Serializer = JsonSerializer.Create(Settings);

        //public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        //{
        //    ContractResolver = new ContractResolver()
        //};

        //public static bool Bind<TInterface, TImplementation>(this JsonContract jsonContract, Type objectType)
        //    where TImplementation : TInterface
        //{
        //    if (typeof(TInterface) == objectType)
        //    {
        //        var impType = typeof(TImplementation);
        //        jsonContract.CreatedType = impType;
        //        jsonContract.DefaultCreator = () => impType.GetConstructor(new Type[0]).Invoke(null);
        //        return true;
        //    }
        //    return false;
        //}

        
        //class ContractResolver : JsonConverter
        //{
        //    static readonly IEnumerable<Func<JsonContract, Type, bool>> _bindings = new List<Func<JsonContract, Type, bool>>()
        //    {
        //        Bind<IStatus, Status>,
        //        Bind<IExceptionModel, ExceptionModel>
        //    };

        //    public override bool CanConvert(Type typeToConvert)
        //        => MessageContractMaps.Maps.ContainsKey(typeToConvert);

        //    protected override JsonContract CreateContract(Type objectType)
        //    {
        //        var result = base.CreateContract(objectType);
        //        if (objectType.IsInterface)
        //        {
        //            if (MessageContractMaps.Maps.ContainsKey(objectType))
        //            {
        //                result.CreatedType = MessageContractMaps.Maps[objectType];
        //            }
        //            else
        //            {
        //                var bindingSuccess = _bindings.FirstOrDefault(f => f(result, objectType));
        //                    //?? throw new ArgumentException($"Interface/Class map not provided for {objectType.FullName}.");
        //            }
        //        }
        //        else
        //        {
        //            result.CreatedType = objectType;
        //        }

        //        return result;
        //    }
        //}
    }

}