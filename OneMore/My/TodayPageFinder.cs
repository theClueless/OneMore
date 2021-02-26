using System.Linq;
using System.Xml.Linq;

namespace River.OneMoreAddIn.My
{
    public class TodayPageFinder
    {
        public const string WorkSectionId = "{2672206C-8D20-4A9B-A381-246FCC6C9622}{1}{B0}";

        public void Find()
        {
            using (var one = new OneNote())
            {
                var theSectionXml = one.GetSection(WorkSectionId);
                var section = new Section(theSectionXml);
                var id = section.GetPageIdFromSection("- Today");
                one.NavigateTo(id).Wait();
            }
        }
    }

    public class Section
    {
        public const string Page_XmlElementName = "Page";
        public const string Page_NameAttribute = "name";
        public const string Page_IdAttribute = "ID";
        public const string Page_DateTimeIdAttribute = "dateTime";
        public const string Page_LastModifiedTimeIdAttribute = "name";

        private readonly XElement _xml;

        public Section(XElement xml)
        {
            this._xml = xml;
        }

        public string GetPageIdFromSection(string name)
        {
            var ns = GetNamespace();
            var val = _xml
                .Elements((ns + Page_XmlElementName))
                .FirstOrDefault(x => x.Attribute(Page_NameAttribute).Value.Contains(name));
                //< one:Page ID = "{2672206C-8D20-4A9B-A381-246FCC6C9622}{1}{E19530992993493370183620109944050162007177131}"
                //name = "To Sort"
                //dateTime = "2016-07-26T09:21:35.000Z"
                //lastModifiedTime = "2021-02-25T13:32:10.000Z"
                //pageLevel = "1" />
            return val?.Attribute(Page_IdAttribute)?.Value;
        }

        /// <summary>
        /// Gets the OneNote namespace for the given element
        /// </summary>
        private XNamespace GetNamespace()
        {
            return _xml.GetNamespaceOfPrefix(OneNote.Prefix);
        }

    }
}