using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MXDGGoodsReceive.Models
{
    public class QRCodeData
    {
        List<QRCode> QrCodes;
        public void Add(QRCode qrCode)
        {
            QrCodes.Add(qrCode);
        }

        public List<QRCode> GetAll()
        {
            return QrCodes;
        }

        public List<QRCode> Remove(QRCode qRCode)
        {
            if (QrCodes.Contains(qRCode))
            {
                QrCodes.Remove(qRCode);
            }
            return QrCodes;
        }
    }
}