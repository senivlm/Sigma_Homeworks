using ProductLibrary.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductLibrary
{
    internal class Config
    {
        public const char FILE_SEPARATOR = ',';

        public readonly Dictionary<string, Type> ProductCodes = new Dictionary<string, Type>()
        {
            { "d", typeof(DiaryProduct) },
            { "m", typeof(Meat) }
        };

    }
}
