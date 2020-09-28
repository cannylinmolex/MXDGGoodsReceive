using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MXDGGoodsReceive.Models
{
    public class QRCode
    {
        public int Id { get; set; }
        public string ReceivingLog { get; set; }
        public string PN { get; set; }
        public double Qty { get; set; }
        public string TraceabilityNumber { get; set; }
        public int BoxNumber { get; set; }
        public string Batch { get; set; }
        public DateTime ProductDate { get; set; }
        public string Country { get; set; }
        public string Chk { get; set; }
        public string FullCode { get; set; }
        public string UserName { get; set; }
        public DateTime ReceivingDate { get; set; }

        public QRCode(string input)
        {
            if (input.Substring(0, 6).ToLower() == "mx2d1p"
                    && input.Substring(16, 1).ToLower() == "q"
                    && input.Substring(29, 1).ToLower() == "s"
                    && input.Substring(42, 3).ToLower() == "13q"
                    && input.Substring(49, 1).ToLower() == "b"
                    && input.Substring(60, 3).ToLower() == "12d"
                    && input.Substring(71, 2).ToLower() == "4l"
                    && input.Substring(93, 3).ToLower() == "mlx"
                    && input.Length == 99
                    )
            {
                //fill into QRCode object
                ReceivingLog = ""; //test use
                UserName = "";
                Id = 1;

                PN = input.Substring(6, 10);
                Qty = Convert.ToDouble( input.Substring(17, 12));
                TraceabilityNumber = input.Substring(30, 12);
                BoxNumber = Convert.ToInt32( input.Substring(45, 4));
                Batch = input.Substring(50, 10);
                ProductDate = Convert.ToDateTime(modifyProductionDate(input.Substring(63, 8)));
                Country = input.Substring(73, 20);
                Chk = input.Substring(96, 3);
                FullCode = input;
                ReceivingDate = DateTime.Now;
            }
            else
            {
                //return error message??
            }
        }

        private string modifyProductionDate(string data)
        {
            var modifiedString = data.Substring(0, 4)
                + "/" + data.Substring(4, 2)
                + "/" + data.Substring(6, 2);
            return modifiedString;
        }
    }
}

