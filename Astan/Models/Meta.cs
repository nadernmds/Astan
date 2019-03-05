using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Astan.Models
{
    [MetadataType(typeof(MetaFaq))]
    partial class Faq
    {

    }
    internal class MetaFaq
    {

        [DisplayName("انتقاد یا پیشنهاد")]
        [Display(Name = "انتقاد یا پیشنهاد")]
        [Required(ErrorMessage ="اجباری است")]
        public string question { get; set; }

    }
    internal class MetaClient
    {
        public long clientID { get; set; }

        [DisplayName("نام مخدوم")]
        [Display(Name = "نام مخدوم")]

        public string name { get; set; }

        [DisplayName("نام پدر")]
        [Display(Name = "نام پدر")]

        public string fatherName { get; set; }

        [DisplayName("کد ملی")]
        [Display(Name = "کد ملی")]


        public string nationalCode { get; set; }

        [DisplayName("شغل")]
        [Display(Name = "شغل")]

        public string jobtitle { get; set; }

        [DisplayName("تاریخ تولد")]
        [Display(Name = "تاریخ تولد")]

        public Nullable<System.DateTime> birthDay { get; set; }

        [DisplayName("محل سکونت")]
        [Display(Name = "محل سکونت")]

        public string homeAdress { get; set; }
        public Nullable<byte> healthStateID { get; set; }

        [DisplayName("شماره موبایل")]
        [Display(Name = "شماره موبایل")]

        public string mobile { get; set; }
        public Nullable<long> mosqueID { get; set; }
        public Nullable<byte> pirorityID { get; set; }

        [DisplayName("نوع خدمت")]
        [Display(Name = "نوع خدمت")]

        public string need { get; set; }

        [DisplayName("وضعیت تاهل")]
        [Display(Name = "وضعیت تاهل")]

        public Nullable<bool> maried { get; set; }
    }
    [MetadataType(typeof(MetaClient))]
    partial class Client
    {
    }


    [MetadataType(typeof(MetaMosque))]
    partial class Mosque
    {
    }

    internal class MetaMosque
    {
        public long mosqueID { get; set; }

        [DisplayName("نام مسجد")]
        [Display(Name = "نام مسجد")]

        public string mosqueName { get; set; }

        [DisplayName("آدرس")]
        [Display(Name = "آدرس")]

        public string adress { get; set; }

        [DisplayName("نام مسئول")]
        [Display(Name = "نام مسئول")]

        public string bossName { get; set; }

        [DisplayName("شماره تماس")]
        [Display(Name = "شماره تماس")]

        public string phone { get; set; }

        [DisplayName("شماره موبایل مسئول")]
        [Display(Name = "شماره موبایل مسئول")]

        public string bossMobile { get; set; }
    }
    [MetadataType(typeof(MetaUser))]
    partial class User
    {
    }

    internal class MetaUser
    {

        [DisplayName("نام کاربری")]
        [Display(Name = "نام کاربری")]
        public string username { get; set; }

        [DisplayName("کلمه عبور")]
        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]

        public string password { get; set; }

        [DisplayName("نام")]
        [Display(Name = "نام")]

        public string name { get; set; }

        [DisplayName("موبایل")]
        [Display(Name = "موبایل")]

        public string mobile { get; set; }
    }

    [MetadataType(typeof(MetaHealthState))]
    partial class HealthState
    {

    }

    internal class MetaHealthState
    {
        public byte healthStateID { get; set; }
        [DisplayName("وضعیت سلامتی")]
        [Display(Name = "وضعیت سلامتی")]

        public string healthStateType { get; set; }
    }

    [MetadataType(typeof(MetaClientMember))]
    partial class ClientMember
    {
    }

    internal class MetaClientMember
    {


        public Nullable<long> clientID { get; set; }

        [DisplayName("نام و نام خانوادگی")]
        [Display(Name = "نام و نام خانوادگی")]

        public string name { get; set; }

        [DisplayName("سن")]
        [Display(Name = "سن")]

        public Nullable<byte> age { get; set; }

        public Nullable<byte> healthStateID { get; set; }

        [DisplayName("نوع خدمت قابل ارائه")]
        [Display(Name = "نوع خدمت قابل ارائه")]

        public string need { get; set; }

    }

    [MetadataType(typeof(MetaUserGroup))]
    partial class userGroup
    {
    }

    internal class MetaUserGroup
    {
        [DisplayName("گروه کاربری")]
        [Display(Name = "گروه کاربری")]

        public string userGroupName { get; set; }

    }
    [MetadataType(typeof(MetaPriority))]
    partial class Piority
    {
    }

    internal class MetaPriority
    {
        [DisplayName("اولویت")]
        [Display(Name = "اولویت")]

        public string priortyType { get; set; }

    }
}