import { baseURL, mainApi } from "./main_api";
import * as apiEndpoints from "./api_endpoint";
import axios from "axios";
// import axios from "axios";
// import { baseURL } from "./main_api";

//Login
export const Login = (email: string, password: string) => {
  return mainApi.post(
    apiEndpoints.LOGIN,
    apiEndpoints.getLoginBody(email, password)
  );
};

//Get Department
export const GetDepartmentAll = (page: number, per_page: number) => {
  return mainApi.get(apiEndpoints.GET_DEPARTMENT, {
    params: { page: page, per_page: per_page },
  });
};

export const GetDepartment = (page: number, per_page: number) => {
  return axios.get(
    `${baseURL}/Department/getAll?page=${page}&per_page=${per_page}`
  );
};

//Create Department
export const CreateDepartment = (name: string, code: string) => {
  return mainApi.post(
    apiEndpoints.CREATE_DEPARTMENT,
    apiEndpoints.createDepartmentBody(name, code)
  );
};

//Update Department
export const UpdateDepartment = (id: string, name: string, code: string) => {
  return mainApi.put(
    apiEndpoints.UPDATE_DEPARTMENT,
    apiEndpoints.updateDepartmentBody(name, code),
    { params: { id: id } }
  );
};

//Delete Department
export const DeleteDepartment = (code: string) => {
  return mainApi.delete(apiEndpoints.DELETE_DEPARTMENT, {
    params: { code: code },
  });
};

//Get Position by department id
export const GetPositionByDepartmentCode = (
  departmentCode: string,
  page: number,
  per_page: number
) => {
  return mainApi.get(apiEndpoints.GET_POSITION_BY_DEPARTMENT_ID, {
    params: { departmentCode: departmentCode, page: page, per_page: per_page },
  });
  // return axios.get(baseURL, {
  //   params: { departmentCode: departmentCode },
  // });
};

//New Position
export const NewPosition = (
  title: string,
  code: string,
  coef: number,
  dpCode: string
) => {
  return mainApi.post(
    apiEndpoints.NEW_POSITION,
    apiEndpoints.newPositionBody(title, code, coef),
    {
      params: { departmentCode: dpCode },
    }
  );
};

//Update Position
export const UpdatePosition = (
  id: string,
  title: string,
  code: string,
  coef: number
) => {
  return mainApi.put(
    apiEndpoints.UPDATE_POSITION,
    apiEndpoints.newPositionBody(title, code, coef),
    { params: { id: id } }
  );
};

//Delete Position
export const DeletePosition = (id: string) => {
  return mainApi.delete(apiEndpoints.DELETE_POSITION, {
    params: { userId: id },
  });
};

//import to excel
export const ImportToExcel = (file: File, departmentId: string) => {
  return mainApi.post(
    apiEndpoints.IMPORT_TO_EXCEL,
    apiEndpoints.importToExcelBody(file, departmentId)
  );
};

//get Employee by department code
export const GetEmployeeByDepartmentCode = (departmentCode: string) => {
  return mainApi.get(apiEndpoints.GET_EMPLOYEE_BY_DEPARTMENT_CODE, {
    params: { departmentCode: departmentCode },
  });
};

//get Employee by position id
export const GetEmployeeByPositionId = (
  positionId: string,
  page: number,
  per_page: number
) => {
  return mainApi.get(apiEndpoints.GET_EMPLOYEE_BY_POSITION_ID, {
    params: { positionId: positionId, page: page, per_page: per_page },
  });
};

//create new employee
// eslint-disable-next-line @typescript-eslint/no-explicit-any
export const CreateNewEmployee = (position_id: string, data: any) => {
  return mainApi.post(apiEndpoints.CREATE_NEW_EMPLOYEE, data, {
    params: { position_id: position_id },
  });
};

// export const ResetPassword = (emp_id: number) => {
//   return mainApi.post(apiEndpoints.RESET_PASSWORD, {
//     params: { emp_id: emp_id },
//   });
// };

export const ResetPassword = (emp_id: number) => {
  return axios.post(`${baseURL}/Mail/reset_password?emp_id=${emp_id}`);
};

//get setting
export const GetSetting = () => {
  return mainApi.get(apiEndpoints.GET_SETTING);
};

//update setting
export const UpdateSetting = (data: any) => {
  return mainApi.put(apiEndpoints.UPDATE_SETTING, data);
};

//dashboard
export const GetDashboard = () => {
  return mainApi.get(apiEndpoints.GET_DASHBOARD);
};

//get attendance list
export const GetAttendanceList = (date: string, department_code: string) => {
  return mainApi.get(apiEndpoints.GET_ATTENDANCE_LIST, {
    params: { date: date, department_code: department_code },
  });
};

//get payroll department
export const GetPayrollDepartment = (
  department_code: string,
  start: string,
  end: string
) => {
  return mainApi.get(apiEndpoints.GET_PAYROLL_DEPARTMENT, {
    params: { department_code: department_code, start: start, end: end },
  });
};
