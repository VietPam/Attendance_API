import { createReducer, createAction } from "@reduxjs/toolkit";
import { DepartmentInforType } from "../../components/employee/department/Department";
// State

// interface IDepartmentState {
//   listDepartment: DepartmentInforType;
// }

const initialState: DepartmentInforType = {
  list_dep: [],
  current_page: 0,
  perpage: 0,
  pages: 0,
};

// Actions
export const addDepartments =
  createAction<DepartmentInforType>("ADD_DEPARTMENTS");
export const removeDepartments = createAction("REMOVE_DEPARTMENTS");

// Reducer
const departmentReducer = createReducer(initialState, (builder) => {
  builder
    .addCase(addDepartments, (state, action) => {
      state.list_dep = action.payload.list_dep;
      state.current_page = action.payload.current_page;
      state.perpage = action.payload.perpage;
      state.pages = action.payload.pages;
    })
    .addCase(removeDepartments, (state) => {
      state.list_dep = [];
      state.current_page = 0;
      state.perpage = 0;
      state.pages = 0;
    });
});

export default departmentReducer;
