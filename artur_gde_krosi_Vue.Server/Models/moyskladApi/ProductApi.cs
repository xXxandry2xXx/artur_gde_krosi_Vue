using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using static artur_gde_krosi_Vue.Server.Models.ProductApi;

namespace artur_gde_krosi_Vue.Server.Models
{
    public class ProductApi
    {
        public Root root = new Root();

        public class Barcode
        {
            public string ean13 { get; set; }
        }

        public class BuyPrice
        {
            public double value { get; set; }
            public Currency currency { get; set; }
        }

        public class Context
        {
            public Employee employee { get; set; }
        }

        public class Currency
        {
            public Meta meta { get; set; }
        }

        public class Employee
        {
            public Meta meta { get; set; }
        }

        public class Files
        {
            public Meta meta { get; set; }
        }

        public class Group
        {
            public Meta meta { get; set; }
        }

        public class Images
        {
            public Meta meta { get; set; }
            public List<Row> rows { get; set; }
        }

        public class Meta
        {
            public string href { get; set; }
            public string metadataHref { get; set; }
            public string type { get; set; }
            public string mediaType { get; set; }
            public string uuidHref { get; set; }
            public int size { get; set; }
            public int limit { get; set; }
            public int offset { get; set; }
            public string downloadHref { get; set; }
        }

        public class Miniature
        {
            public string href { get; set; }
            public string type { get; set; }
            public string mediaType { get; set; }
            public string downloadHref { get; set; }
        }

        public class MinPrice
        {
            public double value { get; set; }
            public Currency currency { get; set; }
        }

        public class Owner
        {
            public Meta meta { get; set; }
        }

        public class PriceType
        {
            public Meta meta { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string externalCode { get; set; }
        }

        public class ProductFolder
        {
            public Meta meta { get; set; }
            public string id { get; set; }
            public string accountId { get; set; }
            public Owner owner { get; set; }
            public bool shared { get; set; }
            public Group group { get; set; }
            public string updated { get; set; }
            public string name { get; set; }
            public string externalCode { get; set; }
            public bool archived { get; set; }
            public string pathName { get; set; }
            public ProductFolder productFolder { get; set; }
            public bool useParentVat { get; set; }
        }

        public new class Root : InterfaceApi.Root
        {
            public new Context context { get; set; }
            public new Meta meta { get; set; }
            public new List<Row> rows { get; set; }
            public override int Count()
            {
                return rows.Count;
            }
            public override Root New()
            {
                return new Root();
            }
            public override void AddRange<T>(T root)
            {
                if (root is Root rootTry)
                {
                    rows.AddRange(rootTry.rows);
                }
            }
        }

        public class Row 
        {
            public Meta meta { get; set; }
            public string id { get; set; }
            public string accountId { get; set; }
            public Owner owner { get; set; }
            public bool shared { get; set; }
            public Group group { get; set; }
            public string updated { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string code { get; set; }
            public string externalCode { get; set; }
            public bool archived { get; set; }
            public string pathName { get; set; }
            public bool useParentVat { get; set; }
            public Uom uom { get; set; }
            public Images images { get; set; }
            public MinPrice minPrice { get; set; }
            public List<SalePrice> salePrices { get; set; }
            public BuyPrice buyPrice { get; set; }
            public List<Barcode> barcodes { get; set; }
            public string paymentItemType { get; set; }
            public bool discountProhibited { get; set; }
            public double weight { get; set; }
            public double volume { get; set; }
            public int variantsCount { get; set; }
            public bool isSerialTrackable { get; set; }
            public string trackingType { get; set; }
            public bool partialDisposal { get; set; }
            public Files files { get; set; }
            public ProductFolder productFolder { get; set; }
            public string title { get; set; }
            public string filename { get; set; }
            public int size { get; set; }
            public Miniature miniature { get; set; }
            public Tiny tiny { get; set; }
        }

        public class SalePrice
        {
            public double value { get; set; }
            public Currency currency { get; set; }
            public PriceType priceType { get; set; }
        }

        public class Tiny
        {
            public string href { get; set; }
            public string type { get; set; }
            public string mediaType { get; set; }
        }

        public class Uom
        {
            public Meta meta { get; set; }
        }
    }
}
