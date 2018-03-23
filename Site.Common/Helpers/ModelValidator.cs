using System;
using System.Linq;

namespace Site.Common.Helpers
{
    public static class ModelValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="propName"></param>
        public static void NotNull(this string s, string propName)
        {
            if (String.IsNullOrEmpty(s))
            {
                throw new BizException($"{propName}不能为空");
            }
        }
        /// <summary>
        /// 不能为空
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propName"></param>
        /// <param name="fullMsg"></param>
        public static void NotNull(this object obj, string propName = "", string fullMsg = "")
        {
            if (obj == null)
            {
                if (!String.IsNullOrEmpty(fullMsg))
                {
                    throw new BizException(fullMsg);
                }
                throw new BizException(String.Format($"{0}不能为空", propName));
            }
        }
        /// <summary>
        /// 字符串相等
        /// </summary>
        /// <param name="actualStr"></param>
        /// <param name="exceptStr"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void ShouldBe(this string actualStr, string exceptStr, string format = "", params string[] args)
        {
            if (exceptStr != actualStr)
            {
                throw new BizException(String.Format(format, args));
            }
        }

        public static void ShouldIn(this string actualStr, string[] exceptStrs, string propName)
        {
            if (!exceptStrs.Contains(actualStr))
            {
                throw new BizException($"{propName}不是合法的{String.Join(",",exceptStrs)}输入值之一");
            }
        }

        public static void DecimalValid(this string actualStr, string propName,bool shouldLargeThanZero=true)
        {
            if (decimal.TryParse(actualStr,out decimal val))
            {
                if (shouldLargeThanZero && val<=0)
                {
                    throw new BizException("数值应大于0");
                }
                return;
            }
            throw new BizException("小数不合法");
        }
        
        public static void LongValid(this string actualStr, string propName,bool shouldLargeThanZero=true)
        {
            if (long.TryParse(actualStr,out long val))
            {
                if (shouldLargeThanZero && val<=0)
                {
                    throw new BizException("数值应大于0");
                }
                return;
            }
            throw new BizException("整数不合法");
        }
        /// <summary>
        /// 不能太长
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <param name="propName"></param>
        public static void NotTooLong(this string s, int length, string propName)
        {
            if (String.IsNullOrEmpty(s))
            {
                return;
            }
            if (s.Length > length)
            {
                throw new BizException(string.Format("字段{0}长度不能超过{1}", propName, length));
            }
        }
        /// <summary>
        /// 应小于
        /// </summary>
        /// <param name="val"></param>
        /// <param name="max"></param>
        /// <param name="propName"></param>
        public static void ShouldLessEqThan(this long val, long max, string propName)
        {
            if (val > max)
            {
                throw new BizException($"{propName}应小于{max}");
            }
        }
        /// <summary>
        /// 应大于
        /// </summary>
        /// <param name="val">输入值</param>
        /// <param name="min">最小值</param>
        /// <param name="propName">属性名字</param>
        /// <exception cref="BizException"></exception>
        public static void ShouldLargeThan(this long val, long min, string propName)
        {
            if (val<min)
            {
                throw new BizException($"{propName}不能小于{min}");
            }
        }
        /// <summary>
        /// 应大于
        /// </summary>
        /// <param name="val">输入值</param>
        /// <param name="min">最小值</param>
        /// <param name="propName">属性名字</param>
        /// <exception cref="BizException"></exception>
        public static void ShouldLargeThan(this decimal val, decimal min, string propName)
        {
            if (val<min)
            {
                throw new BizException($"{propName}不能小于{min}");
            }
        }
        
        /// <summary>
        /// 枚举必须合法
        /// </summary>
        /// <param name="val"></param>
        /// <param name="propName"></param>
        public static void EnumValueValid(this Enum val, string propName)
        {
            var values = Enum.GetValues(val.GetType());

            // Array.FindIndex(values,0,w=>(short)w==(short)val);
            var find = false;
            for (int i = 0; i < values.Length; i++)
            {
                if (String.Compare(values.GetValue(i).ToString(), Convert.ToString(val), true) == 0)
                {
                    find = true;
                    break;
                }
            }
            if (!find)
            {
                throw new BizException($"{propName}对应的值{val}不合法");
            }
            //    var index = Array.IndexOf(values,val);
        }
    }
}