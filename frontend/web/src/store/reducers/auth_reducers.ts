import { createReducer, createAction } from "@reduxjs/toolkit";
import { IAuthState } from "../../types/interface";
// State

const initialState: IAuthState = {
  _id: "",
  email: "",
  password: "",
  address: "",
  age: 0,
  idNumber: "",
  gender: false,
  role: "",
  avatar: "",
  createAt: "",
};

// Actions
export const login = createAction<IAuthState>("LOGIN");
export const logout = createAction("LOGOUT");
// export const gglogin = createAction<IAuthState>("GGLOGIN");

// Reducer
const authReducer = createReducer(initialState, (builder) => {
  builder
    .addCase(login, (state, action) => {
      state._id = action.payload._id;
      state.email = action.payload.email;
      state.password = action.payload.password;
      state.address = action.payload.address;
      state.age = action.payload.age;
      state.idNumber = action.payload.idNumber;
      state.gender = action.payload.gender;
      state.role = action.payload.role;
      state.avatar = action.payload.avatar;
      state.createAt = action.payload.createAt;
    })
    .addCase(logout, (state) => {
      // eslint-disable-next-line @typescript-eslint/no-unused-vars
      state._id = "";
      state.email = "";
      state.password = "";
      state.address = "";
      state.age = 0;
      state.idNumber = "";
      state.gender = false;
      state.role = "";
      state.avatar = "";
      state.createAt = "";
    });
  // .addCase(gglogin, (state, action) => {
  //   state.currentUser = action.payload.currentUser;
  //   state.name = action.payload.name;
  //   state.avatar = action.payload.avatar;
  // });
});

export default authReducer;
