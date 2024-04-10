import { createAction, createReducer } from "@reduxjs/toolkit";
import { Data } from "../../pages/Dashboard";

// State
interface IDashboardState {
    data: Data;
  }
  
  const initialState: IDashboardState = {
    data: {
      employees_Today: {
        thu: 0,
        ngay: 0,
        attendance: 0,
        late: 0,
        absent: 0,
      },
      total_Employee: {
        man: 0,
        woman: 0,
        total: 0,
      },
      attendance_ByWeek: [],
      emp_perDepts: [],
    },
  };
  
  // Actions
  export const addDashboard = createAction<IDashboardState>("ADD_DASHBOARD");
  export const removeDashboard = createAction("REMOVE_DASHBOARD");
  
  // Reducer
  const dashboardReducer = createReducer(initialState, (builder) => {
    builder
      .addCase(addDashboard, (state, action) => {
        state.data = action.payload.data;
      })
      .addCase(removeDashboard, (state) => {
        // eslint-disable-next-line @typescript-eslint/no-unused-vars
        state.data = {
          employees_Today: {
            thu: 0,
            ngay: 0,
            attendance: 0,
            late: 0,
            absent: 0,
          },
          total_Employee: {
            man: 0,
            woman: 0,
            total: 0,
          },
          attendance_ByWeek: [],
          emp_perDepts: [],
        };
      });
  });
  
  export default dashboardReducer;
  