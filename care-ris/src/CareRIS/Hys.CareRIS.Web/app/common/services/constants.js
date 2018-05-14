// define the constant values
commonModule.constant('constants', {
    guid: {
        empty: '00000000-0000-0000-0000-000000000000'
    },
    tabCeiling: 10,
    pageSize: 20,
    expertRoleId: '4d1fa440-28b7-b5d2-976f-9db7f84b5d9d',
    doctorRoleId: '4dc3dbb2-27e0-9eb1-a106-003cae158b16',
    consAdminRoleId: '93321a30-891f-6c86-6b4d-46f17f13dfae',
    adminRoleID: '2ee2fd0c-100d-b934-d0c2-f24ff16039e9',
    siteAdminRoleID: 'd6e52828-6c4f-2efe-c2f0-700de9375e75',
    adminUserID: 'ea436d1d-d494-44f2-9c9c-0c1d5542ede8',
    gcRisUserID: 'cae0fcf9-ea87-4989-a76b-efd95aec2516',
    dateTypes: [
        { id: 1, text: '工作日' },
        { id: 2, text: '周末' },
        { id: 3, text: '节假日' },
        { id: 4, text: '检修日' }
    ],
    risRole: {
        admin: 'Administrator',
        senior: 'HLRadiologist',
        intermediate: 'MLRadiologist',
        junior: 'LLRadiologist',
        technician: 'Technician',
        registrar: 'Registrar',
        nurse: 'Nurse',
        clinician: 'Clinician',
        siteAdmin: 'SiteAdmin',
        globalAdmin: 'GlobalAdmin'
    }
});