using System.Linq;
using System.Xml.Linq;
using River.OneMoreAddIn;

namespace Tester.Console
{
    public class TodayPageFinder
    {
        public const string WorkSectionId = "{2672206C-8D20-4A9B-A381-246FCC6C9622}{1}{B0}";

        public void Find()
        {
            using (var one = new OneNote())
            {
                var res = one.Search(WorkSectionId, "- Today");
                var results = XElement.Parse(res);
                var ress = one.GetSection(WorkSectionId);
                // remove recyclebin nodes
                results.Descendants()
                    .Where(n => n.Name.LocalName == "UnfiledNotes" ||
                                n.Attribute("isRecycleBin") != null ||
                                n.Attribute("isInRecycleBin") != null)
                    .Remove();
                var id = GetPageIdFromSection("- Today", one);
                one.NavigateTo(id).Wait();
                var page = one.CurrentPageId;
                System.Console.WriteLine(page);
                var section = one.CurrentSectionId;
                System.Console.WriteLine(section);
                var page2 = one.GetPage();
                var theSection = one.GetSection();
                var ns = one.GetNamespace(theSection);
                var pageIds = theSection.Elements(ns + "Page")
                    .Select(e => e.Attribute("ID").Value)
                    .ToList();
                var pagesXml = pageIds.Select(x => one.GetPage(x, OneNote.PageDetail.Basic)).ToList();
            }
        }

        public string GetPageIdFromSection(string name, OneNote one)
        {
            var theSection = one.GetSection(WorkSectionId);
            var ns = one.GetNamespace(theSection);
            var val = theSection
                .Elements((ns + "Page"))
                .FirstOrDefault(x => x.Attribute("name").Value.Contains(name));
                //< one:Page ID = "{2672206C-8D20-4A9B-A381-246FCC6C9622}{1}{E19530992993493370183620109944050162007177131}"
                //name = "To Sort"
                //dateTime = "2016-07-26T09:21:35.000Z"
                //lastModifiedTime = "2021-02-25T13:32:10.000Z"
                //pageLevel = "1" />
            return val?.Attribute("ID")?.Value;
        }

        public void SearchHelper()
        {
            //var xml = one.Search(startId, findBox.Text);
            //var results = XElement.Parse(xml);

            //// remove recyclebin nodes
            //results.Descendants()
            //    .Where(n => n.Name.LocalName == "UnfiledNotes" ||
            //                n.Attribute("isRecycleBin") != null ||
            //                n.Attribute("isInRecycleBin") != null)
            //    .Remove();

            //if (results.HasElements)
            //{
            //    resultTree.Populate(results, one.GetNamespace(results));
            //}
        }
    }
}