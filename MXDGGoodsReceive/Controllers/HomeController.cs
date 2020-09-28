using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MXDGGoodsReceive.Models;

namespace MXDGGoodsReceive.Controllers
{
    public class HomeController : Controller
    {
        private List<QRCode> qrCodes;
        public ActionResult Index()
        {
            List<string> qrCodes = new List<string> {
//"MX2D1P1000171931Q000000000144S00100564766813Q0255B000000000112D202008214L               ChinaMLX001"
//,"MX2D1P1000171931Q000000000144S00100564766813Q0227B000000000112D202007154L               ChinaMLX001"
//,"MX2D1P1000171931Q000000000144S00100564766813Q0230B000000000112D202007154L               ChinaMLX001"
//,"MX2D1P1000171931Q000000000144S00100564766813Q0222B000000000112D202007144L               ChinaMLX001"
//,"MX2D1P0430200401Q000000002500S00084612055913Q0160B000000000112D202008034L               ChinaMLX001"
//,"MX2D1P0430200401Q000000002500S00084619548313Q0052B000000000112D202009074L               ChinaMLX001"
//,"MX2D1P0430200401Q000000002500S00084619548313Q0001B000000000112D202009074L               ChinaMLX001"
//,"MX2D1P0430200401Q000000002500S00084619548313Q0003B000000000112D202009074L               ChinaMLX001"
//,"MX2D1P0172004312Q000000001360S00100607849513Q00B100536260712D202006304LV187131        CHINAMLX001"
//,"MX2D1P0172004312Q000000001440S00100607849513Q00B100536260712D202006304LV187131        CHINAMLX001"
//,"MX2D1P0172004312Q000000001440S00100607849513Q00B100536260712D202006304LV187131        CHINAMLX001"
//,"MX2D1P0172004312Q000000001440S00100607849513Q00B100536260712D202006304LV187131        CHINAMLX001"
//,"MX2D1P2063595126Q000000000800S00100615747813Q0001B591916872312D202009204L               ChinaMLX001"
//,"MX2D1P2063595125Q000000000800S00100615747813Q0001B591916872312D202009204L               ChinaMLX001"
//,"MX2D1P0348558006Q000000003000S00084618307213Q0013B000000000112D202009134L               ChinaMLX001"
//,"MX2D1P0348552006Q000000010000S00084617489013Q0049B000000000112D202008314L               ChinaMLX001"
//,"MX2D1P0348558006Q000000003000S00084618307213Q0014B000000000112D202009134L               ChinaMLX001"
//,"MX2D1P0348558006Q000000003000S00084618307213Q0018B000000000112D202009134L               ChinaMLX001"
//,"MX2D1P0172069870Q000000000432S00002020090213Q0231B100595413712D202009024LV170419        ChinaMLX001"
//,"MX2D1P0172069870Q000000000432S00002020090213Q0228B100595413712D202009024LV170419        ChinaMLX001"
//,"MX2D1P0172069870Q000000000432S00002020090113Q0165B100595413712D202009014LV170419        ChinaMLX001"
//,"MX2D1P0172069870Q000000000432S00002020090113Q0170B100595413712D202009014LV170419        ChinaMLX001"

            };
            

            int length = 0;
            List<Boolean> formats = new List<bool>();
            int index = 0;

            foreach (var code in qrCodes)
            {
                length = code.Count();

                if ( (code.Substring(0, 6).ToLower() == "mx2d1p" || code.Substring(0, 6).ToLower() == "ms2d1p")
                    && code.Substring(16, 1).ToLower() == "q"
                    && code.Substring(29, 1).ToLower() == "s"
                    && code.Substring(42, 3).ToLower() == "13q"
                    && code.Substring(49, 1).ToLower() == "b"
                    && code.Substring(60, 3).ToLower() == "12d"
                    && code.Substring(71, 2).ToLower() == "4l"
                    && code.Substring(93, 3).ToLower() == "mlx"
                    && length == 99
                    )
                {
                    //should be fill into QRCode object
                    formats.Add(true);
                    index++;
                    //var pn = code.Substring(6, 10);
                    //var qty = code.Substring(17, 12);
                    //var tracebility = code.Substring(30, 12);
                    //var boxnumber = code.Substring(45, 4);
                    //var batch = code.Substring(50, 10);
                    //var xdate = code.Substring(63, 8);
                    //var xzland = code.Substring(73, 20);
                    //var chk = code.Substring(96, 3);
                }
                else
                {
                    //pop up error message 
                    var pn = code.Substring(6, 10);
                    var qty = code.Substring(17, 12);
                    var tracebility = code.Substring(30, 12);
                    var boxnumber = code.Substring(45, 4);
                    var batch = code.Substring(50, 10);
                    var xdate = code.Substring(63, 8);
                    var xzland = code.Substring(73, 20);
                    //var chk = code.Substring(96, 3);
                    formats.Add(false);
                    index++;
                }
            }

            var test = formats;

            return View();
        }

        [HttpPost]
        public ActionResult Test(string result)
        {
            return RedirectToAction("Test", "Home");
        }
        

        //[HttpPost]
        //public ActionResult ListSummary(string result)
        public ActionResult ListSummary(string result)
        {
            //List<QRCode> build up, pass to listsummary view
            List<string> sepResult = separateResult(result);
            var models = new List<QRCode>();
            foreach (var item in sepResult)
            {
                models.Add(new QRCode(item));
            }

            return View(models);
        }

        private List<string> separateResult(string result)
        {
            var returnList = new List<string>();
            var numberOfQRCode = result.Length / 100;
            for(int i = 0; i < numberOfQRCode; i++)
            {
                returnList.Add(result.Substring((i*100)+1, 99));
            }
            return returnList;

        }

        //[HttpPost]
        //public JsonResult MyajaxCall(string result)
        //{
        //    return Json("ok");
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult ViewPage1()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}