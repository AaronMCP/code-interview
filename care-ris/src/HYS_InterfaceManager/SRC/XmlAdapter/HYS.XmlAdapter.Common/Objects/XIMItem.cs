using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.XmlAdapter.Common.Objects
{
    public class XIMItem
    {
        public static XmlElement ITEM__PROPERTIES = new XmlElement("PROPERTIES", typeof(PROPERTIES));
        public static XmlElement ITEM__SECURITY_AND_PRIVACY = new XmlElement("SECURITY_AND_PRIVACY", typeof(SECURITY_AND_PRIVACY));
        public static XmlElement ITEM__QUERY_RETRIEVE = new XmlElement("QUERY_RETRIEVE", typeof(QUERY_RETRIEVE));
        public static XmlElement ITEM__INSTANCE = new XmlElement("INSTANCE", typeof(INSTANCE));
        public static XmlElement ITEM__PATIENT = new XmlElement("PATIENT", typeof(PATIENT));
        public static XmlElement ITEM__VISIT = new XmlElement("VISIT", typeof(VISIT));
        public static XmlElement ITEM__STUDY = new XmlElement("STUDY", typeof(STUDY));
        public static XmlElement ITEM__SERIES = new XmlElement("SERIES", typeof(SERIES));
        public static XmlElement ITEM__BILLING_AND_MATERIALS = new XmlElement("BILLING_AND_MATERIALS", typeof(BILLING_AND_MATERIALS));
        public static XmlElement ITEM__RADIATION = new XmlElement("RADIATION", typeof(RADIATION));
        public static XmlElement ITEM__ORDER = new XmlElement("ORDER", typeof(ORDER));
        public static XmlElement ITEM__REQUESTED_PROCEDURE = new XmlElement("REQUESTED_PROCEDURE", typeof(REQUESTED_PROCEDURE));
        public static XmlElement ITEM__SCHEDULED_PROCEDURE_STEP = new XmlElement("SCHEDULED_PROCEDURE_STEP[]", typeof(SCHEDULED_PROCEDURE_STEP));
        public static XmlElement ITEM__PERFORMED_PROCEDURE_STEP = new XmlElement("PERFORMED_PROCEDURE_STEP", typeof(PERFORMED_PROCEDURE_STEP));
        public static XmlElement ITEM__OBSERVATION = new XmlElement("OBSERVATION", typeof(OBSERVATION));
        public static XmlElement ITEM__RESULTS = new XmlElement("RESULTS", typeof(RESULTS));
        public static XmlElement ITEM__INTERPRETATION = new XmlElement("INTERPRETATION", typeof(INTERPRETATION));
        public static XmlElement ITEM__ROLE = new XmlElement("ROLE[]", typeof(ROLE));
        public static XmlElement ITEM__NOTES = new XmlElement("NOTES[]", typeof(NOTES));

        public class PROPERTIES
        {
            public static XmlElement CHARACTER_SET = new XmlElement("CHARACTER_SET[]", XIMType.COD);

            public static XmlElement CODING_SCHEME = new XmlElement("CODING_SCHEME[]", typeof(PROPERTIES__CODING_SCHEME));

            public class PROPERTIES__CODING_SCHEME
            {
                public static XmlElement VERSION = new XmlElement("VERSION", XIMType.DTM);
                public static XmlElement DESIGNATOR = new XmlElement("DESIGNATOR");
                public static XmlElement REGISTRY = new XmlElement("REGISTRY");
                public static XmlElement GLOBAL_INDENTIFIER = new XmlElement("GLOBAL_INDENTIFIER");
                public static XmlElement EXTERNAL_INDENTIFIER = new XmlElement("EXTERNAL_INDENTIFIER");
                public static XmlElement NAME = new XmlElement("NAME");
                public static XmlElement RESPONSIBLE_ORGANIZATION = new XmlElement("RESPONSIBLE_ORGANIZATION");
            }

            public static XmlElement TIMEZONE_OFFSET_FROM_UTC = new XmlElement("TIMEZONE_OFFSET_FROM_UTC");
            public static XmlElement COUNTRY = new XmlElement("COUNTRY", XIMType.COD);
            public static XmlElement PRINCIPLE_LANGUAGE = new XmlElement("PRINCIPLE_LANGUAGE", XIMType.COD);

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class SECURITY_AND_PRIVACY
        {
            public static XmlElement ENCRYPTED_DATA = new XmlElement("ENCRYPTED_DATA[]", typeof(SECURITY_AND_PRIVACY__ENCRYPTED_DATA));
            public class SECURITY_AND_PRIVACY__ENCRYPTED_DATA
            {
                public static XmlElement TRANSFER_SYNTAX_IDENTIFIER = new XmlElement("TRANSFER_SYNTAX_IDENTIFIER");
                public static XmlElement CONTENT = new XmlElement("CONTENT");
            }

            public static XmlElement CONFIDENTIALITY = new XmlElement("CONFIDENTIALITY", XIMType.COD);

            public static XmlElement DIGITAL_SIGNATURES = new XmlElement("DIGITAL_SIGNATURES", typeof(SECURITY_AND_PRIVACY__DIGITAL_SIGNATURES));
            public class SECURITY_AND_PRIVACY__DIGITAL_SIGNATURES
            {
                public static XmlElement MAC = new XmlElement("MAC[]", typeof(SECURITY_AND_PRIVACY__DIGITAL_SIGNATURES__MAC));

                public class SECURITY_AND_PRIVACY__DIGITAL_SIGNATURES__MAC
                {
                    public static XmlElement ID = new XmlElement("ID");
                    public static XmlElement ALGORITHM_USED = new XmlElement("ALGORITHM_USED", XIMType.COD);
                    public static XmlElement SIGNED_DATAELEMENT_TRANSFER_SYNTAX = new XmlElement("SIGNED_DATAELEMENT_TRANSFER_SYNTAX");
                    public static XmlElement SIGNED_DATAELEMENT = new XmlElement("SIGNED_DATAELEMENT");
                }

                public static XmlElement SIGNATURE = new XmlElement("SIGNATURE[]", typeof(SECURITY_AND_PRIVACY__DIGITAL_SIGNATURES__SIGNATURE));

                public class SECURITY_AND_PRIVACY__DIGITAL_SIGNATURES__SIGNATURE
                {
                    public static XmlElement MAC_ID_USED = new XmlElement("MAC_ID_USED");
                    public static XmlElement UID = new XmlElement("UID");
                    public static XmlElement DATE = new XmlElement("DATE", XIMType.DAT);
                    public static XmlElement TIME = new XmlElement("TIME", XIMType.TIM);
                    public static XmlElement SIGNERS_CERTIFICATE_TYPE = new XmlElement("SIGNERS_CERTIFICATE_TYPE");
                    public static XmlElement SIGNERS_CERTIFICATE = new XmlElement("SIGNERS_CERTIFICATE");
                    public static XmlElement SIGNATURE = new XmlElement("SIGNATURE");
                    public static XmlElement SIGNATURE_TIMESTAMP = new XmlElement("SIGNATURE_TIMESTAMP");
                    public static XmlElement SIGNATURE_TIMESTAMP_TYPE = new XmlElement("SIGNATURE_TIMESTAMP_TYPE");
                }
            }

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class QUERY_RETRIEVE
        {
            public static XmlElement SOPS_IN_STUDY = new XmlElement("SOPS_IN_STUDY[]");
            public static XmlElement RELATED_STUDY_SERIES = new XmlElement("RELATED_STUDY_SERIES", XIMType.INT);
            public static XmlElement RELATED_STUDY_INSTANCES = new XmlElement("RELATED_STUDY_INSTANCES", XIMType.INT);
            public static XmlElement DESTINATION = new XmlElement("DESTINATION");
            public static XmlElement LEVEL = new XmlElement("LEVEL");

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class INSTANCE
        {
            public static XmlElement IDENTIFICATION = new XmlElement("IDENTIFICATION", typeof(INSTANCE__IDENTIFICATION));
            public class INSTANCE__IDENTIFICATION
            {
                public static XmlElement GLOBAL_IDENTIFIER = new XmlElement("GLOBAL_IDENTIFIER", XIMType.UID);
                public static XmlElement LOCAL_IDENTIFIER = new XmlElement("LOCAL_IDENTIFIER", XIMType.ID);
            }

            public static XmlElement CREATION = new XmlElement("CREATION", typeof(INSTANCE__CREATION));
            public class INSTANCE__CREATION
            {
                public static XmlElement DATE = new XmlElement("DATE", XIMType.DAT);
                public static XmlElement TIME = new XmlElement("TIME", XIMType.TIM);
                public static XmlElement CREATOR = new XmlElement("CODE", XIMType.COD);
            }

            public static XmlElement AUTHORIZATION = new XmlElement("AUTHORIZATION", typeof(INSTANCE__AUTHORIZATION));
            public class INSTANCE__AUTHORIZATION
            {
                public static XmlElement DATE = new XmlElement("DATE", XIMType.DAT);
                public static XmlElement TIME = new XmlElement("TIME", XIMType.TIM);
                public static XmlElement COMMENT = new XmlElement("COMMENT");
                public static XmlElement AUTHORIZOR = new XmlElement("AUTHORIZOR", XIMType.COD);
            }

            public static XmlElement STATUS = new XmlElement("STATUS", XIMType.COD);

            public static XmlElement CONTRIBUTING_EQUIPMENT = new XmlElement("CONTRIBUTING_EQUIPMENT", typeof(INSTANCE__CONTRIBUTING_EQUIPMENT));
            public class INSTANCE__CONTRIBUTING_EQUIPMENT
            {
                public static XmlElement PURPOSE_OF_REFERENCE = new XmlElement("PURPOSE_OF_REFERENCE", XIMType.COD);
                public static XmlElement MANUFACTURER = new XmlElement("MANUFACTURER");
                public static XmlElement STATION_NAME = new XmlElement("STATION_NAME");
                public static XmlElement DEPARTMENT_NAME = new XmlElement("DEPARTMENT_NAME");
                public static XmlElement MODEL = new XmlElement("MODEL");
                public static XmlElement SERIAL_NUMBER = new XmlElement("SERIAL_NUMBER");
                public static XmlElement SOFTWARE_VERSION = new XmlElement("SOFTWARE_VERSION");
                public static XmlElement SPATIAL_RESOLUTION = new XmlElement("SPATIAL_RESOLUTION", XIMType.DEC);
                public static XmlElement CALIBRATION_DATE = new XmlElement("CALIBRATION_DATE", XIMType.DAT);
                public static XmlElement CALIBRATION_TIME = new XmlElement("CALIBRATION_DATE", XIMType.TIM);
                public static XmlElement CONTRIBUTION_DATETIME = new XmlElement("CONTRIBUTION_DATETIME", XIMType.DTM);
                public static XmlElement CONTRIBUTION_DATE = new XmlElement("CONTRIBUTION_DATE", XIMType.DAT);
                public static XmlElement CONTRIBUTION_TIME = new XmlElement("CONTRIBUTION_TIME", XIMType.TIM);
                public static XmlElement CONTRIBUTION_DESCRIPTION = new XmlElement("CONTRIBUTION_DESCRIPTION");
            }

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class PATIENT
        {
            public static XmlElement IDENTIFICATION = new XmlElement("IDENTIFICATION", typeof(PATIENT__IDENTIFICATION));
            public class PATIENT__IDENTIFICATION
            {
                public static XmlElement GLOBAL_IDENTIFIER = new XmlElement("GLOBAL_IDENTIFIER", XIMType.UID);
                public static XmlElement ALTERNATE_GLOBAL_IDENTIFIER = new XmlElement("ALTERNATE_GLOBAL_IDENTIFIER", XIMType.UID);
                public static XmlElement NAME = new XmlElement("NAME[]", XIMType.PNM);
                public static XmlElement ALTERNATE_NAME = new XmlElement("ALTERNATE_NAME[]", XIMType.PNM);
                public static XmlElement ID = new XmlElement("ID[]", XIMType.ID);
                public static XmlElement ALTERNATE_ID = new XmlElement("ALTERNATE_ID[]", XIMType.ID);
                public static XmlElement MOTHERS_MAIDEN_NAME = new XmlElement("MOTHERS_MAIDEN_NAME[]", XIMType.PNM);
                public static XmlElement MEDICAL_RECORD_LOCATOR = new XmlElement("MEDICAL_RECORD_LOCATOR");
                public static XmlElement SOCIAL_SECURITY_NUMBER = new XmlElement("SOCIAL_SECURITY_NUMBER");
                public static XmlElement PATIENT_ACCOUNT_NUMBER = new XmlElement("PATIENT_ACCOUNT_NUMBER", XIMType.ID);
            }

            public static XmlElement PRIOR_IDENTIFICATION = new XmlElement("PRIOR_IDENTIFICATION", typeof(PATIENT__PRIOR_IDENTIFICATION));
            public class PATIENT__PRIOR_IDENTIFICATION
            {
                public static XmlElement PRIOR_ID = new XmlElement("PRIOR_ID[]", XIMType.ID);
                public static XmlElement PRIOR_ALTERNATE_ID = new XmlElement("PRIOR_ALTERNATE_ID[]", XIMType.ID);
                public static XmlElement PRIOR_PATIENT_ACCOUNT_NUMBER = new XmlElement("PRIOR_PATIENT_ACCOUNT_NUMBER", XIMType.ID);
                public static XmlElement PRIOR_NAME = new XmlElement("PRIOR_NAME[]", XIMType.PNM);
            }

            public static XmlElement DEMOGRAPHIC = new XmlElement("DEMOGRAPHIC", typeof(PATIENT__DEMOGRAPHIC));
            public class PATIENT__DEMOGRAPHIC
            {
                public static XmlElement BIRTH_DATE = new XmlElement("BIRTH_DATE", XIMType.DAT);
                public static XmlElement BIRTH_TIME = new XmlElement("BIRTH_TIME", XIMType.TIM);
                public static XmlElement AGE = new XmlElement("AGE", XIMType.MSR);
                public static XmlElement HEIGHT = new XmlElement("HEIGHT", XIMType.MSR);
                public static XmlElement WEIGHT = new XmlElement("WEIGHT", XIMType.MSR);
                public static XmlElement SEX = new XmlElement("SEX", XIMType.COD);
                public static XmlElement ETHNIC_GROUP = new XmlElement("ETHNIC_GROUP[]", XIMType.COD);
                public static XmlElement RELIGION = new XmlElement("RELIGION", XIMType.COD);

                public static XmlElement PRIMARY_LANGUAGE = new XmlElement("PRIMARY_LANGUAGE[]", typeof(PATIENT__DEMOGRAPHIC__PRIMARY_LANGUAGE));
                public class PATIENT__DEMOGRAPHIC__PRIMARY_LANGUAGE
                {
                    public static XmlElement IDENTIFIER = new XmlElement("IDENTIFIER", XIMType.COD);
                    public static XmlElement PRIMARY_LANGUAGE_MODIFIER = new XmlElement("PRIMARY_LANGUAGE_MODIFIER", XIMType.COD);
                }

                public static XmlElement CITIZENSHIP = new XmlElement("CITIZENSHIP[]", XIMType.COD);
                public static XmlElement ADDRESS = new XmlElement("ADDRESS[]",  XIMType.ADR );
                public static XmlElement TELEPHONE_NUMBER = new XmlElement("TELEPHONE_NUMBER[]", XIMType.PHN );

                public static XmlElement MILITARY = new XmlElement("MILITARY", typeof(PATIENT__DEMOGRAPHIC__MILITARY));
                public class PATIENT__DEMOGRAPHIC__MILITARY
                {
                    public static XmlElement STATUS = new XmlElement("STATUS", XIMType.COD);
                    public static XmlElement BRANCH = new XmlElement("BRANCH", XIMType.COD);
                }

                public static XmlElement OCCUPATION = new XmlElement("OCCUPATION");
                public static XmlElement INSURANCE_PLAN = new XmlElement("INSURANCE_PLAN[]", XIMType.COD);
                public static XmlElement SPECIES_CODE = new XmlElement("SPECIES_CODE", XIMType.COD);
                public static XmlElement BREED_CODE = new XmlElement("BREED_CODE", XIMType.COD);
            }

            public static XmlElement MEDICAL = new XmlElement("MEDICAL", typeof(PATIENT__MEDICAL));
            public class PATIENT__MEDICAL
            {
                public static XmlElement MEDICAL_ALERT = new XmlElement("MEDICAL_ALERT[]", XIMType.COD);
                public static XmlElement SPECIAL_NEED = new XmlElement("SPECIAL_NEED[]", XIMType.COD);
                public static XmlElement CONDITION = new XmlElement("CONDITION[]", XIMType.COD);
                
                public static XmlElement ALLERGY = new XmlElement("ALLERGY[]", typeof(PATIENT__MEDICAL__ALLERGY));
                public class PATIENT__MEDICAL__ALLERGY
                {
                    public static XmlElement TYPE = new XmlElement("TYPE", XIMType.COD);
                    public static XmlElement DESCRIPTION = new XmlElement("DESCRIPTION", XIMType.COD);
                    public static XmlElement SEVERITY = new XmlElement("SEVERITY", XIMType.COD);
                    public static XmlElement REACTION_DESCRIPTION = new XmlElement("REACTION_DESCRIPTION");
                    public static XmlElement IDENTIFICATION_DATE = new XmlElement("IDENTIFICATION_DATE", XIMType.DAT);
                }
                
                public static XmlElement SMOKING_STATUS = new XmlElement("SMOKING_STATUS", XIMType.COD);
                public static XmlElement PREGNANCY_STATUS = new XmlElement("PREGNANCY_STATUS", XIMType.COD);
                public static XmlElement LAST_MENSTRAL_DATE = new XmlElement("LAST_MENSTRAL_DATE", XIMType.DAT);
                public static XmlElement ADDITIONAL_PATIENT_HISTORY = new XmlElement("ADDITIONAL_PATIENT_HISTORY");
            }

            public static XmlElement COMMENTS = new XmlElement("COMMENTS");
            public static XmlElement NUMBER_OF_STUDIES = new XmlElement("NUMBER_OF_STUDIES", XIMType.INT);
            public static XmlElement NUMBER_OF_SERIES = new XmlElement("NUMBER_OF_SERIES", XIMType.INT);
            public static XmlElement NUMBER_OF_INSTANCES = new XmlElement("NUMBER_OF_INSTANCES", XIMType.INT);

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class VISIT
        {
            public static XmlElement IDENTIFICATION = new XmlElement("IDENTIFICATION", typeof(VISIT__IDENTIFICATION));
            public class VISIT__IDENTIFICATION
            {
                public static XmlElement GLOBAL_IDENTIFIER = new XmlElement("GLOBAL_IDENTIFIER", XIMType.UID);
                public static XmlElement LOCAL_IDENTIFIER = new XmlElement("LOCAL_IDENTIFIER", XIMType.ID);
                public static XmlElement INSTITUTION = new XmlElement("INSTITUTION", XIMType.INS);
                public static XmlElement VISIT_INDICATOR = new XmlElement("VISIT_INDICATOR", XIMType.COD);
            }

            public static XmlElement SCHEDULING = new XmlElement("SCHEDULING", typeof(VISIT__SCHEDULING));
            public class VISIT__SCHEDULING
            {
                public static XmlElement SCHEDULED_ADMISSION_DATE = new XmlElement("SCHEDULED_ADMISSION_DATE", XIMType.DAT);
                public static XmlElement SCHEDULED_ADMISSION_TIME = new XmlElement("SCHEDULED_ADMISSION_TIME", XIMType.TIM);
                public static XmlElement SCHEDULED_DISCHARGE_DATE = new XmlElement("SCHEDULED_DISCHARGE_DATE", XIMType.DAT);
                public static XmlElement SCHEDULED_DISCHARGE_TIME = new XmlElement("SCHEDULED_DISCHARGE_TIME", XIMType.TIM);
                public static XmlElement SCHEDULED_PATIENT_RESIDENCE = new XmlElement("SCHEDULED_PATIENT_RESIDENCE", XIMType.LOC);
            }

            public static XmlElement STATUS = new XmlElement("STATUS", typeof(VISIT__STATUS));
            public class VISIT__STATUS
            {
                public static XmlElement IDENTIFIER = new XmlElement("IDENTIFIER", XIMType.COD);
                public static XmlElement PATIENT_RESIDENCE = new XmlElement("PATIENT_RESIDENCE", XIMType.LOC);
                public static XmlElement PATIENT_LOCATION = new XmlElement("PATIENT_LOCATION", XIMType.LOC);
                public static XmlElement PRIOR_PATIENT_LOCATION = new XmlElement("PRIOR_PATIENT_LOCATION", XIMType.LOC);
                public static XmlElement TEMPORARY_LOCATION = new XmlElement("TEMPORARY_LOCATION", XIMType.LOC);
                public static XmlElement PRIOR_TEMPORARY_LOCATION = new XmlElement("PRIOR_TEMPORARY_LOCATION", XIMType.LOC);
                public static XmlElement PENDING_LOCATION = new XmlElement("PENDING_LOCATION", XIMType.LOC);
                public static XmlElement PRIOR_PENDING_LOCATION = new XmlElement("PRIOR_PENDING_LOCATION", XIMType.LOC);
                public static XmlElement PRIOR_VISIT_NUMBER = new XmlElement("PRIOR_VISIT_NUMBER", XIMType.ID);
                public static XmlElement PRIOR_ALTERNATE_VISIT_ID = new XmlElement("PRIOR_ALTERNATE_VISIT_ID", XIMType.ID);
                public static XmlElement ALTERNATE_VISIT_ID = new XmlElement("ALTERNATE_VISIT_ID", XIMType.ID);
            }

            public static XmlElement ADMISSION = new XmlElement("ADMISSION", typeof(VISIT__ADMISSION));
            public class VISIT__ADMISSION
            {
                public static XmlElement REFERRING_PHYSICIAN = new XmlElement("REFERRING_PHYSICIAN", XIMType.PHY);

                public static XmlElement DIAGNOSIS = new XmlElement("DIAGNOSIS", typeof(VISIT__ADMISSION__DIAGNOSIS));
                public class VISIT__ADMISSION__DIAGNOSIS
                {
                    public static XmlElement DESCRIPTION = new XmlElement("DESCRIPTION[]", XIMType.STR);
                    public static XmlElement IDENTIFIER = new XmlElement("IDENTIFIER[]", XIMType.COD);
                    public static XmlElement CODING_METHOD = new XmlElement("CODING_METHOD", XIMType.ID);
                    public static XmlElement TYPE = new XmlElement("TYPE", XIMType.COD);
                    public static XmlElement MAJOR_DIAGNOSTIC_CATEGORY = new XmlElement("MAJOR_DIAGNOSTIC_CATEGORY", XIMType.COD);
                    public static XmlElement RELATED_GROUP = new XmlElement("RELATED_GROUP", XIMType.COD);
                    public static XmlElement APPROVAL_INDICATOR = new XmlElement("APPROVAL_INDICATOR", XIMType.ID);
                    public static XmlElement GROUPER_REVIEW_CODE = new XmlElement("GROUPER_REVIEW_CODE", XIMType.COD);
                    public static XmlElement OUTLIER_TYPE = new XmlElement("OUTLIER_TYPE", XIMType.COD);
                    public static XmlElement OUTLIER_DAYS = new XmlElement("OUTLIER_DAYS", XIMType.DEC);
                    public static XmlElement OUTLIER_COST = new XmlElement("OUTLIER_COST", XIMType.CP);
                    public static XmlElement GROUPER_VERSION_TYPE = new XmlElement("GROUPER_VERSION_TYPE", XIMType.STR);
                    public static XmlElement ACTION_CODE = new XmlElement("ACTION_CODE", XIMType.COD);
                }

                public static XmlElement ROUTE_OF_ADMISSIONS = new XmlElement("ROUTE_OF_ADMISSIONS", XIMType.COD);
                public static XmlElement DATE = new XmlElement("DATE", XIMType.DAT);
                public static XmlElement TIME = new XmlElement("TIME", XIMType.TIM);
                public static XmlElement ATTENDING_PHYSICIAN = new XmlElement("ATTENDING_PHYSICIAN[]", XIMType.PHY);
                public static XmlElement CONSULTING_DOCTOR = new XmlElement("CONSULTING_DOCTOR[]", XIMType.PHY);
                public static XmlElement HOSPITAL_SERVICE = new XmlElement("HOSPITAL_SERVICE", XIMType.COD);
                public static XmlElement ADMITTING_DOCTOR = new XmlElement("ADMITTING_DOCTOR[]", XIMType.PHY);
                public static XmlElement PATIENT_TYPE = new XmlElement("PATIENT_TYPE", XIMType.COD);
                public static XmlElement OTHER_HEALTHCARE_PROVIDER = new XmlElement("OTHER_HEALTHCARE_PROVIDER[]", XIMType.PHY);
            }

            public static XmlElement DISCHARGE = new XmlElement("DISCHARGE", typeof(VISIT__DISCHARGE));
            public class VISIT__DISCHARGE
            {
                public static XmlElement DATE = new XmlElement("DATE", XIMType.DAT);
                public static XmlElement TIME = new XmlElement("TIME", XIMType.TIM);

                public static XmlElement DIAGNOSIS = new XmlElement("DIAGNOSIS", typeof(VISIT__DISCHARGE__DIAGNOSIS));
                public class VISIT__DISCHARGE__DIAGNOSIS
                {
                    public static XmlElement DESCRIPTION = new XmlElement("DESCRIPTION[]", XIMType.STR);
                    public static XmlElement IDENTIFIER = new XmlElement("IDENTIFIER[]", XIMType.COD);
                }
            }

            public static XmlElement COMMENTS = new XmlElement("COMMENTS");

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class STUDY
        {
            public static XmlElement IDENTIFICATION = new XmlElement("IDENTIFICATION", typeof(STUDY__IDENTIFICATION));
            public class STUDY__IDENTIFICATION
            {
                public static XmlElement GLOBAL_IDENTIFIER = new XmlElement("GLOBAL_IDENTIFIER", XIMType.UID);
                public static XmlElement LOCAL_IDENTIFIER = new XmlElement("LOCAL_IDENTIFIER", XIMType.ID);
                public static XmlElement OTHER_LOCAL_IDENTIFIER = new XmlElement("OTHER_LOCAL_IDENTIFIER[]", XIMType.ID);
            }

            public static XmlElement DESCRIPTION = new XmlElement("DESCRIPTION", typeof(STUDY__DESCRIPTION));
            public class STUDY__DESCRIPTION
            {
                public static XmlElement LOCAL_DESCRIPTION = new XmlElement("LOCAL_DESCRIPTION", XIMType.COD);
                public static XmlElement PHYSICIAN_OF_RECORD = new XmlElement("PHYSICIAN_OF_RECORD[]", XIMType.PHY);
            }

            public static XmlElement SCHEDULING = new XmlElement("SCHEDULING", typeof(STUDY__SCHEDULING));
            public class STUDY__SCHEDULING
            {
                public static XmlElement SCHEDULED_START_DATE = new XmlElement("SCHEDULED_START_DATE", XIMType.DAT);
                public static XmlElement SCHEDULED_START_TIME = new XmlElement("SCHEDULED_START_TIME", XIMType.TIM);
                public static XmlElement SCHEDULED_STOP_DATE = new XmlElement("SCHEDULED_STOP_DATE", XIMType.DAT);
                public static XmlElement SCHEDULED_STOP_TIME = new XmlElement("SCHEDULED_STOP_TIME", XIMType.TIM);
                public static XmlElement SCHEDULED_LOCATION = new XmlElement("SCHEDULED_LOCATION", XIMType.LOC);
                public static XmlElement SCHEDULED_AE_TITLE = new XmlElement("SCHEDULED_AE_TITLE[]", XIMType.STR);
                public static XmlElement REASON_FOR_STUDY = new XmlElement("REASON_FOR_STUDY[]", XIMType.COD);
            }

            public static XmlElement STATUS = new XmlElement("STATUS", typeof(STUDY__STATUS));
            public class STUDY__STATUS
            {
                public static XmlElement STATUS = new XmlElement("STATUS", XIMType.COD);
                public static XmlElement PRIORITY = new XmlElement("PRIORITY", XIMType.COD);
            }

            public static XmlElement COMMENTS = new XmlElement("COMMENTS");

            public static XmlElement ACQUISITION = new XmlElement("ACQUISITION", typeof(STUDY__ACQUISITION));
            public class STUDY__ACQUISITION
            {
                public static XmlElement ARRIVAL_DATE = new XmlElement("ARRIVAL_DATE", XIMType.DAT);
                public static XmlElement ARRIVAL_TIME = new XmlElement("ARRIVAL_TIME", XIMType.TIM);
                public static XmlElement START_DATE = new XmlElement("START_DATE", XIMType.DAT);
                public static XmlElement START_TIME = new XmlElement("START_TIME", XIMType.TIM);
                public static XmlElement COMPLETION_DATE = new XmlElement("COMPLETION_DATE", XIMType.DAT);
                public static XmlElement COMPLETION_TIME = new XmlElement("COMPLETION_TIME", XIMType.TIM);
                public static XmlElement VERIFIED_DATE = new XmlElement("VERIFIED_DATE", XIMType.DAT);
                public static XmlElement VERIFIED_TIME = new XmlElement("VERIFIED_TIME", XIMType.TIM);
                public static XmlElement CONTAINED_MODALITY = new XmlElement("CONTAINED_MODALITY[]", XIMType.STR);
                public static XmlElement NUMBER_OF_SERIES = new XmlElement("NUMBER_OF_SERIES", XIMType.INT);
                public static XmlElement NUMBER_OF_ACQUISITIONS = new XmlElement("NUMBER_OF_ACQUISITIONS", XIMType.INT);
                public static XmlElement NUMBER_OF_INSTANCES = new XmlElement("NUMBER_OF_INSTANCES", XIMType.INT);
            }

            public static XmlElement READ = new XmlElement("READ", typeof(STUDY__READ));
            public class STUDY__READ
            {
                public static XmlElement READING_PHYSICIAN = new XmlElement("READING_PHYSICIAN[]", XIMType.PHY);
                public static XmlElement READ_DATE = new XmlElement("READ_DATE", XIMType.DAT);
                public static XmlElement READ_TIME = new XmlElement("READ_TIME", XIMType.TIM);
            }

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class SERIES
        {
            public static XmlElement DENTIFICATION = new XmlElement("DENTIFICATION", typeof(SERIES__IDENTIFICATION));
            public class SERIES__IDENTIFICATION
            {
                public static XmlElement GLOBAL_IDENTIFIER = new XmlElement("GLOBAL_IDENTIFIER", XIMType.UID);
                public static XmlElement LOCAL_IDENTIFIER = new XmlElement("LOCAL_IDENTIFIER", XIMType.ID);
            }

            public static XmlElement DESCRIPTION = new XmlElement("DESCRIPTION", typeof(SERIES__DESCRIPTION));
            public class SERIES__DESCRIPTION
            {
                public static XmlElement MODALITY = new XmlElement("MODALITY", XIMType.COD);
                public static XmlElement BODY_PART_EXAMINED = new XmlElement("BODY_PART_EXAMINED", XIMType.COD);
                public static XmlElement LATERALITY = new XmlElement("LATERALITY", XIMType.STR);
                public static XmlElement PATIENT_POSITION = new XmlElement("PATIENT_POSITION", XIMType.STR);
                public static XmlElement LOCAL_DESCRIPTION = new XmlElement("LOCAL_DESCRIPTION", XIMType.STR);
                public static XmlElement SMALLEST_PIXEL_VALUE = new XmlElement("SMALLEST_PIXEL_VALUE", XIMType.DEC);
                public static XmlElement LARGEST_PIXEL_VALUE = new XmlElement("LARGEST_PIXEL_VALUE", XIMType.DEC);
                public static XmlElement PROTOCOL = new XmlElement("PROTOCOL", XIMType.COD);
            }

            public static XmlElement ACQUISITION = new XmlElement("ACQUISITION", typeof(SERIES__ACQUISITION));
            public class SERIES__ACQUISITION
            {
                public static XmlElement START_DATE = new XmlElement("START_DATE", XIMType.DAT);
                public static XmlElement START_TIME = new XmlElement("START_TIME", XIMType.TIM);
                public static XmlElement OPERATOR = new XmlElement("OPERATOR[]", XIMType.PHY);
                public static XmlElement PERFORMING_PHYSICIAN = new XmlElement("PERFORMING_PHYSICIAN[]", XIMType.PHY);
            }

            public static XmlElement STORAGE = new XmlElement("STORAGE", typeof(SERIES__STORAGE));
            public class SERIES__STORAGE
            {
                public static XmlElement RETRIEVE_AE_TITLE = new XmlElement("RETRIEVE_AE_TITLE[]", XIMType.STR);
                public static XmlElement STORAGE_MEDIA_FILESET_LOCAL_ID = new XmlElement("STORAGE_MEDIA_FILESET_LOCAL_ID", XIMType.STR);
                public static XmlElement STORAGE_MEDIA_FILESET_GLOBAL_ID = new XmlElement("STORAGE_MEDIA_FILESET_GLOBAL_ID", XIMType.STR);
            }

            public static XmlElement IMAGE_INSTANCE = new XmlElement("IMAGE_INSTANCE", typeof(SERIES__IMAGE_INSTANCE));
            public class SERIES__IMAGE_INSTANCE
            {
                public static XmlElement CLASS_UID = new XmlElement("CLASS_UID");
                public static XmlElement INSTANCE_UID = new XmlElement("INSTANCE_UID");
                public static XmlElement FRAME_NUMBER = new XmlElement("FRAME_NUMBER", XIMType.INT);
                public static XmlElement PURPOSE_OF_REFERENCE = new XmlElement("PURPOSE_OF_REFERENCE", XIMType.COD);
                public static XmlElement RETRIEVE_AE_TITLE = new XmlElement("RETRIEVE_AE_TITLE[]", XIMType.STR);
                public static XmlElement STORAGE_MEDIA_FILESET_LOCAL_ID = new XmlElement("STORAGE_MEDIA_FILESET_LOCAL_ID", XIMType.STR);
                public static XmlElement STORAGE_MEDIA_FILESET_GLOBAL_ID = new XmlElement("STORAGE_MEDIA_FILESET_GLOBAL_ID", XIMType.STR);
            }

            public static XmlElement NON_IMAGE_INSTANCE = new XmlElement("NON_IMAGE_INSTANCE", XIMType.UID);

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class BILLING_AND_MATERIALS
        {
            public static XmlElement PROCEDURE_STEP_CODE = new XmlElement("PROCEDURE_STEP_CODE[]", XIMType.COD);
            
            public static XmlElement FILM_CONSUMPTION = new XmlElement("FILM_CONSUMPTION[]", typeof(BILLING_AND_MATERIALS__FILM_CONSUMPTION));
            public class BILLING_AND_MATERIALS__FILM_CONSUMPTION
            {
                public static XmlElement NUMBER_OF_FILMS = new XmlElement("NUMBER_OF_FILMS", XIMType.INT);
                public static XmlElement MEDIUM_TYPE = new XmlElement("MEDIUM_TYPE", XIMType.COD);
                public static XmlElement MEDIUM_SIZE = new XmlElement("MEDIUM_SIZE", XIMType.COD);
            }
            
            public static XmlElement SUPPLY_OR_DEVICE = new XmlElement("SUPPLY_OR_DEVICE[]", typeof(BILLING_AND_MATERIALS__SUPPLY_OR_DEVICE));
            public class BILLING_AND_MATERIALS__SUPPLY_OR_DEVICE
            {
                public static XmlElement ITEM = new XmlElement("ITEM", XIMType.COD);
                public static XmlElement QUANTITY = new XmlElement("QUANTITY", XIMType.MSR);
            }

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class RADIATION
        {
            public static XmlElement EXPOSED_REGION = new XmlElement("EXPOSED_REGION[]", XIMType.COD);
            public static XmlElement TOTAL_FLUOROSCOPY_TIME = new XmlElement("TOTAL_FLUOROSCOPY_TIME", XIMType.MSR);
            public static XmlElement TOTAL_NUMBER_OF_EXPOSURES = new XmlElement("TOTAL_NUMBER_OF_EXPOSURES", XIMType.INT);
            public static XmlElement DISTANCE_SOURCE_TO_DETECTOR = new XmlElement("DISTANCE_SOURCE_TO_DETECTOR", XIMType.MSR);
            public static XmlElement DISTANCE_SOURCE_TO_ENTRANCE = new XmlElement("DISTANCE_SOURCE_TO_ENTRANCE", XIMType.MSR);
            public static XmlElement ENTRANCE_DOSE = new XmlElement("ENTRANCE_DOSE", XIMType.MSR);

            public static XmlElement EXPOSED_AREA_DIMENSIONS = new XmlElement("EXPOSED_AREA_DIMENSIONS", typeof(RADIATION__EXPOSED_AREA_DIMENSIONS));
            public class RADIATION__EXPOSED_AREA_DIMENSIONS
            {
                public static XmlElement SHAPE = new XmlElement("SHAPE", XIMType.STR);
                public static XmlElement DIAMETER = new XmlElement("DIAMETER", XIMType.MSR);
                public static XmlElement ROW = new XmlElement("ROW", XIMType.MSR);
                public static XmlElement COLUMN = new XmlElement("COLUMN", XIMType.MSR);
            }

            public static XmlElement AREA_DOSE_PRODUCT = new XmlElement("AREA_DOSE_PRODUCT", XIMType.MSR);
            public static XmlElement COMMENTS = new XmlElement("COMMENTS", XIMType.STR);

            public static XmlElement EXPOSURE_DOSE = new XmlElement("EXPOSURE_DOSE[]", typeof(RADIATION__EXPOSURE_DOSE));
            public class RADIATION__EXPOSURE_DOSE
            {

                public static XmlElement RADIATION_MODE = new XmlElement("RADIATION_MODE", XIMType.COD);
                public static XmlElement GENERATOR_OUTPUT = new XmlElement("GENERATOR_OUTPUT", XIMType.MSR);
                public static XmlElement TUBE_CURRENT = new XmlElement("TUBE_CURRENT", XIMType.MSR);
                public static XmlElement EXPOSURE_DURATION = new XmlElement("EXPOSURE_DURATION", XIMType.MSR);
                public static XmlElement FILTER_TYPE = new XmlElement("FILTER_TYPE", XIMType.COD);
                public static XmlElement FILTER_MATERIAL = new XmlElement("FILTER_MATERIAL", XIMType.COD); 
            }

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class ORDER
        {
            public static XmlElement IDENTIFICATION = new XmlElement("IDENTIFICATION", typeof(ORDER__IDENTIFICATION));
            public class ORDER__IDENTIFICATION
            {
                public static XmlElement PLACER_ORDER_NUMBER = new XmlElement("PLACER_ORDER_NUMBER", XIMType.ID);
                public static XmlElement FILLER_ORDER_NUMBER = new XmlElement("FILLER_ORDER_NUMBER", XIMType.ID);
                public static XmlElement ACCESSION_NUMBER = new XmlElement("ACCESSION_NUMBER", XIMType.ID);
                public static XmlElement PLACER_GROUP_NUMBER = new XmlElement("PLACER_GROUP_NUMBER", XIMType.ID);

                public static XmlElement PARENT = new XmlElement("PARENT", typeof(ORDER__IDENTIFICATION__PARENT));
                public static class ORDER__IDENTIFICATION__PARENT
                {
                    public static XmlElement PLACER_ASSIGNED_ID = new XmlElement("PLACER_ASSIGNED_ID", XIMType.ID);
                    public static XmlElement FILLER_ASSIGNED_ID = new XmlElement("FILLER_ASSIGNED_ID", XIMType.ID);
                }

                public static XmlElement PLACER_FIELD1 = new XmlElement("PLACER_FIELD1", XIMType.STR);
                public static XmlElement PLACER_FIELD2 = new XmlElement("PLACER_FIELD2", XIMType.STR);
                public static XmlElement FILLER_FIELD1 = new XmlElement("FILLER_FIELD1", XIMType.STR);
                public static XmlElement FILLER_FIELD2 = new XmlElement("FILLER_FIELD2", XIMType.STR);
            }

            public static XmlElement REASON = new XmlElement("REASON[]", XIMType.COD);
            public static XmlElement COMMENTS = new XmlElement("COMMENTS", XIMType.STR);
            public static XmlElement ISSUE_DATE = new XmlElement("ISSUE_DATE", XIMType.DAT);
            public static XmlElement ISSUE_TIME = new XmlElement("ISSUE_TIME", XIMType.TIM);

            public static XmlElement ENTERER = new XmlElement("ENTERER[]", typeof(ORDER__ENTERER));
            public class ORDER__ENTERER
            {
                public static XmlElement NAME = new XmlElement("NAME", XIMType.PNM);
                public static XmlElement IDENTIFICATION = new XmlElement("IDENTIFICATION", XIMType.ID);
                public static XmlElement AUTHORIZATION_MODE = new XmlElement("AUTHORIZATION_MODE", XIMType.CON);
            }

            public static XmlElement ENTERING_LOCATION = new XmlElement("ENTERING_LOCATION", XIMType.COD);
            public static XmlElement ENTERERS_LOCATION = new XmlElement("ENTERERS_LOCATION", XIMType.LOC);
            public static XmlElement CALLBACK_PHONE_NUMBER = new XmlElement("CALLBACK_PHONE_NUMBER[]", XIMType.PHN);
            public static XmlElement REQUESTING_PHYSICIAN = new XmlElement("REQUESTING_PHYSICIAN[]", XIMType.PHY);
            public static XmlElement REQUESTING_SERVICE = new XmlElement("REQUESTING_SERVICE", XIMType.COD);
            public static XmlElement STATUS = new XmlElement("STATUS", XIMType.ID);
            public static XmlElement PROVIDER = new XmlElement("PROVIDER[]", XIMType.PNM);

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class REQUESTED_PROCEDURE
        {
            public static XmlElement DENTIFICATION = new XmlElement("DENTIFICATION", typeof(REQUESTED_PROCEDURE__IDENTIFICATION));
            public class REQUESTED_PROCEDURE__IDENTIFICATION
            {
                public static XmlElement GLOBAL_IDENTIFIER = new XmlElement("GLOBAL_IDENTIFIER", XIMType.UID);
                public static XmlElement LOCAL_IDENTIFIER = new XmlElement("LOCAL_IDENTIFIER", XIMType.ID);
            }

            public static XmlElement REFERENCED_STUDY = new XmlElement("REFERENCED_STUDY[]", XIMType.UID);
            public static XmlElement REASON = new XmlElement("REASON", XIMType.COD);
            public static XmlElement COMMENTS = new XmlElement("COMMENTS", XIMType.STR);
            public static XmlElement DESCRIPTOR = new XmlElement("DESCRIPTOR[]", XIMType.COD);

            public static XmlElement QUANTITY_AND_TIMING = new XmlElement("QUANTITY_AND_TIMING", typeof(REQUESTED_PROCEDURE__QUANTITY_AND_TIMING));
            public class REQUESTED_PROCEDURE__QUANTITY_AND_TIMING
            {
                public static XmlElement PRIORITY = new XmlElement("PRIORITY", XIMType.COD);
                public static XmlElement QUANTITY = new XmlElement("QUANTITY", XIMType.MSR);

                public static XmlElement INTERVAL = new XmlElement("INTERVAL", typeof(REQUESTED_PROCEDURE__QUANTITY_AND_TIMING__INTERVAL));
                public class REQUESTED_PROCEDURE__QUANTITY_AND_TIMING__INTERVAL
                {
                    public static XmlElement REPEAT_PATTERN = new XmlElement("REPEAT_PATTERN", XIMType.COD);
                    public static XmlElement INTERVAL = new XmlElement("INTERVAL", XIMType.STR);
                }

                public static XmlElement START_END_DATES = new XmlElement("START_END_DATES", XIMType.DR);
                public static XmlElement CONDITION = new XmlElement("CONDITION", XIMType.STR);
                public static XmlElement TEXT = new XmlElement("TEXT", XIMType.STR);
                public static XmlElement CONJUNCTION = new XmlElement("CONJUNCTION", XIMType.COD);

                public static XmlElement ORDER_SEQUENCING = new XmlElement("ORDER_SEQUENCING", typeof(REQUESTED_PROCEDURE__QUANTITY_AND_TIMING__ORDER_SEQUENCING));
                public class REQUESTED_PROCEDURE__QUANTITY_AND_TIMING__ORDER_SEQUENCING
                {
                    public static XmlElement PLACER_ORDER = new XmlElement("PLACER_ORDER", XIMType.HD);
                    public static XmlElement FILLER_ORDER = new XmlElement("FILLER_ORDER", XIMType.HD);
                    public static XmlElement SEQUENCE_RESULTS_FLAG = new XmlElement("SEQUENCE_RESULTS_FLAG", XIMType.COD);
                    public static XmlElement SEQUENCE_CONDITION_VALUE = new XmlElement("SEQUENCE_CONDITION_VALUE", XIMType.STR);
                    public static XmlElement MAXIMUM_NUMBER_OF_REPEATS = new XmlElement("MAXIMUM_NUMBER_OF_REPEATS", XIMType.DEC);
                }

                public static XmlElement OCCURANCE_DURATION = new XmlElement("OCCURANCE_DURATION", XIMType.COD);
                public static XmlElement TOTAL_OCCURRANCES = new XmlElement("TOTAL_OCCURRANCES", XIMType.DEC);
            }

            public static XmlElement PATIENT_TRANSPORTATION = new XmlElement("PATIENT_TRANSPORTATION", XIMType.COD);
            public static XmlElement LOCATION = new XmlElement("LOCATION", XIMType.LOC);
            public static XmlElement CONFIDENTIALITY = new XmlElement("CONFIDENTIALITY", XIMType.COD);
            public static XmlElement REPORTING_PRIORITY = new XmlElement("REPORTING_PRIORITY", XIMType.COD);

            public static XmlElement RESULTS_RECIPIENT = new XmlElement("RESULTS_RECIPIENT[]", typeof(REQUESTED_PROCEDURE__RESULTS_RECIPIENT));
            public class REQUESTED_PROCEDURE__RESULTS_RECIPIENT
            {
                public static XmlElement NAME = new XmlElement("NAME", XIMType.PNM);
                public static XmlElement PERSON_CODE = new XmlElement("PERSON_CODE[]", XIMType.COD);
                public static XmlElement ADDRESS = new XmlElement("ADDRESS", XIMType.ADR);
                public static XmlElement TELEPHONE_NUMBER = new XmlElement("TELEPHONE_NUMBER[]", XIMType.PHN);
                public static XmlElement INSTITUTION = new XmlElement("INSTITUTION", XIMType.INS);
            }

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class SCHEDULED_PROCEDURE_STEP
        {
            public static XmlElement TRANSACTION_UID = new XmlElement("TRANSACTION_UID", XIMType.STR);

            public static XmlElement IDENTIFICATION = new XmlElement("IDENTIFICATION", typeof(SCHEDULED_PROCEDURE_STEP__IDENTIFICATION));
            public class SCHEDULED_PROCEDURE_STEP__IDENTIFICATION
            {
                public static XmlElement GLOBAL_IDENTIFIER = new XmlElement("GLOBAL_IDENTIFIER", XIMType.UID);
                public static XmlElement LOCAL_IDENTIFIER = new XmlElement("LOCAL_IDENTIFIER", XIMType.ID);
            }

            public static XmlElement AE_TITLE = new XmlElement("AE_TITLE", XIMType.STR);
            public static XmlElement STATION_NAME = new XmlElement("STATION_NAME", XIMType.STR);
            public static XmlElement LOCATION = new XmlElement("LOCATION", XIMType.LOC);
            public static XmlElement SCHEDULED_START_DATE = new XmlElement("SCHEDULED_START_DATE", XIMType.DAT);
            public static XmlElement SCHEDULED_START_TIME = new XmlElement("SCHEDULED_START_TIME", XIMType.TIM);
            public static XmlElement SCHEDULED_END_DATE = new XmlElement("SCHEDULED_END_DATE", XIMType.DAT);
            public static XmlElement SCHEDULED_END_TIME = new XmlElement("SCHEDULED_END_TIME", XIMType.TIM);
            public static XmlElement SCHEDULED_PERFORMING_PHYSICIAN = new XmlElement("SCHEDULED_PERFORMING_PHYSICIAN[]", XIMType.PHY);
            
            public static XmlElement PROTOCOL = new XmlElement("PROTOCOL[]", typeof(SCHEDULED_PROCEDURE_STEP__PROTOCOL));
            public class SCHEDULED_PROCEDURE_STEP__PROTOCOL
            {
                public static XmlElement DESCRIPTOR = new XmlElement("DESCRIPTOR", XIMType.COD);
                public static XmlElement CONTEXT = new XmlElement("CONTEXT[]", XIMType.CON);
            }
            
            public static XmlElement LOCAL_DESCRIPTION = new XmlElement("LOCAL_DESCRIPTION", XIMType.COD);
            public static XmlElement STATUS = new XmlElement("STATUS", XIMType.COD);
            public static XmlElement COMMENTS = new XmlElement("COMMENTS", XIMType.STR);
            public static XmlElement MODALITY = new XmlElement("MODALITY", XIMType.COD);
            public static XmlElement REQUESTED_CONTRAST_AGENT = new XmlElement("REQUESTED_CONTRAST_AGENT", XIMType.STR);
            public static XmlElement PREMEDICATION = new XmlElement("PREMEDICATION", XIMType.STR);

            public static XmlElement HUMAN_PERFORMER = new XmlElement("HUMAN_PERFORMER[]", typeof(SCHEDULED_PROCEDURE_STEP__HUMAN_PERFORMER));
            public class SCHEDULED_PROCEDURE_STEP__HUMAN_PERFORMER
            {
                public static XmlElement NAME = new XmlElement("NAME", XIMType.PNM);
                public static XmlElement IDENTIFIER = new XmlElement("IDENTIFIER", XIMType.COD);
                public static XmlElement ORGANIZATION_NAME = new XmlElement("ORGANIZATION_NAME", XIMType.STR);
            }

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class PERFORMED_PROCEDURE_STEP
        {
            public static XmlElement IDENTIFICATION = new XmlElement("IDENTIFICATION", typeof(PERFORMED_PROCEDURE_STEP__IDENTIFICATION));
            public class PERFORMED_PROCEDURE_STEP__IDENTIFICATION
            {
                public static XmlElement GLOBAL_IDENTIFIER = new XmlElement("GLOBAL_IDENTIFIER", XIMType.UID);
                public static XmlElement LOCAL_IDENTIFIER = new XmlElement("LOCAL_IDENTIFIER", XIMType.ID);
            }

            public static XmlElement STATION = new XmlElement("STATION", typeof(PERFORMED_PROCEDURE_STEP__STATION));
            public class PERFORMED_PROCEDURE_STEP__STATION
            {
                public static XmlElement AE_TITLE = new XmlElement("AE_TITLE", XIMType.STR);
                public static XmlElement NAME = new XmlElement("NAME", XIMType.STR);
                public static XmlElement IDENTIFIER = new XmlElement("IDENTIFIER", XIMType.COD);
                public static XmlElement CLASS = new XmlElement("CLASS", XIMType.COD);
                public static XmlElement GEOGRAPHIC_LOCATION = new XmlElement("GEOGRAPHIC_LOCATION", XIMType.COD);
                public static XmlElement PROCESSING_APPLICATION = new XmlElement("PROCESSING_APPLICATION", XIMType.COD);
            }

            public static XmlElement HUMAN_PERFORMER = new XmlElement("HUMAN_PERFORMER[]", typeof(PERFORMED_PROCEDURE_STEP__HUMAN_PERFORMER));
            public class PERFORMED_PROCEDURE_STEP__HUMAN_PERFORMER
            {
                public static XmlElement NAME = new XmlElement("NAME", XIMType.PNM);
                public static XmlElement IDENTIFIER = new XmlElement("IDENTIFIER", XIMType.COD);
                public static XmlElement ORGANIZATION_NAME = new XmlElement("ORGANIZATION_NAME", XIMType.STR);
            }

            public static XmlElement LOCATION = new XmlElement("LOCATION", XIMType.LOC);
            public static XmlElement START_DATE = new XmlElement("START_DATE", XIMType.DAT);
            public static XmlElement START_TIME = new XmlElement("START_TIME", XIMType.TIM);
            public static XmlElement END_DATE = new XmlElement("END_DATE", XIMType.DAT);
            public static XmlElement END_TIME = new XmlElement("END_TIME", XIMType.TIM);
            public static XmlElement STATUS = new XmlElement("STATUS", XIMType.COD);
            public static XmlElement PERFORMED_PROCEDURE = new XmlElement("PERFORMED_PROCEDURE[]", XIMType.COD);
            public static XmlElement LOCAL_DESCRIPTION = new XmlElement("LOCAL_DESCRIPTION", XIMType.COD);
            public static XmlElement COMMENTS = new XmlElement("COMMENTS", XIMType.STR);
            public static XmlElement PROCEDURE_TYPE = new XmlElement("PROCEDURE_TYPE", XIMType.COD);
            public static XmlElement DISCONTINUATION_REASON = new XmlElement("DISCONTINUATION_REASON[]", XIMType.COD);

            public static XmlElement PROTOCOL = new XmlElement("PROTOCOL[]", typeof(SPERFORMED_PROCEDURE_STEP__PROTOCOL));
            public class SPERFORMED_PROCEDURE_STEP__PROTOCOL
            {
                public static XmlElement DESCRIPTOR = new XmlElement("DESCRIPTOR", XIMType.COD);
                public static XmlElement CONTEXT = new XmlElement("CONTEXT[]", XIMType.CON);
            }

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class OBSERVATION
        {
            public static XmlElement REQUESTED_DATE_TIME = new XmlElement("REQUESTED_DATE_TIME", XIMType.DTM);
            public static XmlElement PRIORITY = new XmlElement("PRIORITY", XIMType.ID);
            public static XmlElement REQUEST_DATE_TIME = new XmlElement("REQUEST_DATE_TIME", XIMType.DTM);
            public static XmlElement SPECIMEN_RECEIVED_DATE_TIME = new XmlElement("SPECIMEN_RECEIVED_DATE_TIME", XIMType.DTM);
            
            public static XmlElement SPECIMEN_SOURCE = new XmlElement("SPECIMEN_SOURCE", typeof(OBSERVATION__SPECIMEN_SOURCE));
            public class OBSERVATION__SPECIMEN_SOURCE
            {
                public static XmlElement NAME = new XmlElement("NAME", XIMType.COD);
                public static XmlElement ADDITIVES = new XmlElement("ADDITIVES", XIMType.COD);
                public static XmlElement COLLECTION_METHOD = new XmlElement("COLLECTION_METHOD", XIMType.STR);
                public static XmlElement BODY_SITE = new XmlElement("BODY_SITE", XIMType.COD);
                public static XmlElement SITE_MODIFIER = new XmlElement("SITE_MODIFIER", XIMType.COD);
                public static XmlElement COLLECTION_METHOD_MODIFIER_ROLE = new XmlElement("COLLECTION_METHOD_MODIFIER_ROLE", XIMType.COD);
                public static XmlElement SPECIMEN_ROLE = new XmlElement("SPECIMEN_ROLE", XIMType.COD);
            }
            
            public static XmlElement SCHEDULED_DATE_TIME = new XmlElement("SCHEDULED_DATE_TIME", XIMType.DTM);
            public static XmlElement COMMENTS = new XmlElement("COMMENTS[]", XIMType.COD);
            public static XmlElement TRANSPORT_ARRANGED = new XmlElement("TRANSPORT_ARRANGED", XIMType.ID);
            public static XmlElement VALUE_TYPE = new XmlElement("VALUE_TYPE", XIMType.ID);
            public static XmlElement ID = new XmlElement("ID", XIMType.COD);
            public static XmlElement SUB_ID = new XmlElement("SUB_ID", XIMType.STR);
            public static XmlElement OBSERVATION_RESULTS_STATUS = new XmlElement("OBSERVATION_RESULTS_STATUS", XIMType.ID);

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class RESULTS
        {
            public static XmlElement IDENTIFICATION = new XmlElement("IDENTIFICATION", typeof(RESULTS__IDENTIFICATION));
            public class RESULTS__IDENTIFICATION
            {
                public static XmlElement GLOBAL_IDENTIFIER = new XmlElement("GLOBAL_IDENTIFIER", XIMType.UID);
                public static XmlElement LOCAL_IDENTIFIER = new XmlElement("LOCAL_IDENTIFIER", XIMType.ID);
            }

            public static XmlElement REQUESTED_SUBSEQUENT_WORKITEM = new XmlElement("REQUESTED_SUBSEQUENT_WORKITEM[]", XIMType.COD);
            public static XmlElement NON_DICOM_OUTPUT = new XmlElement("NON_DICOM_OUTPUT[]", XIMType.COD);
            public static XmlElement SUMMARY_IMPRESSIONS = new XmlElement("SUMMARY_IMPRESSIONS", XIMType.STR);
            public static XmlElement COMMENTS = new XmlElement("COMMENTS", XIMType.STR);
            
            public static XmlElement OUTPUT_INFORMATION = new XmlElement("OUTPUT_INFORMATION", typeof(RESULTS__OUTPUT_INFORMATION));
            public class RESULTS__OUTPUT_INFORMATION
            {
                public static XmlElement STUDY = new XmlElement("STUDY[]", typeof(RESULTS__OUTPUT_INFORMATION__STUDY));
                public class RESULTS__OUTPUT_INFORMATION__STUDY
                {
                    public static XmlElement IDENTIFICATION = new XmlElement("IDENTIFICATION", typeof(RESULTS__OUTPUT_INFORMATION__STUDY__IDENTIFICATION));
                    public class RESULTS__OUTPUT_INFORMATION__STUDY__IDENTIFICATION
                    {
                        public static XmlElement STUDY_INSTANCE_UID = new XmlElement("STUDY_INSTANCE_UID", XIMType.STR);
                    }
                }

                public static XmlElement SERIES = new XmlElement("SERIES[]", typeof(RESULTS__OUTPUT_INFORMATION__SERIES));
                public class RESULTS__OUTPUT_INFORMATION__SERIES
                {
                    public static XmlElement IDENTIFICATION = new XmlElement("IDENTIFICATION", typeof(RESULTS__OUTPUT_INFORMATION__SERIES__IDENTIFICATION));
                    public class RESULTS__OUTPUT_INFORMATION__SERIES__IDENTIFICATION
                    {
                        public static XmlElement GLOBAL_IDENTIFIER = new XmlElement("GLOBAL_IDENTIFIER", XIMType.UID);
                    }

                    public static XmlElement STORAGE = new XmlElement("STORAGE", typeof(RESULTS__OUTPUT_INFORMATION__STORAGE));
                    public class RESULTS__OUTPUT_INFORMATION__STORAGE
                    {
                        public static XmlElement RETRIEVE_AE_TITLE = new XmlElement("RETRIEVE_AE_TITLE", XIMType.STR);
                        public static XmlElement STORAGE_MEDIA_FILESET_LOCAL_ID = new XmlElement("STORAGE_MEDIA_FILESET_LOCAL_ID", XIMType.STR);
                        public static XmlElement STORAGE_MEDIA_FILESET_GLOBAL_ID = new XmlElement("STORAGE_MEDIA_FILESET_GLOBAL_ID", XIMType.STR);
                        public static XmlElement INSTANCE = new XmlElement("INSTANCE[]", XIMType.UID);
                    }
                }
            }

            public static XmlElement STATUS_CHANGE_DATE_TIME = new XmlElement("STATUS_CHANGE_DATE_TIME", XIMType.DTM);
            public static XmlElement STATUS = new XmlElement("STATUS", XIMType.ID);

            public static XmlElement PRINCIPLE_RESULTS_INTERPRETER = new XmlElement("PRINCIPLE_RESULTS_INTERPRETER", typeof(RESULTS__PRINCIPLE_RESULTS_INTERPRETER));
            public class RESULTS__PRINCIPLE_RESULTS_INTERPRETER
            {
                public static XmlElement NAME = new XmlElement("NAME", XIMType.PNM);
                public static XmlElement START_DATE_TIME = new XmlElement("START_DATE_TIME", XIMType.DTM);
                public static XmlElement END_DATE_TIME = new XmlElement("END_DATE_TIME", XIMType.DTM);
                public static XmlElement LOCATION_INFORMATION = new XmlElement("LOCATION_INFORMATION", XIMType.LOC);
            }

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class INTERPRETATION
        {
            public static XmlElement TRANSCRIPTION_DATE = new XmlElement("TRANSCRIPTION_DATE", XIMType.DAT);
            public static XmlElement TRANSCRIPTION_TIME = new XmlElement("TRANSCRIPTION_TIME", XIMType.TIM);

            public static XmlElement TRANSCRIBER = new XmlElement("TRANSCRIBER", typeof(INTERPRETATION__TRANSCRIBER));
            public class INTERPRETATION__TRANSCRIBER
            {
                public static XmlElement NAME = new XmlElement("NAME", XIMType.PNM);
            }

            public static XmlElement AUTHOR = new XmlElement("AUTHOR", typeof(INTERPRETATION__AUTHOR));
            public class INTERPRETATION__AUTHOR
            {
                public static XmlElement NAME = new XmlElement("NAME", XIMType.PNM);
            }

            public static XmlElement CONTENTS = new XmlElement("CONTENTS", typeof(INTERPRETATION__CONTENTS));
            public class INTERPRETATION__CONTENTS
            {
                public static XmlElement TEXT = new XmlElement("TEXT", XIMType.STR);
            }

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class ROLE
        {
            public static XmlElement INSTANCE_ID = new XmlElement("INSTANCE_ID", XIMType.HD);
            public static XmlElement ACTION_CODE = new XmlElement("ACTION_CODE", XIMType.COD);
            public static XmlElement ROLE_FUNCTION = new XmlElement("ROLE_FUNCTION", XIMType.COD);
            public static XmlElement ROLE_PERSON = new XmlElement("ROLE_PERSON[]", XIMType.PHY);

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }

        public class NOTES
        {
            public static XmlElement SOURCE_COMMENT = new XmlElement("SOURCE_COMMENT", XIMType.COD);
            public static XmlElement COMMENT = new XmlElement("COMMENT[]", XIMType.STR);
            public static XmlElement COMMENT_TYPE = new XmlElement("COMMENT_TYPE", XIMType.COD);

            public static XmlElement CUSTOMER_1 = new XmlElement("CUSTOMER_1");
            public static XmlElement CUSTOMER_2 = new XmlElement("CUSTOMER_2");
            public static XmlElement CUSTOMER_3 = new XmlElement("CUSTOMER_3");
            public static XmlElement CUSTOMER_4 = new XmlElement("CUSTOMER_4");
        }
    }
}
