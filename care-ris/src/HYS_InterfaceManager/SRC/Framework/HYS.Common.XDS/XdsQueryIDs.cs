using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Registry.Objects
{
    /// <summary>
    /// Query IDs defined in XDS Registry Store Query
    /// </summary>
    public class XdsQueryIDs
    {
        public const string FindDocuments = "urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d";
        public const string FindSubmissionSets = "urn:uuid:f26abbcb-ac74-4422-8a30-edb644bbc1a9";
        public const string FindFolders = "urn:uuid:958f3006-baad-4929-a4de-ff1114824431";
        public const string GetAll = "urn:uuid:10b545ea-725c-446d-9b95-8aeb444eddf3";
        public const string GetDocuments = "urn:uuid:5c4f972b-d56b-40ac-a5fc-c8ca9b40b9d4";
        public const string GetFolders = "urn:uuid:5737b14c-8a1a-4539-b659-e03a34a5e1e4";
        public const string GetAssociations = "urn:uuid:a7ae438b-4bc2-4642-93e9-be891f7bb155";
        public const string GetDocumentsAndAssociations = "urn:uuid:bab9529a-4a10-40b3-a01f-f68a615d247a";
        public const string GetSubmissionSets = "urn:uuid:51224314-5390-4169-9b91-b1980040715a";
        public const string GetSubmissionSetAndContents = "urn:uuid:e8e3cb2c-e39c-46b9-99e4-c12f57260b83";
        public const string GetFolderAndContents = "urn:uuid:b909a503-523d-4517-8acf-8e5834dfc4c7";
        public const string GetFoldersForDocument = "urn:uuid:10cae35a-c7f9-4cf5-b61e-fc3278ffb578";
        public const string GetRelatedDocuments = "urn:uuid:d90e5407-b356-4d91-a89f-873917b4b0e6";
    }
}
