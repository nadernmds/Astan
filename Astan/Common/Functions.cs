using System.Globalization;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Data;
using System.Collections.Generic;
using System.IO;
using Astan.Models;
using System;

namespace System
{
    public static class ExtentionFuncs
    {

        public static bool isAdmin(this User user)
        {
            return user.userGroupID == 1 || user.userGroupID == 2;
        }


        public static IEnumerable<Client> EncodeClients(this IEnumerable<Client> clients)
        {
            foreach (var client in clients)
            {
                yield return EncodeClient(client);
            }
        }
        private const string Key = "pep";
        public static Client EncodeClient(this Client client)
        {
            string KeyData = Key.ToBase64();
            client.name = client.name.ToBase64(KeyData);
            client.fatherName = client.fatherName.ToBase64(KeyData);
            client.need = client.need.ToBase64(KeyData);
            client.nationalCode = client.nationalCode.ToBase64(KeyData);
            client.homeAdress = client.homeAdress.ToBase64(KeyData);
            client.mobile = client.mobile.ToBase64(KeyData);
            client.jobtitle = client.jobtitle.ToBase64(KeyData);
            return client;
        }
        public static string DecodeItem(this string item)
        {
            return item.FromBase64(Key.ToBase64());
        }
        public static string EncodeItem(this string item)
        {
            string KeyData = Key.ToBase64();
            return item.ToBase64(KeyData);
        }
        public static IEnumerable<Client> DecodeClients(this IEnumerable<Client> clients)
        {
            foreach (var client in clients)
            {
                yield return DecodeClient(client);
            }
        }
        public static Client DecodeClient(this Client client)
        {
            if (client == null)
            {
                return new Client();
            }
            string KeyData = Key.ToBase64();
            client.name = client.name.FromBase64(KeyData);
            client.fatherName = client.fatherName.FromBase64(KeyData);
            client.need = client.need.FromBase64(KeyData);
            client.nationalCode = client.nationalCode.FromBase64(KeyData);
            client.homeAdress = client.homeAdress.FromBase64(KeyData);
            client.jobtitle = client.jobtitle.FromBase64(KeyData);
            client.mobile = client.mobile.FromBase64(KeyData);
            return client;
        }
        public static HtmlString ToTag(this string Tags)
        {
            string Tag = "";
            if (!string.IsNullOrWhiteSpace(Tags))
                foreach (string x in Tags.Split(','))
                {
                    Tag += $"<a href='/tag/{x.CodeString()}' target='_blank'>{x}</a>";
                }

            return new HtmlString(Tag);
        }
        public static string SubString(this string text, int WordCount)
        {
            if (text.Split(' ').Count() >= WordCount)
            {
                string Res = "";
                for (int i = 0; i < WordCount; i++)
                {
                    Res += text.Split(' ')[i] + " ";
                }
                return Res + " ...";
            }
            return text;
        }

        public static string CodeString(this string txt)
        {

            string tmp = txt.Replace(' ', '-').Replace('.', '_').Replace("&", "and").Replace("#", "sharp").Replace("/", "and").Replace(":", "");
            return tmp;
        }
        public static string DecodeString(this string txt)
        {

            string tmp = txt.Replace('-', ' ').Replace('_', '.');
            return tmp;
        }

        public static int ToInt(this object number)
        {
            if (number == null || number.ToString().IsNullOrEmpty())
                return 0;
            else
                return int.Parse(number.ToString());
        }

        public static float ToFloat(this object number)
        {
            if (number == null)
                return 0;
            else
                return float.Parse(number.ToString());
        }

        public static short ToShort(this object number)
        {
            if (number == null)
                return 0;
            else
                return short.Parse(number.ToString());
        }

        public static long ToLong(this object number)
        {
            if (number == null)
                return 0;
            else
                return long.Parse(number.ToString());
        }
        public static byte ToByte(this object number)
        {
            if (number == null)
                return 0;
            else
                return byte.Parse(number.ToString());
        }
        public static bool ToBoolean(this object _bool)
        {
            if (_bool == null)
                return false;
            else
                return bool.Parse(_bool.ToString());
        }
        public enum ImageType
        {
            UserIcon,
            Header
        }
        public static string GetImage(this string img, ImageType imgType)
        {
            string DefaultImage = "/images/DefaultItemLogo.jpg";
            if (imgType == ImageType.UserIcon)
                DefaultImage = "/images/noavatar.png";

            if (img == null)
                return DefaultImage;
            else
                return string.IsNullOrEmpty(img) ? DefaultImage : img;
        }
        public static string GetImage(this byte[] img, ImageType imgType = ImageType.UserIcon)
        {
            string DefaultImage = "/img/noavatar.png";

            if (img == null)
                return DefaultImage;
            else
                return (img).Length < 20 ? DefaultImage : "data:image/png;base64," + Convert.ToBase64String(img);
        }
        public static string FormatPrice(this int? price)
        {
            if (price.HasValue)
                return price.Value.ToString("N0");
            else
                return "";
        }

        public static string MakeSecureSQLParam(this string str)
        {
            string res = str.Replace("--", "");
            res = res.Replace("'", "").Replace("\"", "");
            return res;
        }


        /// <summary>
        /// [HS] 
        /// Convert List Of Object To Json String
        /// </summary>
        public static string ToJson<T>(this object entityClass, List<T> ListOfThisEntity)
        {
            JObject json = new JObject();
            if (entityClass.GetType().IsGenericType)
            {
                // Is a List
                return JsonConvert.SerializeObject(entityClass);
            }
            else
            {
                // Is a Simple Class   
                Type t = entityClass.GetType();
                foreach (PropertyInfo info in t.GetProperties())
                {
                    string propertytype = info.GetGetMethod().ReturnType.Name.ToLower();
                    if (propertytype == "string")
                    {
                        json.Add(info.Name.ToLower(), info.GetValue(entityClass).ToString());
                    }
                    else if (propertytype == "int32")
                    {
                        json.Add(info.Name.ToLower(), int.Parse(info.GetValue(entityClass).ToString()));
                    }
                    else if (propertytype == "double")
                    {
                        json.Add(info.Name.ToLower(), float.Parse(info.GetValue(entityClass).ToString()));
                    }
                    else if (propertytype == "boolean")
                    {
                        json.Add(info.Name.ToLower(), bool.Parse(info.GetValue(entityClass).ToString()));
                    }
                }
                if (ListOfThisEntity != null)
                {
                    json.Add("Data", JsonConvert.SerializeObject(ListOfThisEntity));
                }
            }
            return json.ToString().Replace("\\\"", "\"").Replace("\"[", "[").Replace("]\"", "]");
        }

        public static string ToJson(this object entityClass)
        {
            JObject json = new JObject();

            Type[] MainProperties = new Type[] {
                typeof(bool),
                typeof(string),
                typeof(long),
                typeof(int),
                typeof(Int32),
                typeof(double),
                typeof(float)
            };
            // Is a Simple Class   
            Type t = entityClass.GetType();
            foreach (PropertyInfo info in t.GetProperties())
            {

                // is System Main type of [Custom Type]

                if (!MainProperties.Contains(info.PropertyType))
                {
                    string innerClassJson = info.GetValue(entityClass).ToJson();
                    json.Add(info.Name.ToLower(), innerClassJson);
                }
                else
                {
                    Type propertytype = info.PropertyType;
                    if (propertytype == typeof(string))
                    {
                        json.Add(info.Name.ToLower(), info.GetValue(entityClass) == null ? "" : info.GetValue(entityClass).ToString());
                    }
                    else if (propertytype == typeof(int))
                    {
                        json.Add(info.Name.ToLower(), int.Parse(info.GetValue(entityClass).ToString()));
                    }
                    else if (propertytype == typeof(long))
                    {
                        json.Add(info.Name.ToLower(), long.Parse(info.GetValue(entityClass).ToString()));
                    }
                    else if (propertytype == typeof(double))
                    {
                        json.Add(info.Name.ToLower(), float.Parse(info.GetValue(entityClass).ToString()));
                    }
                    else if (propertytype == typeof(bool))
                    {
                        json.Add(info.Name.ToLower(), bool.Parse(info.GetValue(entityClass).ToString()));
                    }
                }
            }
            return json.ToString().Replace("\\\"", "\"").Replace("\"[", "[").Replace("]\"", "]").Replace("\"{", "{").Replace("}\"", "}").Replace("\r\n", "");
        }


        public static object GetValueFromJson(this string msg, string filed)
        {
            // استخراج کد دستور از جیسون فرستاده شده
            JToken json = JToken.Parse(msg);
            return json.SelectToken(filed);
        }

        public static T GetEntityFromJson<T>(this string json)
        {
            T cls = default(T);
            cls = JsonConvert.DeserializeObject<T>(json);
            return cls;
        }

        public static List<T> GetListFromJson<T>(this string json)
        {
            if (json.Length < 5)
                return new List<T>();
            var result = JsonConvert.DeserializeObject<List<T>>(json);
            var status = result[0];
            return result;
        }


        public static string ToJson(this DataTable table)
        {

            JArray jArray = new JArray();

            ///طرز کار : 
            ///یکی پراپرتی ایجاد و مقدار آن را برابر یک آرایه قرار میدهد
            /// یکی ابجکت از توع جی سون ساخته و مقادیر تک تک ستون ها را داخل آن قرار میدهد و آن را در ابجکت جی سون قرار میدهد و ابچکت را به آرایه کپی میکند
            ///سپس همین کار را برای سایر سطر های جدول نیز انجام میدهد

            foreach (DataRow dr in table.Rows)
            {
                JObject jObject = new JObject();
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    jObject.Add(dr.Table.Columns[i].ColumnName.ToLower(), dr[i].ToString());
                }
                jArray.Add(jObject);
            }

            JProperty jProperty = new JProperty("report", jArray);

            return "{" + jProperty.ToString() + "}";
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string ToBase64(this string str)
        {
            byte[] Text = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(Text);
        }
        public static string ToBase64(this string str, string parity)
        {
            byte[] Text = System.Text.Encoding.UTF8.GetBytes(str);
            return (Convert.ToBase64String(Text) + parity).ToBase64();
        }
        public static string FromBase64(this string str)
        {
            byte[] Text = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(Text);
        }
        public static string FromBase64(this string str, string parity)
        {
            byte[] Text = Convert.FromBase64String(str);
            var w = System.Text.Encoding.UTF8.GetString(Text);
            return w.Remove(w.LastIndexOf(parity), parity.Length).FromBase64();
        }

        public static Functions.InValidFile IsValidFile(this HttpPostedFileBase file, Functions.FileType fileType, int MaximumeKB = 512000) // 500 Mb
        {
            int maxSize = MaximumeKB * 1024; // Convert to Byte
            Dictionary<string, string> AllowedFileExtention = new Dictionary<string, string>();


            AllowedFileExtention.Add("Image", ".jpg,.png,.bmp,.jpeg,.tiff");
            AllowedFileExtention.Add("Video", ".mp4,.avi,.mkv");
            AllowedFileExtention.Add("Documet", ".pdf,.doc,.docx,.txt");


            if (file == null)
                return Functions.InValidFile.IsNull;
            else if (file.InputStream.Length > maxSize)
                return Functions.InValidFile.IsLarg;
            else
            {
                if (fileType == Functions.FileType.Image)
                {
                    if (!AllowedFileExtention["Image"].Contains(Path.GetExtension(file.FileName).ToLower()))
                    {
                        return Functions.InValidFile.NotValidFormat;
                    }
                }
                else if (fileType == Functions.FileType.Video)
                {
                    if (!AllowedFileExtention["Video"].Contains(Path.GetExtension(file.FileName).ToLower()))
                    {
                        return Functions.InValidFile.NotValidFormat;
                    }
                }
                else if (fileType == Functions.FileType.Document)
                {
                    if (!AllowedFileExtention["Document"].Contains(Path.GetExtension(file.FileName).ToLower()))
                    {
                        return Functions.InValidFile.NotValidFormat;
                    }
                }
            }
            return Functions.InValidFile.Valid;
        }
    }

    public class Functions
    {

        public enum InValidFile
        {
            IsNull,
            IsLarg,
            NotValidFormat,
            Valid
        }

        public enum FileType
        {
            Image,
            Video,
            Document,
            All
        }
        public static string GetPersianDate()
        {
            DateTime dt = DateTime.Now;
            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetYear(dt)}/{pc.GetMonth(dt)}/{pc.GetDayOfMonth(dt)} {dt.Hour}:{dt.Minute}";
        }

        //public static string ShortDate
        //{
        //    get {
        //        DateTime dt = DateTime.Now;
        //        PersianCalendar pc = new PersianCalendar();
        //        string Res = $"{pc.GetYear(dt)}/{pc.GetMonth(dt)}/{pc.GetDayOfMonth(dt)}";
        //        return FixDate(Res);
        //    }
        //}

        public static PersianDate ShortDate
        {
            get
            {
                DateTime dt = DateTime.Now;
                PersianCalendar pc = new PersianCalendar();
                System.PersianDate pd = new System.PersianDate();
                pd.Year = pc.GetYear(dt);
                pd.Month = pc.GetMonth(dt);
                pd.Day = pc.GetDayOfMonth(dt);
                pd.Hour = dt.Hour;
                pd.Minute = dt.Minute;
                return pd;
            }
        }

        public static string ShortDateNotFixed
        {
            get
            {
                DateTime dt = DateTime.Now;
                PersianCalendar pc = new PersianCalendar();
                return $"{pc.GetYear(dt)}/{pc.GetMonth(dt)}/{pc.GetDayOfMonth(dt)}";
            }
        }

        public static string DisplayShortDate
        {
            get
            {
                DayOfWeek Day = DateTime.Now.DayOfWeek;
                string DayName = "شنبه";
                switch (Day)
                {
                    case DayOfWeek.Sunday:
                        DayName = "یکشنبه";
                        break;
                    case DayOfWeek.Monday:
                        DayName = "دوشنبه";
                        break;
                    case DayOfWeek.Tuesday:
                        DayName = "سه شنبه";
                        break;
                    case DayOfWeek.Wednesday:
                        DayName = "چهارشنبه";
                        break;
                    case DayOfWeek.Thursday:
                        DayName = "پنجشنبه";
                        break;
                    case DayOfWeek.Friday:
                        DayName = "جمعه";
                        break;
                }

                return $@"امروز {DayName} {ShortDate.ToString()}";
            }
        }

        public static string GetMonth(string Date)
        {
            string[] Parts = Date.Split('/');
            switch (Parts[1].ToInt())
            {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default: return Parts[1];
            }
        }
        public static string GetMonth(int Month)
        {

            switch (Month)
            {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default: return Month.ToString();
            }
        }

        public static string GetDay(int Day)
        {

            switch (Day)
            {
                case 0:
                    return "شنبه";
                case 1:
                    return "یکشنبه";
                case 2:
                    return "دوشنبه";
                case 3:
                    return "سه شنبه";
                case 4:
                    return "چهارشنبه";
                case 5:
                    return "پنجشنبه";
                case 6:
                    return "جمعه";
                default: return Day.ToString();
            }
        }

        public static string FixDate(string Date)
        {
            string[] Parts = Date.Split('/');
            string Res = Parts[0] + "/";
            if (Parts[1].Length < 2)
                Res += "0" + Parts[1];
            else
                Res += Parts[1];

            if (Parts[2].Length < 2)
                Res += "/0" + Parts[2];
            else
                Res += "/" + Parts[2];
            return Res;
        }
        public static int MakeExpire()
        {
            DateTime dt = DateTime.Now;
            return ((dt.Year - 2000) * 365) + (dt.Month * 31) + dt.Day + 8;
        }
        public static bool CheckExpire(int Expire)
        {
            DateTime dt = DateTime.Now;
            int Date = ((dt.Year - 2000) * 365) + (dt.Month * 31) + dt.Day;
            return (Date <= Expire);
        }


        public static int EncryptID(int id)
        {
            return (id * 25 + 14);
        }
        public static int DecryptID(int id)
        {
            return ((id - 14) / 25);
        }

        //public static LoginData IsLogin() // return UserID
        //{
        //    if (HttpContext.Current.Request.Cookies["Hs_pl"] != null)
        //    {
        //        int UserID = HttpContext.Current.Request.Cookies["Hs_pl"].Values["LiveSession"].ToInt();
        //        UserType UserType = (UserType)HttpContext.Current.Request.Cookies["Hs_pl"].Values["UT"].ToByte();
        //        return new LoginData(UserID, UserType);
        //    }
        //    else if (HttpContext.Current.Request.Cookies["BasketCount"] != null)
        //    {
        //        int UserID = HttpContext.Current.Request.Cookies["BasketCount"].Value.ToInt();
        //        return new LoginData(UserID, UserType.Admin);
        //    }
        //    else  //0: Guest
        //        return new LoginData(0, UserType.Guest); // 0 is Record of Guest in User_T
        //}





        public static string GetRoute()
        {

            string ActionName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            switch (ActionName.ToLower())
            {
                case "home":
                    return "کارتابل";
                case "grade":
                    return "مقطع تحصیلی";
                case "student":
                    return "دانش آموزان";
                case "profile":
                    return "پروفایل";
                case "transaction":
                    return "بانک";
                case "filing":
                    return "مدیریت فایل ها";
                case "message":
                    return "صندوق ورودی";
                case "gallery":
                    return "گالری";
                case "exams":
                    return "امتحانات";
                case "accepts":
                    return "تائیدیه";
                case "lesson":
                    return "لیست دروس";
                default:
                    return "--";

            }


        }

        public enum GenerateType
        {
            Text,
            Num,
            TextNum
        }
        public static string GenerateName(int count = 6, GenerateType type = GenerateType.TextNum)
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            if (type == GenerateType.Text)
                chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            else if (type == GenerateType.Num)
                chars = "1234567890";

            string res = "";
            Random rd = new Random();
            for (int i = 0; i < count; i++)
            {
                res += chars[rd.Next(0, chars.Length)];
            }
            return res;
        }





    }


    public class PersianDate
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public int Hour { get; set; }
        public int Minute { get; set; }
        public override string ToString()
        {
            return Functions.FixDate($"{Year}/{Month}/{Day}");
        }

        public string Date
        {
            get
            {
                return Functions.FixDate($"{Year}/{Month}/{Day}");
            }
        }

        public string DateTime
        {
            get
            {
                return Functions.FixDate($"{Year}/{Month}/{Day}") + $" {Hour}:{Minute}";
            }
        }
    }

}