using JosephM.Core.Attributes;
using JosephM.Core.FieldType;

namespace JosephM.InstanceComparer
{
    public class InstanceComparerDifference
    {
        [GridWidth(150)]
        [DisplayOrder(10)]
        public string Type { get; }
        [DisplayOrder(20)]
        [GridWidth(300)]
        public string Name { get; }
        [DisplayOrder(30)]
        public string Difference { get; }
        [GridWidth(125)]
        [DisplayOrder(70)]
        [PropertyInContextByPropertyNotNull(nameof(Url1))]
        public Url Url1 { get; }
        [GridWidth(125)]
        [DisplayOrder(80)]
        [PropertyInContextByPropertyNotNull(nameof(Url2))]
        public Url Url2 { get; }
        //[Multiline]
        [DisplayOrder(90)]
        [PropertyInContextByPropertyNotNull(nameof(Value1))]
        public string Value1 { get; }
        //[Multiline]
        [DisplayOrder(95)]
        [PropertyInContextByPropertyNotNull(nameof(Value2))]
        public string Value2 { get; }
        [Hidden]
        public string Id1 { get; }
        [Hidden]
        public string Id2 { get; }
        [Hidden]
        public int? ComponentTypeForSolution { get; }
        [Hidden]
        public string IdForSolution1 { get; }
        [Hidden]
        public string IdForSolution2 { get; }

        public InstanceComparerDifference(string type, string name, string difference, string parentReference, string value1, string value2, Url url1, Url url2, string id1, string id2, int? componentTypeForSolution, string idForSolution1, string idForSolution2)
        {
            Type = type;
            Name = string.Format("{0}{1}", parentReference == null ? null : ("[" + parentReference + "] "), name);
            Difference = difference;
            Value1 = value1;
            Value2 = value2;
            Url1 = url1;
            Url2 = url2;
            Id1 = id1;
            Id2 = id2;
            ComponentTypeForSolution = componentTypeForSolution;
            IdForSolution1 = idForSolution1;
            IdForSolution2 = idForSolution2;
        }
    }
}
