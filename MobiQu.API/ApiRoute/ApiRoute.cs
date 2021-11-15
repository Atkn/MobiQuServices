using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiQu.API.ApiRoute
{
    public class ApiRoute
    {
        // api/v1?API_KEY=
        public const string Version = "v1";
        public const string BaseName = "/api";
        public const string BasePath = Version + BaseName;

        public static class SmartBox
        {
            /// <summary>
            /// Müşteriye ait akıllı kutuların listesini getirir
            /// </summary>
            public const string GetSmartBoxes = BasePath + "/smartboxes&API_KEY={API_KEY}&skip={skip}&pageSize={pageSize}";

            /// <summary>
            /// Belirtilen kutu numarasına göre kutu verilerini getirir
            /// </summary>
            public const string GetSmartBoxDetailByNumber = BasePath + "/smartboxnumber&boxNumber={boxNumber}";

            /// <summary>
            /// Belirtilen Id bilgisine göre kutu verilerini getirir
            /// </summary>
            public const string GetSmartBoxDetailById = BasePath + "/smartboxid&boxId={boxId}";
        }

        public static class ColdChainBox
        {
            /// <summary>
            /// Müşteriye ait soğuk zincir kutularını listesini getirir
            /// </summary>
            public const string GetColdChainBoxes = BasePath + "/coldchainboxes&API_KEY={API_KEY}&skip={skip}&pageSize={pageSize}";

            /// <summary>
            /// Belirtilen kutu numarasına göre kutu verilerini getirir
            /// </summary>
            public const string GetColdChainBoxDetailByNumber = BasePath + "/coldchainboxnumber&boxNumber={boxNumber}";

            /// <summary>
            /// Belirtilen Id bilgisine göre kutu verilerini getirir
            /// </summary>
            public const string GetColdChainBoxDetailById = BasePath + "/getcoldchainboxbyid&boxId={boxId}";
        }

        public static class Company
        {

            /// <summary>
            /// Id ile sorgulatılan Şirketin sistem üzerindeki verilerini getirir
            /// </summary>
            public const string GetCompanyInformationById = BasePath + "/companyinformationid&companyId={companyId}";


            /// <summary>
            /// Şirketin sistem üzerindeki verilerini getirir
            /// </summary>
            public const string GetCompanyInformationByApiKey = BasePath + "/companyinformation&API_KEY={API_KEY}";
        }

        public static class Settings
        {
            /// <summary>
            /// Şirketin tablo ayarlarını getirir
            /// </summary>
            public const string GetCompanyTableSettings = BasePath + "/gettablesettings&API_KEY={API_KEY}";
        }
    }
}
