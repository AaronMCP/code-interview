using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class DictionaryMapDto
    {
        public string UniqueID { get; set; }
        public int Tag { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public int? MapTag { get; set; }
        public string MapValue { get; set; }

        //关联关系
        //字典名
        public string Name { get; set; }
        //map字典名
        public string MapName { get; set; }
        //map字典值
        public IEnumerable<DictionaryValueDto> MapDicValues { get; set; }


        //public bool? IsDefault { get; set; }
        //public string ShortcutCode { get; set; }
        //public int? OrderID { get; set; }
        //public string Site { get; set; }
        //public string Domain { get; set; }
    }
}
