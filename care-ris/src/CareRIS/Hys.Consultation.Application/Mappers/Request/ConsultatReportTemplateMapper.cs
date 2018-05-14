namespace Hys.Consultation.Application.Mappers
{
    using AutoMapper;
    using Hys.Consultation.Application.Dtos;
    using Hys.Consultation.Domain.Entities;
    using Hys.CareRIS.Domain.Entities;
    using System;
    using System.Drawing;

    public class ConsultatRpeortTemplateMapper : Profile
    {
        public ConsultatRpeortTemplateMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ConsultatReportTemplate, ConsultatReportTemplateDto>()
                 .ForMember(dto => dto.IsPositive, opt =>
                    opt.MapFrom(src => src.IsPositive.HasValue ? (bool?)(src.IsPositive.Value == 1) : null))
                    .ForMember(dto => dto.WYSText, opt =>
                    opt.MapFrom(src => GetStrFromRTF(GetStringFromBytes(src.WYS))))
                    .ForMember(dto => dto.WYGText, opt =>
                    opt.MapFrom(src => GetStrFromRTF(GetStringFromBytes(src.WYG))));

            CreateMap<ConsultatReportTemplateDto, ConsultatReportTemplate>()
                 .ForMember(src => src.IsPositive, opt =>
                    opt.MapFrom(dto => dto.IsPositive.HasValue ? (int?)(dto.IsPositive.Value ? 1 : 0) : null))
                    .ForMember(src => src.WYS, opt =>
                    opt.MapFrom(dto => GetBytes(dto.WYSText)))
               .ForMember(src => src.WYG, opt =>
                    opt.MapFrom(dto => GetBytes(dto.WYGText)));
        }

        private string GetStringFromBytes(object buff)
        {
            try
            {
                if (buff != null && !(buff is DBNull))
                    return System.Text.Encoding.Default.GetString(buff as byte[]);
            }
            catch (Exception)
            {
            }

            return "";
        }

        private byte[] GetBytes(object value)
        {
            try
            {
                if (value != null && !(value is DBNull))
                    return System.Text.Encoding.Default.GetBytes(value as string);
            }
            catch (Exception)
            {
            }

            return new byte[] { 0 };
        }

        private string GetRTF(string text)
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

        private string GetStrFromRTF(string text)
        {
            string ret = text;
            if (text.StartsWith(@"{\rtf"))
                using (System.Windows.Forms.RichTextBox richText = new System.Windows.Forms.RichTextBox())
                {
                    richText.Rtf = text;
                    ret = richText.Text;
                }

            return ret;
        }
    }
}
