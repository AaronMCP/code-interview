export interface IPatientInfo {
  /**
   * 出生省份
   */
  bornProvince: string;

  /**
   * 出生县市
   */
  bornContry: string;

  /**
   * 社保卡号
   */
  socialSecurityNo: string;

  /**
   * 证件类型
   */
  credentialType: string;

  /**
   * 证件号码
   */
  credentialNumber: string;

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
}
