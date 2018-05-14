export interface IPatient {

  /**
   * 主键
   */
  id?: number;

  /**
   * empiId
   */
  empiId: string;

  /**
   * empi号码
   */
  empiNumber: string;

  /**
   * 姓名
   */
  name: string;

  /**
   * 性别
   */
  sex: string;

  /**
   * 生日
   */
  birthday: Date;

  /**
   * 证件类型
   */
  credentialType: string;

  /**
   * 证件号码
   */
  credentialNumber: string;

  /**
   * 出生省市
   */
  bornProvince: string;

  /**
   * 出生县市
   */
  bornContry: string;

  /**
   * 医保号码
   */
  medicalInsuranceNo: string;

  // 社保号码
  socialSecurityNo: string;

  /**
   * 婚姻状况
   */
  maritalStatus: string;

  /**
   * 国籍
   */
  nationality: string;

  /**
   * 电话
   */
  phone: string;

  /**
   * 地址
   */
  address: string;

  /**
   * 更新日期
   */
  updatedAt?: string;

  /**
   * 输入框变化函数
   */
  inputChange?: any;

  /**
   * 弹出框数据
   */
  tableListData?: any;
}
