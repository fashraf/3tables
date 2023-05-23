using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace InternalLims.AppCode
{
    public class NIPTdto
    {
        public class AddRequest
        {
            public int NIPTSerNo { get; set; }
            public int TestId { get; set; }
            public int PatientSerNo { get; set; }
            public int HID { get; set; }
            public string ActiveBarcode { get; set; }
            public int NationalId { get; set; }
            public int PatientMRN { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string DOB { get; set; }
            public long Mobile { get; set; }
            public string Email { get; set; }
            public int City { get; set; }
            public string Address { get; set; }
            public int Ethnic { get; set; }
            public string LastMenstrulPeriodDate { get; set; }
            public int AgeOfGestation { get; set; }
            public int PregnancyType { get; set; }
            public int MaternalWeight { get; set; }
            public int MaternalHeight { get; set; }
            public int MarriagConsanguineous { get; set; }
            public int ModeConeption { get; set; }
            public int HistoryGeneticTesting { get; set; }
            public string Others { get; set; }
            public int HistoryofAbortion { get; set; }
            public string LatestUtrasound { get; set; }
            public string SampleCollectionDT { get; set; }
            public string UltrasoundFindings { get; set; }
            public string FurtherClinicalDetails { get; set; }
            public string RequestorName { get; set; }
            public long RequestorMobile { get; set; }
            public string RequstorEmail { get; set; }
            public int RequestorID { get; set; }
            public string ConcentForm { get; set; }
            public bool NIPTStatus { get; set; }
            public List<FileUpload> FileUploadList { get; set; }
            public int FileUploadCount { get; set; }
        }

        public class FileUpload
        {
            public string FileName { get; set; }
            public string FilePathName { get; set; }
            public long RecordID { get; set; }
            public decimal FileSize { get; set; }
            public string FileExtension { get; set; }
        }

        public class JsonMessage
        {
            public string Message { get; set; }
            public bool Success { get; set; }
            public int Id { get; set; }
        }

        #region Utilities
        public static string SerializeToXml(object dataToSerialize)
        {
            try
            {
                if (dataToSerialize == null) return null;
                using (StringWriter stringwriter = new System.IO.StringWriter())
                {
                    XmlSerializer serializer = new XmlSerializer(dataToSerialize.GetType());
                    serializer.Serialize(stringwriter, dataToSerialize, null);
                    return stringwriter.ToString();
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(ex.StackTrace);
                return null;
            }
        }

        public static string CovertDataTableToXML(DataTable dt)
        {
            string xml = "";
            using (StringWriter sw = new StringWriter())
            {
                dt.WriteXml(sw);
                xml = sw.ToString();
            }
            return xml;
        }

        public static string ConvertDataTableToJSON(DataTable dt)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;

        }

        public static string ConvertJsonToXML(string json)
        {
            XmlDocument doc = new XmlDocument();
            doc = JsonConvert.DeserializeXmlNode("{\"root\":" + json + "}", "root");
            return doc.InnerXml;

        }
        #endregion
    }
}