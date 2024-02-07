namespace artur_gde_krosi_Vue.Server.Models.moyskladApi
{
    public class GroupApi
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

        public class Group
        {
            public Meta meta { get; set; }
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

        public class Owner
        {
            public Meta meta { get; set; }
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
            public bool useParentVat { get; set; }
            public ProductFolder productFolder { get; set; }
        }

        public class Root  : InterfaceApi.Root
        {
            public Context context { get; set; }
            public Meta meta { get; set; }
            public List<Row> rows { get; set; }
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
            public string externalCode { get; set; }
            public bool archived { get; set; }
            public string pathName { get; set; }
            public bool useParentVat { get; set; }
            public ProductFolder productFolder { get; set; }
        }


    }
}
