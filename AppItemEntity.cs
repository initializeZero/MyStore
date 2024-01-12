using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore
{
    public class AppItemEntity
    {
        public enum STATUS
        {
            FREE = 0,
            PAID = 1,
            OWNED = 2,
        }
        public string Name { get; set; }
        public string Score { get; set; }
        public string IconUrl { get; set; }
        public STATUS Status { get; set; }

        public static string GetDisplayNameOfStatus(STATUS status) {
            switch (status)
            {
                case STATUS.FREE:
                    return "Free";
                case STATUS.PAID:
                    return "Paid";
                case STATUS.OWNED:
                    return "Owned";
                default:
                    return "Unknown";
            }
        }
    }
}
