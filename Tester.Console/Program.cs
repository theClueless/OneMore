using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using River.OneMoreAddIn;

namespace Tester.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            new TodayPageFinder().Find();
            //var sw = Stopwatch.StartNew();
            //using (var one = new OneNote())
            //{
            //    var page = one.CurrentPageId;
            //    System.Console.WriteLine(page);
            //    var section = one.CurrentSectionId;
            //    System.Console.WriteLine(section);
            //    var page2 = one.GetPage();
            //    var theSection = one.GetSection();
            //    var ns = one.GetNamespace(theSection);
            //    var pageIds = theSection.Elements(ns + "Page")
            //        .Select(e => e.Attribute("ID").Value)
            //        .ToList();
            //    var pagesXml = pageIds.Select(x => one.GetPage(x, OneNote.PageDetail.Basic)).ToList();
            //}

            //sw.Stop();
            //System.Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            //System.Console.ReadKey();
        }



    }
}
