using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Drawing;

namespace Hys.CareRIS.Application.Mappers
{
    public class ReportMapper : Profile
    {
        public ReportMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<Report, ReportDto>()
                .ForMember(dto => dto.WYS, opt =>
                    opt.MapFrom(src => GetStringFromBytes(src.WYS)))
                .ForMember(dto => dto.WYG, opt =>
                    opt.MapFrom(src => GetStringFromBytes(src.WYG)))
                .ForMember(dto => dto.IsDiagnosisRight, opt =>
                    opt.MapFrom(src => src.IsDiagnosisRight.HasValue ? (bool?)(src.IsDiagnosisRight.Value == 1) : null))
                .ForMember(dto => dto.IsPrint, opt =>
                    opt.MapFrom(src => src.IsPrint.HasValue ? (bool?)(src.IsPrint.Value == 1) : null))
                .ForMember(dto => dto.IsLeaveWord, opt =>
                    opt.MapFrom(src => src.IsLeaveWord.HasValue ? (bool?)(src.IsLeaveWord.Value == 1) : null))
                .ForMember(dto => dto.IsDraw, opt =>
                    opt.MapFrom(src => src.IsDraw.HasValue ? (bool?)(src.IsDraw.Value == 1) : null))
                .ForMember(dto => dto.IsLeaveSound, opt =>
                    opt.MapFrom(src => src.IsLeaveSound.HasValue ? (bool?)(src.IsLeaveSound.Value == 1) : null))
                .ForMember(dto => dto.RebuildMark, opt =>
                    opt.MapFrom(src => src.RebuildMark.HasValue ? (bool?)(src.RebuildMark.Value == 1) : null))
                .ForMember(dto => dto.Uploaded, opt =>
                    opt.MapFrom(src => src.Uploaded.HasValue ? (bool?)(src.Uploaded.Value == 1) : null))
                .ForMember(dto => dto.IsModified, opt =>
                    opt.MapFrom(src => src.IsModified.HasValue ? (bool?)(src.IsModified.Value == 1) : null));

            CreateMap<ReportDto, Report>()
               .ForMember(src => src.WYS, opt =>
                    opt.MapFrom(dto => GetBytes(GetRTF(dto.WYS))))
               .ForMember(src => src.WYG, opt =>
                    opt.MapFrom(dto => GetBytes(GetRTF(dto.WYG))))
               .ForMember(src => src.WYSText, opt =>
                    opt.MapFrom(dto => dto.WYS))
               .ForMember(src => src.WYGText, opt =>
                    opt.MapFrom(dto => dto.WYG))
               .ForMember(src => src.ReportText, opt =>
                    opt.MapFrom(dto => dto.WYS + dto.WYG))
               .ForMember(src => src.IsDiagnosisRight, opt =>
                    opt.MapFrom(dto => dto.IsDiagnosisRight.HasValue ? (int?)(dto.IsDiagnosisRight.Value ? 1 : 0) : null))
               .ForMember(src => src.IsPrint, opt =>
                    opt.MapFrom(dto => dto.IsPrint.HasValue ? (int?)(dto.IsPrint.Value ? 1 : 0) : null))
               .ForMember(src => src.IsLeaveWord, opt =>
                    opt.MapFrom(dto => dto.IsLeaveWord.HasValue ? (int?)(dto.IsLeaveWord.Value ? 1 : 0) : null))
               .ForMember(src => src.IsDraw, opt =>
                    opt.MapFrom(dto => dto.IsDraw.HasValue ? (int?)(dto.IsDraw.Value ? 1 : 0) : null))
               .ForMember(src => src.IsLeaveSound, opt =>
                    opt.MapFrom(dto => dto.IsLeaveSound.HasValue ? (int?)(dto.IsLeaveSound.Value ? 1 : 0) : null))
               .ForMember(src => src.RebuildMark, opt =>
                    opt.MapFrom(dto => dto.RebuildMark.HasValue ? (int?)(dto.RebuildMark.Value ? 1 : 0) : null))
               .ForMember(src => src.Uploaded, opt =>
                    opt.MapFrom(dto => dto.Uploaded.HasValue ? (int?)(dto.Uploaded.Value ? 1 : 0) : null))
               .ForMember(src => src.IsModified, opt =>
                    opt.MapFrom(dto => dto.IsModified.HasValue ? (int?)(dto.IsModified.Value ? 1 : 0) : null));
        }

        public static string GetStringFromBytes(object buff)
        {
            if (buff != null && !(buff is DBNull))
            {
                return System.Text.Encoding.Default.GetString(buff as byte[]);
            }

            return "";
        }

        public static byte[] GetBytes(object value)
        {
            if (value != null && !(value is DBNull))
            {
                return System.Text.Encoding.Default.GetBytes(value as string);
            }

            return new byte[] { 0 };
        }

        public static string GetRTF(string text)
        {
            string ret = text;

            if (!string.IsNullOrEmpty(text) && !text.StartsWith(@"{\rtf"))
            {
                using (System.Windows.Forms.RichTextBox richText = new System.Windows.Forms.RichTextBox())
                {

                    //string fontFamily = ProfileManager.Instance.GetProfileValue("ReportEditor_Font_FamilyName");
                    //string fontSize = ProfileManager.Instance.GetProfileValue("ReportEditor_Font_Size");
                    string fontFamily = "Microsoft Sans Serif";
                    string fontSize = "14";
                    if (!string.IsNullOrWhiteSpace(fontFamily) && !string.IsNullOrWhiteSpace(fontSize))
                    {
                        float fsize;
                        if (float.TryParse(fontSize, out fsize))
                        {
                            Font fnt = new Font(fontFamily, fsize);
                            richText.Font = fnt;
                        }
                    }
                    richText.Text = text;
                    ret = richText.Rtf;
                }
            }

            return ret;
        }

        public static string GetStrFromRTF(string text)
        {
            string ret = text;
            if (text.StartsWith(@"{\rtf"))
            {
                using (System.Windows.Forms.RichTextBox richText = new System.Windows.Forms.RichTextBox())
                {
                    richText.Rtf = text;
                    ret = richText.Text;
                }
            }

            return ret;
        }
    }
}
