using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtMatterManagement.Service.Constants
{
    public class ApiResponseMessages
    {
        public const string DataFormat = "DataFormat is incorrect";
        public const string NoClients = "No clients available";
        public const string ClientList = "List of clients";
        public const string ClientNotFound = "Client Not Found";
        public const string ClientDetails = "Client Details";
        public const string ClientCreated= "Client Created Successfully";
        public const string ClientUpdated = "Client Updated Successfully";
        public const string ClientDeleted = "Client Deleted Successfully";

        public const string NoJurisdictions = "No jurisdictions available";
        public const string JurisdictionList = "List of jurisdictions";
        public const string JurisdictionNotFound = "Jurisdiction Not Found";
        public const string JurisdictionDetails = "Jurisdiction Details";
        public const string JurisdictionCreated = "Jurisdiction Created Successfully";
        public const string JurisdictionUpdated = "Jurisdiction Updated Successfully";
        public const string JurisdictionDeleted = "Jurisdiction Deleted Successfully";

        public const string NoAttorneys = "No attorneys available";
        public const string AttorneyList = "List of attorneys";
        public const string AttorneyNotFound = "Attorney Not Found";
        public const string AttorneyDetails = "Attorney Details";
        public const string AttorneyCreated = "Attorney Created Successfully";
        public const string AttorneyUpdated = "Attorney Updated Successfully";
        public const string AttorneyDeleted = "Attorney Deleted Successfully";

        public const string NoInvoices = "No invoices available";
        public const string InvoiceList = "List of invoices";
        public const string InvoiceNotFound = "Invoice Not Found";
        public const string InvoiceDetails = "Invoice Details";
        public const string InvoiceCreated = "Invoice Created Successfully";
        public const string InvoiceUpdated = "Invoice Updated Successfully";
        public const string InvoiceDeleted = "Invoice Deleted Successfully";

        public const string NoMatters = "No matters available";
        public const string MatterList = "List of matters";
        public const string MatterNotFound = "Matter Not Found";
        public const string MatterDetails = "Matter Details";
        public const string MatterCreated = "Matter Created Successfully";
        public const string MatterUpdated = "Matter Updated Successfully";
        public const string MatterDeleted = "Matter Deleted Successfully";

        public const string MattersByClient = "List of Matters by Client";
        public const string InvoicesByMatter = "List of Invoices by Matter";
        public const string InvoicesByAttorney = "List of Invoices by Attorney";
    }
}
