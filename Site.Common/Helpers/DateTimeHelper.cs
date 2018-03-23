using System;

namespace Site.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static long GetUnixValue(this DateTime dt)
        {
            if (dt < new DateTime(1970, 1, 1))
            {
                throw new BizException("时间不正确");
            }

            return (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

    }
}