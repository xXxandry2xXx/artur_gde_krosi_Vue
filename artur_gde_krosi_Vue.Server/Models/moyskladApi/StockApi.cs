using Newtonsoft.Json;
using System.IO.Compression;

namespace artur_gde_krosi_Vue.Server.Models.moyskladApi
{
    public class StockApi
    {
        public Root root { get; set; }

        public class Context
        {
            public Employee employee { get; set; }
        }

        public class Employee
        {
            public Meta meta { get; set; }
        }

        public class Folder
        {
            public Meta meta { get; set; }
            public string name { get; set; }
            public string pathName { get; set; }
        }

        public class Image
        {
            public Meta meta { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public int size { get; set; }
            public string updated { get; set; }
            public Miniature miniature { get; set; }
            public Tiny tiny { get; set; }
        }

        public class Meta
        {
            public string href { get; set; }
            public string metadataHref { get; set; }
            public string type { get; set; }
            public string mediaType { get; set; }
            public int size { get; set; }
            public int limit { get; set; }
            public int offset { get; set; }
            public string uuidHref { get; set; }
        }

        public class Miniature
        {
            public string href { get; set; }
            public string mediaType { get; set; }
            public string downloadHref { get; set; }
        }

        public class Root : InterfaceApi.Root
        {
            public Context context { get; set; }
            public Meta meta { get; set; }
            public List<Row> rows { get; set; }
        }

        public class Row
        {
            public Meta meta { get; set; }
            public string stock { get; set; }
            public double inTransit { get; set; }
            public double reserve { get; set; }
            public double quantity { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public double price { get; set; }
            public double salePrice { get; set; }
            public Uom uom { get; set; }
            public Folder folder { get; set; }
            public Image image { get; set; }
            public string externalCode { get; set; }
            public double stockDays { get; set; }
        }

        public class Tiny
        {
            public string href { get; set; }
            public string mediaType { get; set; }
        }

        public class Uom
        {
            public Meta meta { get; set; }
            public string name { get; set; }
        }


    }
}
