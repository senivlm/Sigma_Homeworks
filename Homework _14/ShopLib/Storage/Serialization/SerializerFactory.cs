using ShopLib.Products;
using ShopLib.Products.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ShopLib.Storage.Serialization
{
    public class SerializerFactory
    {
        Dictionary<string, Type> types;

        public SerializerFactory()
        {
            LoadTypesICanReturn();
        }

        public IStorageSerializer<T> CreateInstance<T>(string description) where T : IProduct
        {
            description = GetCompilationTypeName(description);
            Type typeToCreate = GetTypeToCreate(description);
            
            //if (t == null)
            //    return new UnknownSerializer();

            if (typeToCreate == null)
                return null;

            var t = typeToCreate.MakeGenericType(typeof(T));

            return (IStorageSerializer<T>)Activator.CreateInstance(t);
        }

        private Type GetTypeToCreate(string serializerName)
        {
            foreach (var serializer in types)
            {
                if (serializer.Key.Contains(serializerName))
                {
                    return types[serializer.Key];
                }
            }

            return null;
        }

        private void LoadTypesICanReturn()
        {
            types = new Dictionary<string, Type>();

            Type[] typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in typesInThisAssembly)
            {

                if (type.GetInterface("IStorageSerializer`1") != null) 
                {
                    types.Add(type.Name.ToLower(), type);
                }
            }
        }

        private static string GetCompilationTypeName(string typeName)
        {
            if (typeName == null)
                throw new ArgumentNullException();

            typeName = typeName.ToLower();
            typeName += "`1";

            return typeName;
        }
    }
}
