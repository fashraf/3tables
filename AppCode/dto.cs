using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class dto
    {
        public class InstituteInformation
        {
            public int ID { get; set; }
            public string InstituteName { get; set; }
            public string City { get; set; }
            public string Ownership { get; set; }
            public string MainBranch { get; set; }
            public string CRNumber { get; set; }
            public string Address { get; set; }
            public string Status { get; set; }
            public string StatusCss { get; set; }
        }

        public class NewUserRegistration
        {
            public int UserID { get; set; }
            public string UserTitle { get; set; }
            public string UserFullName { get; set; }
            public string UserMobile { get; set; }
            public string UserEmail { get; set; }
            public string InstituteName { get; set; }
            public string InstituteCity { get; set; }
            public string CreatedDate { get; set; }
            public string UserStatus { get; set; }
            public string TestColor { get; set; }
        }


        public class TestRegistrationList
        {
            public int TestSerno { get; set; }
            public int TestId { get; set; }
            public string Barcode { get; set; }
            public string TestName { get; set; }
            public string SubTestName { get; set; }
            public string InstituteName { get; set; }
            public string NationalId { get; set; }
            public string PatientMRN { get; set; }
            public string Name { get; set; }
            public string RejectStatus { get; set; }
            public string RejectImg { get; set; }
            public string Mobile { get; set; }
            public string CreatedDt { get; set; }
            public string TestStatus { get; set; }
            public string TestColor { get; set; }
            public string TransportCompany { get; set; }
            public string TrackingNumber { get; set; }
            public string PickUpDate { get; set; }

        }


        public class TestKitsRequestList
        {
            public int RequestId { get; set; }
            public int TestId { get; set; }
            public string TestName { get; set; }
            public int SubTestId { get; set; }
            public string SubTestName { get; set; }
            public string InstituteId { get; set; }
            public string InstituteName { get; set; }
            public string City { get; set; }
            public int RequestedTest { get; set; }
            public string ActualTest { get; set; }
            public string Status { get; set; }
            public string CreatedDt { get; set; }
            public string Barcode { get; set; }
            public string TestColor { get; set; }
        }
        public class BarCodeLogs
        {
            public int? Id { get; set; }
            public int TestSemo { get; set; }
            public string BID { get; set; }
            public string ErrorMesssage { get; set; }
            public string IPAddress { get; set; }
        }

        public class TestMasterT
        {
            public int? TestId { get; set; }
            public int? TestSerno { get; set; }
            public int? SubTestId { get; set; }
            public int? TypeId { get; set; }
            public int? InstituteId { get; set; }
            public string BID { get; set; }
            public string PatName { get; set; }
            public DateTime CreatedDt { get; set; }
            public string CreatedBy { get; set; }
            public DateTime ReceiveDt { get; set; }
        }

        public class InstituteBarList
        {
            public int? TestId { get; set; }
            public string BID { get; set; }
            public string InstituteName { get; set; }
            public string TestName { get; set; }
            public string SubTestName { get; set; }
            public string CompletedStatus { get; set; }
            public string CreatedDt { get; set; }
          
        }


        public class PatientList
        {
            public int SerId { get; set; }
            public int PatId { get; set; }
            public string NationalId { get; set; }
            public string MRN { get; set; }
            public string FullName { get; set; }
            public string DOB { get; set; }
            public string Gender { get; set; }
            public string City { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string IsSaudi { get; set; }
        }


        public class TestTatList
        {
            public int TestId { get; set; }
            public string Barcode { get; set; }
            public string TestName { get; set; }
            public string SubTestName { get; set; }
            public string InstituteName { get; set; }
            public string Name { get; set; }
            public string SampleCollectionDT { get; set; }
            public string TestColor { get; set; }
            public string TestStatus { get; set; }
            public string InLabDt { get; set; }
            public string RecievedatNOVO { get; set; }
            
            public string StatusStyle { get; set; }
            public string ActalStatus { get; set; }
            public string DaysLeft { get; set; }
            public string DaysInNovo { get; set; }
        }
    }
}