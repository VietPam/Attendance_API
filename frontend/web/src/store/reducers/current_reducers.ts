import { createReducer, createAction } from "@reduxjs/toolkit";
import { ICurrentState } from "../../types/interface";
// State

const initialState: ICurrentState = {
  dataPage: "",
};

// Actions
export const login = createAction<ICurrentState>("CHANGE");
export const logout = createAction("UNCHANGE");
// export const gglogin = createAction<IAuthState>("GGLOGIN");

// Reducer
const currentReducer = createReducer(initialState, (builder) => {
  builder
    .addCase(login, (state, action) => {
      state.dataPage = action.payload.dataPage;
    })
    .addCase(logout, (state) => {
      state.dataPage = "";
    });
  // .addCase(gglogin, (state, action) => {
  //   state.currentUser = action.payload.currentUser;
  //   state.name = action.payload.name;
  //   state.avatar = action.payload.avatar;
  // });
});

export default currentReducer;
