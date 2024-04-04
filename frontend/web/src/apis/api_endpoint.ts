//Login
export const LOGIN = "/Employee/login";
export const getLoginBody = (email: string, password: string) => ({
  email: email,
  password: password,
});

//Register
export const REGISTER = "/user/register";

//Import user by excel
export const IMPORT_USER = "/user/importToExcel";

//Get Department
export const GET_DEPARTMENT = "/Department/getAll";

//Create Department
export const CREATE_DEPARTMENT = "/Department/createNew";
export const createDepartmentBody = (name: string, code: string) => ({
  name: name,
  code: code,
});

//Update Department
export const UPDATE_DEPARTMENT = `/Department/updateOne`;
export const updateDepartmentBody = (name: string, code: string) => ({
  name: name,
  code: code,
});

//Delete Department
export const DELETE_DEPARTMENT = `/Department/deleteOne`;

//Get Position by department code
export const GET_POSITION_BY_DEPARTMENT_ID = "/Position/getByDepartmentCode";
export const getPositionByDepartmentIdBody = (departmentCode: string) => ({
  departmentCode: departmentCode,
});

//New Position
export const NEW_POSITION = "/Position/createNew";
export const newPositionBody = (title: string, code: string, coef: number) => ({
  title: title,
  code: code,
  salary_coeffcient: coef,
});

//Update Position
export const UPDATE_POSITION = "/Position/updateOne";

//Delete Position
export const DELETE_POSITION = "/Position/remove-position";

//import to excel
export const IMPORT_TO_EXCEL = "/user/importToExcel";
export const importToExcelBody = (file: File, departmentId: string) => {
  const formData = new FormData();
  formData.append("import", file);
  formData.append("departmentID", departmentId);
  return formData;
};

//get Employee by department code
export const GET_EMPLOYEE_BY_DEPARTMENT_CODE = "/Employee/getByDepartmentCode";

//get Employee by position id
export const GET_EMPLOYEE_BY_POSITION_ID = "/Employee/getByPositionID";

//get Employee role
export const GET_EMPLOYEE_BY_ROLE = "/Employee/getRole";

//create new employee
export const CREATE_NEW_EMPLOYEE = "/Employee/createNew";
export const createNewEmployeeBody = (
  email: string,
  fullName: string,
  phoneNumber: string,
  avatar: string,
  birth_day: string,
  gender: string,
  cmnd: string,
  address: string
) => ({
  email: email,
  fullName: fullName,
  phoneNumber: phoneNumber,
  avatar: avatar,
  birth_day: birth_day,
  gender: gender,
  cmnd: cmnd,
  address: address,
});

//reset password
export const RESET_PASSWORD = "/Mail/reset_password";

// setting
export const GET_SETTING = "/Setting/get";

// update setting
export const UPDATE_SETTING = "/Setting/updateOne";
export const updateSettingBody = (
  company_name: string,
  company_code: string,
  start_time_hour: number,
  start_time_minute: number,
  salary_per_coef: number,
  payment_date: number
) => ({
  company_name: company_name,
  company_code: company_code,
  start_time_hour: start_time_hour,
  start_time_minute: start_time_minute,
  salary_per_coef: salary_per_coef,
  payment_date: payment_date,
});

//dashboard
export const GET_DASHBOARD = "/DashBoard";

//get attendance list
export const GET_ATTENDANCE_LIST = "/Attendance/getList";

//get payroll department
export const GET_PAYROLL_DEPARTMENT = "/Payroll/getPayrollDepartment";
