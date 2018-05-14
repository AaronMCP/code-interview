using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class DictionaryValueDto
    {
        public string UniqueID { get; set; }
        public int Tag { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool? IsDefault { get; set; }
        public string ShortcutCode { get; set; }
        public int? OrderID { get; set; }
        public int? MapTag { get; set; }
        public string MapValue { get; set; }
        public string Site { get; set; }
        public string Domain { get; set; }

        //修改时 用作排序
        public List<DictionaryValueDto> Values { get; set; }


    }
}
